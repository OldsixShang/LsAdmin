using Ls.Components;
using System.Reflection;
using Example.Domain;

namespace ExampleRepository
{
    [DependsOn(typeof(DomainComponent))]
    public class RepositoryComponent : ComponentBase {
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
