using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Views;
using Xamarin.Forms;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class SaleCalculatorViewModel : BaseViewModel
    {
        private SpendObject spendData;

        public SpendObject SpendData
        {
            get => spendData;
            set
            {
                if (SpendData == value) return;
                spendData = value;
                RaisePropertyChanged();
            }
        }

       

        private double percentage;

        public double Percentage
        {
            get => percentage;
            set
            {
                if (Percentage == value) return;
                percentage = value;
                RaisePropertyChanged();
            }
        }

        private bool showAdd;

        public bool ShowAdd
        {
            get => showAdd;
            set
            {
                if (ShowAdd == value) return;
                showAdd = value;
                RaisePropertyChanged();
            }
        }

        private bool enableAddButton;

        public bool EnableAddButton
        {
            get => enableAddButton;
            set
            {
                if (EnableAddButton == value) return;
                enableAddButton = value;
                RaisePropertyChanged();
            }
        }

        private double price;

        public double Price
        {
            get => price;
            set
            {
                if (Price == value) return;
                price = value;
                RaisePropertyChanged();
            }
        }

        private double discountedPrice;

        public double DiscountedPrice
        {
            get => discountedPrice;
            set
            {
                if (DiscountedPrice == value) return;
                discountedPrice = value;
                DiscountedPriceString = HelperShuffle.SignificantTruncate(value,2).ToString(CultureInfo.InvariantCulture);
                RaisePropertyChanged();
            }
        }

        private string discountedPriceString;

        public string DiscountedPriceString
        {
            get => discountedPriceString;
            set
            {
                if (DiscountedPriceString == value) return;
                discountedPriceString = value;
                RaisePropertyChanged();
            }
        }




        public SaleCalculatorViewModel()
        {
            if (SpendData == null)
            {
                SpendData = new SpendObject();
            }
        }

        public async Task AddItemToSpendList()
        {

            try
            {
                SpendData.ProductPrice = DiscountedPriceString;


                //deserialize the string of saved data
                var data = await RetrieveFile<string>(Constants.CurrentSpendList, false);

                if (!string.IsNullOrEmpty(data))
                {
                    var des = DeserializeDataBase(new ObservableCollection<SpendObject>(), data);

                    if (des != null && des.Any())
                    {
                        des.Add(SpendData);
                        //serialze and save
                        var res = SerializeDataBase(des);
                        var result = await SaveAnyString(res, Constants.CurrentSpendList);
                    }
                    else
                    {
                        //save the item in a new list and then write to local storage
                        var newData = new ObservableCollection<SpendObject> {SpendData};
                        var res = SerializeDataBase(newData);
                        var result = await SaveAnyString(res, Constants.CurrentSpendList);
                    }
                }
                else
                {

                    //save the item in a new list and then write to local storage
                    var newData = new ObservableCollection<SpendObject> { SpendData };
                    var res = SerializeDataBase(newData);
                    var result = await SaveAnyString(res, Constants.CurrentSpendList);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}