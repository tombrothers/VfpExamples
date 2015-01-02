using System.Windows;
using VfpReaderSchemaExample.Services.Interfaces;

namespace VfpReaderSchemaExample.Services {
    public class PresentationService : IPresentationService {
        public double VirtualScreenWidth {
            get {
                return SystemParameters.VirtualScreenWidth;
            }
        }

        public double VirtualScreenHeight {
            get {
                return SystemParameters.VirtualScreenHeight;
            }
        }

        public double PrimaryScreenWidth {
            get {
                return SystemParameters.PrimaryScreenWidth;
            }
        }

        public double PrimaryScreenHeight {
            get {
                return SystemParameters.PrimaryScreenHeight;
            }
        }
    }
}