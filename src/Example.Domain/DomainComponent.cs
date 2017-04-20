using Ls.Components;
using System.Reflection;

namespace Example.Domain
{
    public class DomainComponent : ComponentBase {
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
