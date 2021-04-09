using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public ICommand ExportCSVFile { get; set; }

        public MainViewModel()
        {
            ExportCSVFile = new RelayCommand(ExportCSVFileExecute);
        }

        private async void ExportCSVFileExecute(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv файлы(*.csv)|*.csv|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    //using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.Default))
                    //{
                        MainWindowDataGrid = new ObservableCollection<MainWindowDataGridModel>();

                    //await Task.Run(() =>
                    //{
                    //    for (int i = 0; i < 1000; i++)
                    //    {
                    //        MainWindowDataGrid.Add(
                    //            new MainWindowDataGridModel()
                    //            {
                    //                Loc_Date = DateTime.Parse("02,02,2020"),
                    //                Object_A = "02,02,2020",
                    //                Type_A = "02,02,2020",
                    //                Object_B = "02,02,2020",
                    //                Type_B = "02,02,2020",
                    //                Direction = "02,02,2020",
                    //                Color = "02,02,2020",
                    //                Intensity = "02,02,2020",
                    //                LatitudeA = 60,
                    //                LongitudeA = 60,
                    //                LatitudeB = 60,
                    //                LongitudeB = 60
                    //            });
                    //    }
                    //});
                    //string getLine;
                    //while ((getLine = sr.ReadLine()) != null)
                    //{
                    //    string[] getSplitLine = getLine.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    //    DateTime tryToDate;
                    //    if (getSplitLine.Length == 12 && DateTime.TryParse(getSplitLine[0], out tryToDate))
                    //    {
                    //        MainWindowDataGrid.Add(
                    //            new MainWindowDataGridModel()
                    //            {
                    //                Loc_Date = DateTime.Parse(getSplitLine[0]),
                    //                Object_A = getSplitLine[1],
                    //                Type_A = getSplitLine[2],
                    //                Object_B = getSplitLine[3],
                    //                Type_B = getSplitLine[4],
                    //                Direction = getSplitLine[5],
                    //                Color = getSplitLine[6],
                    //                Intensity = getSplitLine[7],
                    //                LatitudeA = Convert.ToDouble(getSplitLine[8].Replace('.', ',')),
                    //                LongitudeA = Convert.ToDouble(getSplitLine[9].Replace('.', ',')),
                    //                LatitudeB = Convert.ToDouble(getSplitLine[10].Replace('.', ',')),
                    //                LongitudeB = Convert.ToDouble(getSplitLine[11].Replace('.', ','))
                    //            });
                    //    }
                    //}

                    await Task.Run(() =>
                    {
                        MainWindowDataGrid = ReadFileExcute(openFileDialog.FileName);
                    });

                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("Выбранный файл занят. Прожалуйста выбирете другой файл.", "Ошибка!");
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Непредвиденная ошибка!\n]n" + exp, "Ошибка!");
                }
            }
        }

        private ObservableCollection<MainWindowDataGridModel> ReadFileExcute(string FileName)
        {
            using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
            {
                ObservableCollection<MainWindowDataGridModel> localOC = new ObservableCollection<MainWindowDataGridModel>();
                string getLine;
                while ((getLine = sr.ReadLine()) != null)
                {
                    string[] getSplitLine = getLine.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime tryToDate;
                    if (getSplitLine.Length == 12 && DateTime.TryParse(getSplitLine[0], out tryToDate))
                    {
                        localOC.Add(
                            new MainWindowDataGridModel()
                            {
                                Loc_Date = DateTime.Parse(getSplitLine[0]),
                                Object_A = getSplitLine[1],
                                Type_A = getSplitLine[2],
                                Object_B = getSplitLine[3],
                                Type_B = getSplitLine[4],
                                Direction = getSplitLine[5],
                                Color = getSplitLine[6],
                                Intensity = getSplitLine[7],
                                LatitudeA = Convert.ToDouble(getSplitLine[8].Replace('.', ',')),
                                LongitudeA = Convert.ToDouble(getSplitLine[9].Replace('.', ',')),
                                LatitudeB = Convert.ToDouble(getSplitLine[10].Replace('.', ',')),
                                LongitudeB = Convert.ToDouble(getSplitLine[11].Replace('.', ','))
                            });
                    }
                }
                return localOC;
            }
        }

    }
}
