using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace obudai_webapp.Models
{
    public class BalanceContext : DbContext
    {
        public BalanceContext() : base("BalanceContext")
        {
        }

        public DbSet<Balance> BalanceItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}