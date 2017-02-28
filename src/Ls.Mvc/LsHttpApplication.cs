using Ls.Reflection;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace Ls.Mvc {
    /// <summary>
    /// Ls Web 应用程序入口。
    /// </summary>
    public abstract class LsHttpApplication : HttpApplication {
        protected string ApplicationName { get; set; }
        protected Bootstrapper Bootstrapper { get; set; }

        protected LsHttpApplication() {
            Bootstrapper = new Bootstrapper();
        }

        protected virtual void Application_Start(object sender, EventArgs e) {
            if (!Bootstrapper.IocManager.IsRegistered(typeof(IAssemblyFinder))) {
                Bootstrapper.IocManager.IocContainer.Register(
                    Component.For<IAssemblyFinder>().ImplementedBy<WebAssemblyFinder>().LifestyleSingleton());
            }
            Bootstrapper.Initialize();
            ControllerBuilder.Current.SetControllerFactory(Bootstrapper.IocManager.Resolve<LsControllerFactory>());
        }

        protected virtual void Application_End() {
            Bootstrapper.Dispose();
        }
    }

    public class WebAssemblyFinder : IAssemblyFinder {
        public List<Assembly> GetAllAssemblies() {
            var assembliesInBinFolder = new List<Assembly>();

            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            var dllFiles = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin\\", "*.dll", SearchOption.TopDirectoryOnly).ToList();

            foreach (string dllFile in dllFiles) {
                var locatedAssembly = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllFile)));
                if (locatedAssembly != null) {
                    assembliesInBinFolder.Add(locatedAssembly);
                }
            }

            return assembliesInBinFolder;
        }
    }
}
