using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using VfpReaderSchemaExample.Providers;
using VfpReaderSchemaExample.Providers.Interfaces;
using VfpReaderSchemaExample.Services.Interfaces;
using VfpReaderSchemaExample.ViewModels.Interfaces;

namespace VfpReaderSchemaExample.ViewModels {
    public class ShellViewModel : NotificationObject, IShellViewModel, IPositionViewModel {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public WindowState WindowState { get; set; }
        public ICommand WindowClosing { get; private set; }
        
        private string _displayText;

        public string DisplayText {
            get {
                return this._displayText;
            }
            set {
                if (_displayText == value) {
                    return;
                }

                _displayText = value;
                RaisePropertyChanged(() => this.DisplayText);
            }
        }

        public ObservableCollection<string> Tables { get; private set; }

        private string _selectedTable;

        public string SelectedTable {
            get {
                return this._selectedTable;
            }
            set {
                if (_selectedTable == value) {
                    return;
                }

                _selectedTable = value;
                RaisePropertyChanged(() => SelectedTable);
                UpdateSchema();
            }
        }

        private DataTable _oleDbSchema;

        public DataTable OleDbSchema {
            get {
                return this._oleDbSchema;
            }
            set {
                if (_oleDbSchema == value) {
                    return;
                }

                _oleDbSchema = value;
                RaisePropertyChanged(() => OleDbSchema);
            }
        }

        private DataTable _vfpSchema;

        public DataTable VfpSchema {
            get {
                return this._vfpSchema;
            }
            set {
                if (_vfpSchema == value) {
                    return;
                }

                _vfpSchema = value;
                RaisePropertyChanged(() => VfpSchema);
            }
        }

        private readonly ISchemaProvider _schemaProvider;
        private readonly IShellPositionService _shellPositionService;

        public ShellViewModel(ITableNamesProvider tableNamesProvider,
                              ISchemaProvider schemaProvider,
                              IShellPositionService shellPositionService) {
            ArgumentUtility.CheckNotNull("tableNamesProvider", tableNamesProvider);

            _schemaProvider = ArgumentUtility.CheckNotNull("schemaProvider", schemaProvider);
            _shellPositionService = ArgumentUtility.CheckNotNull("shellPositionService", shellPositionService);

            Tables = new ObservableCollection<string>(tableNamesProvider.GetTableNames());
            DisplayText = "Schema Compare";

            _shellPositionService.LoadPosition(this);

            WindowClosing = new DelegateCommand<CancelEventArgs>(OnWindowClosing);
        }

        private void OnWindowClosing(CancelEventArgs e) {
            _shellPositionService.SavePosition(this);
        }

        private void UpdateSchema() {
            var schema = _schemaProvider.GetSchema(SelectedTable);

            OleDbSchema = schema.Tables[SchemaProvider.OleDbSchemaTableName];
            VfpSchema = schema.Tables[SchemaProvider.VfpSchemaTableName];
        }
    }
}