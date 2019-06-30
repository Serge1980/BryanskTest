using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizMeteoWCF.Db
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    class CWtrContext : DbContext
    {
        public CWtrContext()
                : base("conn")
            {
        }

        public DbSet<Cityes> Cityes { get; set; }
        public DbSet<City_Weather> CityWeather { get; set; }
    }
}
