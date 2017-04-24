using Example.Domain.Entities.Authorization;

namespace Example.Repository.Mapping.Authorization
{
    /// <summary>
    /// 权限映射
    /// </summary>
    public class MenuMap : BaseEntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menu", Schema.Authority);

            this.HasKey(x => x.Id);
            this.Property(x => x.Name)
                .IsRequired();
            this.Property(x => x.MenuType)
                .IsRequired();
            //关系配置
            //this.HasMany(t => t.PermissionExtensions)
            //    .WithOptional(t => t.Menu)
            //    .HasForeignKey(t=>t.MenuId)
            //    .WillCascadeOnDelete(false);
        }
    }
}