using Ls.Authorization;
using Ls.Components;
using Ls.IoC;
using Ls.Logging;
using Castle.MicroKernel.Registration;
using System.Reflection;
using Example.Domain;
using Example.Domain.Entities.Authorization;

namespace Example.Application
{
    [DependsOn(typeof(DomainComponent))]
    public class ApplicationComponent : ComponentBase {
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            if (IocManager.IsRegistered(typeof(IAuthStore<User,Role,Permission,AuthAction,Menu>)))
            {
                IocManager.Release(typeof(IAuthStore<User, Role, Permission, AuthAction, Menu>));
            }
            //Automap 映射初始化
            ApplicationStartUp.AutoMapInit();
        }
    }
}
