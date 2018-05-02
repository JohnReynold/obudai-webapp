using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace obudai_webapp.Models
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext() : base("CurrencyContext")
        {
        }

        public DbSet<Currency> CurrencyItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}