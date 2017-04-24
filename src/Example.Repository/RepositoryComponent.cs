using Ls.Components;
using System.Reflection;
using Example.Domain;
using Ls.Authorization;
using Example.Domain.Entities.Authorization;
using Example.Repository.Repositories;

namespace ExampleRepository
{
    [DependsOn(typeof(DomainComponent))]
    public class RepositoryComponent : ComponentBase {
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
