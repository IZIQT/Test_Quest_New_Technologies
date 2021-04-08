using System;
using Test_Quest_New_Technologies.ViewModel;

namespace Test_Quest_New_Technologies.Model
{
    class MainWindowDataGridModel:ViewModelBase
    {
        //        Date	Object A	Type A	Object B	Type B	Direction	Color	Intensity	LongitudeA	LongitudeA	LatitudeB	LongitudeB

        private DateTime loc_Date;
        public DateTime Loc_Date
        {
            get => loc_Date;
            set
            {
                loc_Date = value;
                OnPropertyChanged(nameof(loc_Date));
            }
        }

    }
}
