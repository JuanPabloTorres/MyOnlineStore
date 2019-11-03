using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Audits;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Stores;
using MyOnlineStore.Shared.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Context
{
    enum MyEnum
    {
        ClientUser = 1
    }
    public class MyContext : DbContext
    {
        public DbSet<Audit> Audits { get;private set; }
        public DbSet<BaseUser> Users { get; private set; }
        public DbSet<RoleType> Roles { get; private set; }
        public DbSet<Role> RoleUser { get; private set; }
        public DbSet<FavoriteStoreClient> ClientsFavoritesStores { get; private set; }
        public DbSet<Location> Locations { get; private set; }
        public DbSet<Store> Stores { get; private set; }

        public DbSet<Order> Orders { get; private set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public MyContext() { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=192.168.10.120,40935;Initial Catalog=MyOnlineStore;Persist Security Info=True; Integrated Security=False; User ID=SA;Password=FPNe1eXv");
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionStrings:DefaultConnection"].ConnectionString);
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPF5QA4\\SQLEXPRESS;Initial Catalog=MyOnlineStore;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>();

            modelBuilder
                .Entity<StoreEmployee>()
                .HasKey(se => new { se.StoreId, se.UserId });
            modelBuilder
                .Entity<StoreEmployee>()
                .HasOne(se => se.EmployeeUser)
                .WithMany(se => se.MyWorkStore)
                .HasForeignKey(se => se.UserId);
            modelBuilder
                .Entity<StoreEmployee>()
                .HasOne(se => se.Store);

            modelBuilder
                .Entity<FavoriteStoreClient>()
                .HasKey(fs => new { fs.UserId, fs.StoreId });
            modelBuilder
                .Entity<FavoriteStoreClient>()
                .HasOne(fs => fs.Client)
                .WithMany(fs => fs.FavoriteStores)
                .HasForeignKey(fs => fs.UserId);
            modelBuilder
                .Entity<FavoriteStoreClient>()
                .HasOne(fs => fs.Store);

            modelBuilder
                .Entity<Store>()
                .HasMany(s => s.MyProducts)
                .WithOne(pi => pi.MyStore);

            //modelBuilder
            //    .Entity<BaseUser>()
            //    .HasDiscriminator(user => user.Discriminator)
            //    .HasValue<string>(typeof(ClientUser).Name)
            //    .HasValue<string>(typeof(EmployeeUser).Name)
            //    .HasValue<string>(typeof(AdminUser).Name);

            modelBuilder
               .Entity<BaseUser>()
               .HasDiscriminator(user=>user.Discriminator)
               .HasValue<ClientUser>(typeof(ClientUser).Name)
               .HasValue<EmployeeUser>(typeof(EmployeeUser).Name)
               .HasValue<AdminUser>(typeof(AdminUser).Name);
             

            modelBuilder
                .Entity<EmployeeUser>()
                .HasMany<Role>()
                .WithOne()
                .HasForeignKey("MyUserId");
            modelBuilder
                .Entity<EmployeeUser>()
                .HasMany(eu => eu.UserCards)
                .WithOne()
                .HasForeignKey("MyUserId");

            modelBuilder
                .Entity<AdminUser>()
                .HasMany(au => au.UserCards)
                .WithOne()
                .HasForeignKey("MyUserId");

            modelBuilder
                .Entity<ClientUser>()
                .HasMany(cu => cu.UserCards)
                .WithOne()
                .HasForeignKey("MyUserId");

            modelBuilder
                .Entity<Role>()
                .HasKey(r => new { r.MyUserId, r.RoleReferenceId });
            modelBuilder
                .Entity<Role>()
                .HasOne(r => r.MyUser)
                .WithMany(r => r.Roles)
                .HasForeignKey(r => r.MyUserId);
            modelBuilder
               .Entity<Role>()
               .HasOne(r => r.RoleReference)
               .WithMany(r => r.Roles)
               .HasForeignKey(r => r.RoleReferenceId);


            modelBuilder.Entity<RoleType>()
                .HasData(
                new RoleType() { Id = 1, RoleName = "Store Manager" },
                new RoleType() { Id = 2, RoleName = "Employee" });
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                 
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName()// Relational().TableName
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }
    }
}
