using VfpReaderSchemaExample.ViewModels.Interfaces;

namespace VfpReaderSchemaExample.Services.Interfaces {
    public interface IShellPositionService {
        void LoadPosition(IPositionViewModel positionViewModel);
        void SavePosition(IPositionViewModel positionViewModel);
    }
}