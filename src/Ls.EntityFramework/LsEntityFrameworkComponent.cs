using Ls.Components;
using Ls.EntityFramework.Repositories;
using Ls.Extensions;
using Ls.Reflection;
using System.Data.Entity;
using System.Reflection;

namespace Ls.EntityFramework {
    /// <summary>
    /// 框架 EF 组件。
    /// </summary>
    [DependsOn(typeof(LsCoreComponent))]
    public class LsEntityFrameworkComponent : ComponentBase {
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 创建<see cref="LsEntityFrameworkComponent"/>类型对象。
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public LsEntityFrameworkComponent(ITypeFinder typeFinder) {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 组件初始化前操作。
        /// </summary>
        public override void PreInitialize() {
            IocManager.AddConventionalRegistrar(new EfConventionalRegistrar());
        }

        /// <summary>
        /// 组件初始化。
        /// </summary>
        public override void Initialize() {
            RegisterGenericRepositories();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        private void RegisterGenericRepositories() {
            var dbContextTypes =
                _typeFinder.GetAllTypes(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(DbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty()) {
                return;
            }

            foreach (var dbContextType in dbContextTypes) {
                EfRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
        }
    }
}
