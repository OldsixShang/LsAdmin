using Ls.EntityFramework.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Example.Domain.Entities.Authorization;
using Example.Repository.Mapping;

namespace Example.Repository
{
    public class ExampleDbContext : LsDbContext
    {
        public ExampleDbContext()
            : base("conn_debug")
        {

        }
#if Debug
        public ExampleDbContext()
            : base("conn_debug")
        {
            
        }
#endif
#if Release
        public ExampleDbContext()
            : base("conn_release")
        {
            
        }
#endif
        static ExampleDbContext()
        {
            System.Data.Entity.Database.SetInitializer<ExampleDbContext>(null);
        }



        #region Authiorization
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AuthAction> Actions { get; set; }

        #endregion
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
               type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
