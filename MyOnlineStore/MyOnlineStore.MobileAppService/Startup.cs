using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.MobileAppService.Repositories;
using MyOnlineStore.MobileAppService.CustomAttribute;

namespace MyOnlineStore.MobileAppService
{
    public class Startup
    {
        private static readonly string _ActiveEnviroment = "ActiveEnviromentStage";
        public static bool Mode { get; private set; }
        public IConfiguration Configuration;
        public string _ConnectionString { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Console.WriteLine($"App Root Path: {env.ContentRootPath}");           
            
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile($"appsettings.json", optional: false)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();

            Configuration = builder.Build();

            env.EnvironmentName = Configuration.GetSection(_ActiveEnviroment).Value;

            Mode = env.IsDevelopment();
            Console.WriteLine($"Enviroment: { env.EnvironmentName}");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()))
                .AddNewtonsoftJson();
            services.AddControllers();

            //services.AddSingleton<IRepository, Repository>();
            services.AddScoped<IRoleRefRepository, RoleTypeRepository>();
            services.AddScoped(typeof(IUsersRepository<>),typeof(UsersRepository<>));
            services.AddScoped<IUserCardRepository, UserCardRepository>();
            services.AddScoped<IStoresRepository, StoresRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            if (Mode)
            {
                _ConnectionString = Configuration.GetConnectionString("DevelopmentDBLocal");
                Console.WriteLine(_ConnectionString);
                Console.WriteLine("################################");
                services.AddDbContext<MyContext>(options => options.UseSqlServer(_ConnectionString));
            }
            else
            {
                Console.WriteLine("Not in Dev Mode".ToUpper());
                // Production DB
                //_ConnectionString = Configuration.GetConnectionString("DevelopmentDB");
                //services.AddDbContext<MyContext>(options => options.UseSqlServer(_ConnectionString));
            }


            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            { ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All });

            Console.WriteLine($"=============== Listening ===============");
        }
    }
}