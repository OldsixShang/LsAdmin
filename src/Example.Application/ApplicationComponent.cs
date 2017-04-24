using Ls.Authorization;
using Ls.Components;
using Ls.IoC;
using Ls.Logging;
using Castle.MicroKernel.Registration;
using System.Reflection;
using Example.Domain;
using Example.Domain.Entities.Authorization;
using Example.Repository.Repositories;

namespace Example.Application
{
    [DependsOn(typeof(DomainComponent))]
    public class ApplicationComponent : ComponentBase {
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            if (IocManager.IsRegistered(typeof(IAuthStore)))
            {
                IocManager.Release(typeof(IAuthStore));
            }
            //Automap 映射初始化
            ApplicationStartUp.AutoMapInit();
        }
    }
}
