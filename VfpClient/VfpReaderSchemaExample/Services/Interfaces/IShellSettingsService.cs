using System.Windows;

namespace VfpReaderSchemaExample.Services.Interfaces {
    public interface IShellSettingsService {
        double Height { get; set; }
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        WindowState WindowState { get; set; }
        void Save();
    }
}