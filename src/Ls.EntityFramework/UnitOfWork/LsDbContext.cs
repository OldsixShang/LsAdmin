using System.Data.Entity;
using EntityFramework.DynamicFilters;
using Ls.Domain.Entities;
using Ls.Session;

namespace Ls.EntityFramework.UnitOfWork {
    /// <summary>
    /// 框架<see cref="DbContext"/>基类。
    /// </summary>
    public abstract class LsDbContext : DbContext {
        public ILsSession LsSession { get; set; }

        public LsDbContext(string conn) : base(conn) {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Filter("SoftDelete", (ISoftDelete d) => d.IsDeleted, false);
        }
    }
}
