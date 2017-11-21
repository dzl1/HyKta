using System;
using System.Collections.ObjectModel;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<ChartDataModel> data = new ObservableCollection<ChartDataModel>();
        public ObservableCollection<ChartDataModel> Data
        {
            get => data;
            set
            {
                if (Data == value) return;
                data = value;
                RaisePropertyChanged();
            }
        }

        public HomeViewModel()
        {
            Data = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Labour", 28),
                new ChartDataModel("Legal", 10),
                new ChartDataModel("Production", 20),
                new ChartDataModel("License", 15),
                new ChartDataModel("Facilities", 23),
                new ChartDataModel("Taxes", 17),
                new ChartDataModel("Insurance", 12)
            };
        }

        private string mainDataString;
        public string MainDataString
        {
            get => mainDataString;
            set
            {
                if (MainDataString == value) return;
                mainDataString = value;
                RaisePropertyChanged();
            }
        }



    }


    public class ChartDataModel
    {
        public string Name { get; set; }

        public DateTime date { get; set; }

        public double Value { get; set; }

        public double Value1 { get; set; }

        public double Size { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public ChartDataModel(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public ChartDataModel(string name, double value, double size)
        {
            Name = name;
            Value = value;
            Size = size;
        }

        public ChartDataModel(double value, double value1, double size)
        {
            Value1 = value;
            Value = value1;
            Size = size;
        }

        public ChartDataModel(string name, double high, double low, double open, double close)
        {
            Name = name;
            High = high;
            Low = low;
            Value = open;
            Size = close;
        }
        public ChartDataModel(DateTime Date, double high, double low, double open, double close)
        {
            date = Date;
            High = high;
            Low = low;
            Value = open;
            Size = close;
        }
        public ChartDataModel(double value, double size)
        {
            Value = value;
            Size = size;
        }

        public ChartDataModel(DateTime dateTime, double value)
        {
            date = dateTime;
            Value = value;
        }
    }
}