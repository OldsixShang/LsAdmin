using Ls.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Example.Repository.Mapping
{
    public abstract class BaseEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : Entity
    {
        protected BaseEntityTypeConfiguration()
        {
            //关闭数值主键的标识
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
