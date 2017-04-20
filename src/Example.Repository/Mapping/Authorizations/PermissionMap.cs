using System.Collections.Generic;
using Example.Domain.Entities.Authorization;
using Example.Repository.Mapping;

namespace Example.Repository.Mapping.Authorization
{
    /// <summary>
    /// 权限映射
    /// </summary>
    public class PermissionMap : BaseEntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            ToTable("Permissions", Schema.Authority);

            this.HasKey(x => x.Id);
            this.Property(x => x.Name)
                .IsRequired();
            this.HasOptional(t => t.Parent)
                .WithMany(t => (IList<Permission>)t.Children)
                .WillCascadeOnDelete(false);
            this.HasOptional(t => t.Menu);
            this.HasOptional(t => t.Action);
        }
    }
}