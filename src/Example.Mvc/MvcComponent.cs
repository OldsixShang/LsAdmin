using Example.Domain;
using ExampleRepository;
using Ls.Components;
using System.Reflection;
using Example.Application;

namespace Example.Management
{
    [DependsOn(typeof(ApplicationComponent), typeof(DomainComponent), typeof(RepositoryComponent))]
    public class MvcComponent : ComponentBase
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}