using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Mobiles.Models
{
    public class MobilesContext:DbContext
    {
        public MobilesContext()
            : base("name=DefaultConnection")
        {
            //Database.SetInitializer<MobilesContext>(new DropCreateDatabaseAlways<MobilesContext>());
        }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Manufacturer> Manfacturers { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}