using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Test_Quest_New_Technologies.Common.MVVM;
using Test_Quest_New_Technologies.Model;

namespace Test_Quest_New_Technologies.ViewModel
{
    class MainViewModel:ViewModelBase
    {
        private string selectedCSVFile;
        public string SelectedCSVFile
        {
            get => selectedCSVFile;
            set
            {
                if (selectedCSVFile == value) return;
                selectedCSVFile = value;
                OnPropertyChanged(nameof(SelectedCSVFile));
            }
        }

        private ObservableCollection<MainWindowDataGridModel> mainWindowDataGrid;
        public ObservableCollection<MainWindowDataGridModel> MainWindowDataGrid
        {
            get => mainWindowDataGrid;
            set
            {
                if (mainWindowDataGrid == value) return;
                mainWindowDataGrid = value;
                OnPropertyChanged(nameof(MainWindowDataGrid));
            }
        }

        ICommand ExportCSVFile { get; set; }

        public MainViewModel()
        {
            ExportCSVFile = new RelayCommand(ExportCSVFileExecute);
        }

        private void ExportCSVFileExecute(object obj)
        {
            StringBuilder ExportText = new StringBuilder();
            using (StreamReader sr = new StreamReader(selectedCSVFile))
            {
                ExportText = await sr.ReadToEndAsync();
            }
        }
    }
}
