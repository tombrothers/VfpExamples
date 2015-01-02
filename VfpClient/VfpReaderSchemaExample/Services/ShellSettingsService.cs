using System.Windows;
using VfpReaderSchemaExample.Properties;
using VfpReaderSchemaExample.Services.Interfaces;

namespace VfpReaderSchemaExample.Services {
    public class ShellSettingsService : IShellSettingsService {
        public double Left {
            get {
                return Settings.Default.ShellLeft;
            }
            set {
                Settings.Default.ShellLeft = value;
            }
        }

        public double Top {
            get {
                return Settings.Default.ShellTop;
            }
            set {
                Settings.Default.ShellTop = value;
            }
        }

        public double Width {
            get {
                return Settings.Default.ShellWidth;
            }
            set {
                Settings.Default.ShellWidth = value;
            }
        }

        public double Height {
            get {
                return Settings.Default.ShellHeight;
            }
            set {
                Settings.Default.ShellHeight = value;
            }
        }

        public WindowState WindowState {
            get {
                return Settings.Default.ShellWindowState;
            }
            set {
                Settings.Default.ShellWindowState = value;
            }
        }

        public void Save() {
            Settings.Default.Save();
        }
    }
}