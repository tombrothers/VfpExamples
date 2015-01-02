using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace VfpReaderSchemaExample {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();

            bootstrapper.Run();
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            MessageBox.Show(e.Exception.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}