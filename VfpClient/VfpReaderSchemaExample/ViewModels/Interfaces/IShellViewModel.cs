using System.Collections.ObjectModel;
using System.Data;

namespace VfpReaderSchemaExample.ViewModels.Interfaces {
    interface IShellViewModel {
        DataTable OleDbSchema { get; set; }
        string SelectedTable { get; set; }
        ObservableCollection<string> Tables { get; }
        DataTable VfpSchema { get; set; }
    }
}