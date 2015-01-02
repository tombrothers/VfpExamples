using VfpReaderSchemaExample.Services.Interfaces;
using VfpReaderSchemaExample.ViewModels.Interfaces;

namespace VfpReaderSchemaExample.Services {
    public class ShellPositionService : IShellPositionService {
        private readonly IPresentationService _presentationService;
        private readonly IShellSettingsService _shellSettingsService;

        public ShellPositionService(IPresentationService presentationService,
                                    IShellSettingsService shellSettingsService) {
            _presentationService = ArgumentUtility.CheckNotNull("presentationService", presentationService);
            _shellSettingsService = ArgumentUtility.CheckNotNull("shellSettingsService", shellSettingsService);
        }

        public void SavePosition(IPositionViewModel positionViewModel) {
            ArgumentUtility.CheckNotNull("positionViewModel", positionViewModel);

            _shellSettingsService.Left = positionViewModel.Left;
            _shellSettingsService.Top = positionViewModel.Top;
            _shellSettingsService.Height = positionViewModel.Height;
            _shellSettingsService.Width = positionViewModel.Width;
            _shellSettingsService.WindowState = positionViewModel.WindowState;

            _shellSettingsService.Save();
        }

        public void LoadPosition(IPositionViewModel positionViewModel) {
            ArgumentUtility.CheckNotNull("positionViewModel", positionViewModel);

            if (IsValidSettings()) {
                UpdatePositionUsingSettings(positionViewModel);
            }
            else {
                UpdatePositionUsingDefaults(positionViewModel);
            }

            positionViewModel.WindowState = this._shellSettingsService.WindowState;
        }

        private void UpdatePositionUsingDefaults(IPositionViewModel positionViewModel) {
            positionViewModel.Height = _presentationService.PrimaryScreenHeight / 2;
            positionViewModel.Width = _presentationService.PrimaryScreenWidth / 2;
            positionViewModel.Top = (_presentationService.PrimaryScreenHeight - positionViewModel.Height) / 2;
            positionViewModel.Left = (_presentationService.PrimaryScreenWidth - positionViewModel.Width) / 2;
        }

        private void UpdatePositionUsingSettings(IPositionViewModel positionViewModel) {
            positionViewModel.Left = _shellSettingsService.Left;
            positionViewModel.Top = _shellSettingsService.Top;
            positionViewModel.Height = _shellSettingsService.Height;
            positionViewModel.Width = _shellSettingsService.Width;
        }

        private bool IsValidSettings() {
            return _shellSettingsService.Left >= 0 &&
                   _shellSettingsService.Top >= 0 &&
                   _shellSettingsService.Width > 0 &&
                   _shellSettingsService.Height > 0 &&
                   _shellSettingsService.Left + _shellSettingsService.Width <= _presentationService.VirtualScreenWidth &&
                   _shellSettingsService.Top + _shellSettingsService.Height <= _presentationService.VirtualScreenHeight;
        }
    }
}