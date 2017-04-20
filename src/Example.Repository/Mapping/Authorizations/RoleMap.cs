using Ls.Authorization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Example.Domain.Entities.Authorization;
using Example.Repository.Mapping;

namespace Example.Repository.Mapping.Authorization
{
    /// <summary>
    /// 角色映射
    /// </summary>
    public class RoleMap : BaseEntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles", Schema.Authority);

            this.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("RolesNameIndex", 2) { IsUnique = true }));
            this.HasKey(x => x.Id);
            this.HasMany(t => t.Permissions)
                .WithMany()
                .Map(t => t.MapLeftKey("RoleId").MapRightKey("PId").ToTable("Role_Permission", Schema.Authority));
        }
    }
}