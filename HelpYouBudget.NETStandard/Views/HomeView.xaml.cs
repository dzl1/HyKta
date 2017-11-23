using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Data.Entities;
using HelpYouBudget.NETStandard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private FrequencyOfIncomeExpenses currentFequencyUsedToCalculate = FrequencyOfIncomeExpenses.Weekly;
        public HomeViewModel CurrentViewModel { get; set; }
        public HomeView()
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new HomeViewModel();
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
            if (!(Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS))
            {
                Chart.Series[0].AnimationDuration = 2;
            }

            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            try
            {
                CurrentViewModel.IsBusy = true;
                if (Constants.LocalListOfBudgetData == null)
                {
                    Constants.LocalListOfBudgetData = new List<BudgetData>();
                }

                //if (CurrentViewModel != null && CurrentViewModel.BudgetDataEntry.Any())
                //{
                //    CurrentViewModel.IsBusy = false;
                //    return;
                //}

                //check for saved data
                var data = await CurrentViewModel.RetrieveFile<string>(Constants.BudgetDataFileName, false);

                if (!string.IsNullOrEmpty(data))
                {
                    var res = CurrentViewModel.DeserializeDataBase(CurrentViewModel.BudgetDataEntry, data);
                    if (res != null && res.Any())
                    {
                        CurrentViewModel.BudgetDataEntry.Replace(res);
                        CalculateTotals(currentFequencyUsedToCalculate);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;

        }


        private void CalculateTotals(FrequencyOfIncomeExpenses typeOfCalculationToPerform = FrequencyOfIncomeExpenses.Weekly)
        {

            try
            {
                CurrentViewModel.TotalIncome = 0;
                CurrentViewModel.WeeklyCosts = 0;
                CurrentViewModel.MonthlyCosts = 0;
                CurrentViewModel.AnnualCosts = 0;
                CurrentViewModel.FortnightlyCosts = 0;



                //*****calculate everything for weekly then set at the end
                foreach (var item in CurrentViewModel.BudgetDataEntry)
                {
                    //get all the incomes
                    if (item.IncomeOrExpense)
                    {
                        switch (item.Frequency)
                        {
                            case FrequencyOfIncomeExpenses.Weekly:
                                CurrentViewModel.TotalIncome += item.Amount;
                                break;
                            case FrequencyOfIncomeExpenses.Fortnightly:
                                CurrentViewModel.TotalIncome += item.Amount * 26 / 52;
                                break;
                            case FrequencyOfIncomeExpenses.Monthly:
                                CurrentViewModel.TotalIncome += item.Amount * 12 / 52;
                                break;
                            case FrequencyOfIncomeExpenses.Annually:
                                CurrentViewModel.TotalIncome += item.Amount / 52;
                                break;
                            default:
                                CurrentViewModel.TotalIncome += item.Amount;
                                break;
                        }
                    }
                    else
                    {
                        switch (item.Frequency)
                        {
                            case FrequencyOfIncomeExpenses.Weekly:
                                CurrentViewModel.WeeklyCosts += item.Amount;
                                break;
                            case FrequencyOfIncomeExpenses.Fortnightly:
                                CurrentViewModel.FortnightlyCosts += item.Amount / 2;
                                break;
                            case FrequencyOfIncomeExpenses.Monthly:
                                CurrentViewModel.MonthlyCosts += item.Amount * 12 / 52;
                                break;
                            case FrequencyOfIncomeExpenses.Annually:
                                CurrentViewModel.AnnualCosts += item.Amount / 52;
                                break;
                            default:
                                CurrentViewModel.WeeklyCosts += item.Amount;
                                break;
                        }
                    }
                }

                switch (typeOfCalculationToPerform)
                {
                    case FrequencyOfIncomeExpenses.Weekly:
                        break;
                    case FrequencyOfIncomeExpenses.Fortnightly:
                        CurrentViewModel.TotalIncome = CurrentViewModel.TotalIncome * 52 / 26;
                        CurrentViewModel.WeeklyCosts = CurrentViewModel.WeeklyCosts * 52 / 26;
                        CurrentViewModel.FortnightlyCosts = (CurrentViewModel.FortnightlyCosts * 52) / 26;
                        CurrentViewModel.MonthlyCosts = CurrentViewModel.MonthlyCosts * 2;
                        CurrentViewModel.AnnualCosts = CurrentViewModel.AnnualCosts * 2;
                        break;
                    case FrequencyOfIncomeExpenses.Monthly:
                        CurrentViewModel.TotalIncome = CurrentViewModel.TotalIncome * 52 / 12;
                        CurrentViewModel.WeeklyCosts = CurrentViewModel.WeeklyCosts * 52 / 12;
                        CurrentViewModel.FortnightlyCosts = (CurrentViewModel.FortnightlyCosts * 52) / 12;
                        CurrentViewModel.MonthlyCosts = CurrentViewModel.MonthlyCosts * 52 / 12;
                        CurrentViewModel.AnnualCosts = CurrentViewModel.AnnualCosts * 52 / 12;
                        break;
                    case FrequencyOfIncomeExpenses.Annually:
                        CurrentViewModel.TotalIncome = CurrentViewModel.TotalIncome * 52;
                        CurrentViewModel.FortnightlyCosts = CurrentViewModel.FortnightlyCosts * 52;
                        CurrentViewModel.WeeklyCosts = CurrentViewModel.WeeklyCosts * 52;
                        CurrentViewModel.MonthlyCosts = CurrentViewModel.MonthlyCosts * 52;
                        CurrentViewModel.AnnualCosts = CurrentViewModel.AnnualCosts * 52;
                        break;
                    default:
                        break;
                }


                var totalIncome = CurrentViewModel.TotalIncome;

                var totalCosts = CurrentViewModel.WeeklyCosts + CurrentViewModel.FortnightlyCosts +
                                 CurrentViewModel.MonthlyCosts + CurrentViewModel.AnnualCosts;
                var dt = new ObservableCollection<ChartDataModel>();
                if (totalIncome > totalCosts)
                {
                    //calculate the percentage of total costs based on income
                    //var wk = (CurrentViewModel.WeeklyCosts / CurrentViewModel.TotalIncome) * 100;
                    //var ft = (CurrentViewModel.FortnightlyCosts / CurrentViewModel.TotalIncome) * 100;
                    //var mth = (CurrentViewModel.MonthlyCosts / CurrentViewModel.TotalIncome) * 100;
                    //var yr = (CurrentViewModel.AnnualCosts / CurrentViewModel.TotalIncome) * 100;
                    var wk = CurrentViewModel.WeeklyCosts;
                    var ft = CurrentViewModel.FortnightlyCosts;
                    var mth = CurrentViewModel.MonthlyCosts;
                    var yr = CurrentViewModel.AnnualCosts;
                    var leftOver = totalIncome - (wk + ft + mth + yr);


                    dt = new ObservableCollection<ChartDataModel>
                    {
                        new ChartDataModel(FrequencyOfIncomeExpenses.Weekly.ToString(), wk),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Fortnightly.ToString(), ft),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Monthly.ToString(), mth),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Annually.ToString(), yr),
                        new ChartDataModel("Remaining", leftOver)
                    };

                    CurrentViewModel.Data.Replace(dt);
                }
                else
                {
                    //calculate the percentage of costs based on the total

                    var wk = (CurrentViewModel.WeeklyCosts / totalCosts) * 100;
                    var ft = (CurrentViewModel.FortnightlyCosts / totalCosts) * 100;
                    var mth = (CurrentViewModel.MonthlyCosts / totalCosts) * 100;
                    var yr = (CurrentViewModel.AnnualCosts / totalCosts) * 100;
                    dt = new ObservableCollection<ChartDataModel>
                    {
                        new ChartDataModel(FrequencyOfIncomeExpenses.Weekly.ToString(), wk),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Fortnightly.ToString(), ft),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Monthly.ToString(), mth),
                        new ChartDataModel(FrequencyOfIncomeExpenses.Annually.ToString(), yr)
                    };
                    CurrentViewModel.Data.Replace(dt);

                }



                //dt = new ObservableCollection<ChartDataModel>
                //{
                //    new ChartDataModel(FrequencyOfIncomeExpenses.Weekly.ToString(), CurrentViewModel.WeeklyCosts),
                //    new ChartDataModel(FrequencyOfIncomeExpenses.Fortnightly.ToString(), CurrentViewModel.FortnightlyCosts),
                //    new ChartDataModel(FrequencyOfIncomeExpenses.Monthly.ToString(), CurrentViewModel.MonthlyCosts),
                //    new ChartDataModel(FrequencyOfIncomeExpenses.Annually.ToString(), CurrentViewModel.AnnualCosts)
                //};

                //CurrentViewModel.Data.Replace(dt);
                //CurrentViewModel.Data
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }








        public async Task CheckLocalData()
        {

            try
            {

                //add new object for testing
                var kt = new BudgetData()
                {
                    ProductName = "Test Product _" + DateTime.Now.ToString("F"),
                    StoreOne = "Pak n Save",
                    IsValid = true,
                    FlagForFutureUse = true,
                    Description = "Test Description.."
                };

                await App.DataTable.InsertAsync(kt);
                // }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        private async void NavigateToNext(object sender, EventArgs e)
        {
            if (!(sender is Button bt)) return;

            switch (bt.Text)
            {
                case "Admin Settings":
                    await Navigation.PushAsync(new AdminView() { Title = "Admin Tasks" });
                    break;
                case "Compare products":
                    await Navigation.PushAsync(new CompareProductsView() { Title = "Products" });
                    break;
                case "Calculate per item":
                    await Navigation.PushAsync(new CalculateByItemsView() { Title = "Calculate per item" });
                    break;
                case "Calculate by weight":
                    await Navigation.PushAsync(new CalculateByWeightView() { Title = "Calculate by weight" });
                    break;
                case "View watchlist":
                    await Navigation.PushAsync(new WatchlistView() { Title = "My Watchlist" });
                    break;
                case "Spending list":
                    await Navigation.PushAsync(new SpendListView() { Title = "Spending" });
                    break;
                case "View budget":
                    await Navigation.PushAsync(new OverallBudgetView() { Title = "Budget" });
                    break;
                case "Sale discount calculator":
                    await Navigation.PushAsync(new SaleCalculatorView() { Title = "Sale calculator" });
                    break;
            }
        }
    }
}