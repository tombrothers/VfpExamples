using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using VfpReaderSchemaExample.ViewModels.Interfaces;
using VfpReaderSchemaExample.Views;

namespace VfpReaderSchemaExample {
    public class Bootstrapper : UnityBootstrapper {
        protected override DependencyObject CreateShell() {
            var shell = Container.Resolve<ShellView>();

            shell.DataContext = this.Container.Resolve<IShellViewModel>();
            shell.Show();

            return shell;
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();

            var assembly = typeof(Bootstrapper).Assembly;

            assembly.GetTypes()
                    .Where(x => x.IsInterface)
                    .Where(x => x.FullName.Contains(".Interfaces.I"))
                    .Select(x => new { Interface = x, Class = assembly.GetType(x.FullName.Replace(".Interfaces.I", ".")) })
                    .Where(x => x.Class != null)
                    .ToList()
                    .ForEach(x => Container.RegisterType(x.Interface, x.Class, new ContainerControlledLifetimeManager()));
        }
    }
}