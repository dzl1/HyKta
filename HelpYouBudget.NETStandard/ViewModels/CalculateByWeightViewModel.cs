namespace HelpYouBudget.NETStandard.ViewModels
{
    public enum WeightType
    {
        Millilitres,
        Litres,
        Grams,
        Ounce,
        Gallon,
        Kilograms,
        CubicMetres
    }
    public class CalculateByWeightViewModel : BaseViewModel
    {
        private string stats;

        public string Stats
        {
            get => stats;
            set
            {
                if (Stats == value) return;
                stats = value;
                RaisePropertyChanged();
            }
        }

        public CalculateByWeightViewModel()
        {
            if (string.IsNullOrEmpty(TotalBulkWeight))
            {
                TotalBulkWeight = "Total weight in...";
            }
        }

        private string totalBulkWeight;

        public string TotalBulkWeight
        {
            get => totalBulkWeight;
            set
            {
                if (TotalBulkWeight == value) return;
                totalBulkWeight = value;
                RaisePropertyChanged();
            }
        }


        private WeightType myData;

        public WeightType MyData
        {
            get => myData;
            set
            {
                if (MyData == value) return;
                myData = value;
                RaisePropertyChanged();
            }
        }

        private string perBulkWeight;

        public string PerBulkWeight
        {
            get => perBulkWeight;
            set
            {
                if (PerBulkWeight == value) return;
                perBulkWeight = value;
                RaisePropertyChanged();
            }
        }


        private bool productCalculator;

        public bool ProductCalculator
        {
            get => productCalculator;
            set
            {
                if (ProductCalculator == value) return;
                productCalculator = value;
                RaisePropertyChanged();
            }
        }

        private bool bulkCalculator;

        public bool BulkCalculator
        {
            get => bulkCalculator;
            set
            {
                if (BulkCalculator == value) return;
                bulkCalculator = value;
                RaisePropertyChanged();
            }
        }

        private string bulkPrice;

        public string BulkPrice
        {
            get => bulkPrice;
            set
            {
                if (BulkPrice == value) return;
                bulkPrice = value;
                RaisePropertyChanged();
            }
        }

        private string bulkWeight;

        public string BulkWeight
        {
            get => bulkWeight;
            set
            {
                if (BulkWeight == value) return;
                bulkWeight = value;
                RaisePropertyChanged();
            }
        }


        private WeightType myDataTwo;

        public WeightType MyDataTwo
        {
            get => myDataTwo;
            set
            {
                if (MyDataTwo == value) return;
                myDataTwo = value;
                RaisePropertyChanged();
            }
        }

        private WeightType bulkData;

        public WeightType BulkData
        {
            get => bulkData;
            set
            {
                if (BulkData == value) return;
                bulkData = value;
                TotalBulkWeight = "Total weight in : " + value;
                RaisePropertyChanged();
            }
        }


        private  string price;

        public  string Price
        {
            get => price;
            set
            {
                if (Price == value) return;
                price = value;
                RaisePropertyChanged();
            }
        }

        private string priceTwo;

        public string PriceTwo
        {
            get => priceTwo;
            set
            {
                if (PriceTwo == value) return;
                priceTwo = value;
                RaisePropertyChanged();
            }
        }
        private string volumeTwo;

        public string VolumeTwo
        {
            get => volumeTwo;
            set
            {
                if (VolumeTwo == value) return;
                volumeTwo = value;
                RaisePropertyChanged();
            }
        }


        private bool compareTwoProducts;

        public bool CompareTwoProducts
        {
            get => compareTwoProducts;
            set
            {
                if (CompareTwoProducts == value) return;
                compareTwoProducts = value;
                RaisePropertyChanged();
            }
        }

        private string volume;

        public string Volume
        {
            get => volume;
            set
            {
                if (Volume == value) return;
                volume = value;
                RaisePropertyChanged();
            }
        }



    }
}