using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    [Table("Locations")]
    public class Location 
    {
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public override bool Equals(object obj)
        {
            bool equals;
            if (obj is Location locationObj)
            {
                equals = this.Longitude == locationObj.Longitude && this.Latitude == locationObj.Latitude;
            }
            else
                equals = false;
            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string toString = $"Latitude:{this.Latitude} & Longitude:{this.Longitude}";
            return toString;
        }

        public static bool operator ==(Location locationA, Location locationB)
        {

            return (locationA.Latitude == locationB.Latitude && locationA.Longitude == locationB.Longitude);
        }
        public static bool operator !=(Location locationA, Location locationB)
        {
            return (locationA.Latitude != locationB.Latitude || locationA.Longitude != locationB.Longitude);
        }

    }
}
