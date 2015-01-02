namespace VfpReaderSchemaExample.Services.Interfaces {
    public interface IPresentationService {
        double PrimaryScreenHeight { get; }
        double PrimaryScreenWidth { get; }
        double VirtualScreenHeight { get; }
        double VirtualScreenWidth { get; }
    }
}