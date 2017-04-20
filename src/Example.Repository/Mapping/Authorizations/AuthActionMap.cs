using Example.Domain.Entities.Authorization;

namespace Example.Repository.Mapping.Authorization
{
    /// <summary>
    /// 权限映射
    /// </summary>
    public class AuthActionMap : BaseEntityTypeConfiguration<AuthAction>
    {
        public AuthActionMap()
        {
            ToTable("AuthAction", Schema.AuthorityExtension);

            this.HasKey(x => x.Id);
            this.Property(x => x.Name)
                .IsRequired();
            this.Property(x => x.Template)
                .IsRequired();

            //关系配置
            //this.HasMany(t => t.pe)
            //    .WithOptional(t => t.Action)
            //    .HasForeignKey(t => t.ActionId)
            //    .WillCascadeOnDelete(false);
        }
    }
}