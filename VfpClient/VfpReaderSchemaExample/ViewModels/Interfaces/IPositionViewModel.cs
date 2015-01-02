using System.Windows;

namespace VfpReaderSchemaExample.ViewModels.Interfaces {
    public interface IPositionViewModel {
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        WindowState WindowState { get; set; }
    }
}