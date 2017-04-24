using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Example.Domain.Entities.Authorization;
using Example.Repository.Mapping;

namespace ExampleRepository.Mapping.Authorization
{
    /// <summary>
    /// 用户映射
    /// </summary>
    public class UserMap : BaseEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users", Schema.Authority);

            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(x => x.LoginId)
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("LOGINIDINDEX") { IsUnique = true }))
                .IsRequired();
            this.HasOptional(t => t.Role)
                .WithMany();
        }
    }
}