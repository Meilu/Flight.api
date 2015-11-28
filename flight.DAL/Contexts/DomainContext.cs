using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight.DAL.Entities;
using flight.library.Attributes;

namespace flight.DAL.Contexts
{
    [UnityIoCTransientLifetimed]
    public class DomainContext : BaseContext, IDomainContext
    {
        public DomainContext() : base("Domain")
        {
            // check for migration
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DomainContext>());
        }

        public DbSet<ApiUserEntity> ApiUsers { get; set; }

        // make sure the tables that are being generated are not plural.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // load the configurations that belong to this context.
       
            modelBuilder.Configurations.Add(new ApiUserEntityConfiguration());

        }
    }
}
