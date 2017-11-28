using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverallBudgetView : ContentPage
    {
        private FrequencyOfIncomeExpenses currentFequencyUsedToCalculate = FrequencyOfIncomeExpenses.Weekly;
        private uint duration = 100;
        private double LocalHeight { get; set; }
        public OverallBudgetViewModel CurrentViewModel { get; set; }
        public OverallBudgetView()
        {
            LocalHeight = Application.Current.MainPage.Height;
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new OverallBudgetViewModel { IsBusy = true };
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
            OptionsGrid.TranslationY = LocalHeight;
            FrequencyStatsPicker.SelectedIndex = 0;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                CurrentViewModel.IsBusy = true;
                if (CurrentViewModel != null && CurrentViewModel.BudgetDataEntry.Any())
                {
                    CurrentViewModel.IsBusy = false;
                    return;
                }

                //check for saved data
                var data = await CurrentViewModel.RetrieveFile<string>(Constants.BudgetDataFileName, false);

                if (!string.IsNullOrEmpty(data))
                {
                    var res = CurrentViewModel.DeserializeDataBase(CurrentViewModel.BudgetDataEntry, data);
                    if (res != null && res.Any())
                    {
                        CurrentViewModel.BudgetDataEntry.Replace(res);
                    }
                    else
                    {
                        CurrentViewModel.SetDefaultBudgetDataMain();
                    }
                    CalculateTotals(currentFequencyUsedToCalculate);
                }
                else
                {
                    CurrentViewModel.SetDefaultBudgetDataMain();
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
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                //save the data
                var str = CurrentViewModel.SerializeDataBase(CurrentViewModel.BudgetDataEntry);
                if (string.IsNullOrEmpty(str)) return;

                var data = await CurrentViewModel.SaveAnyString(str, Constants.BudgetDataFileName);

                if (!data)
                {
                    await DisplayAlert("Unable to save changes", "We couldn't save your changes at this time.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private int CurrentBudgetIndex = 0;
        private async void ShowSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(sender is ListView lv)) return;

            if (!(lv.SelectedItem is BudgetDataMain sel)) return;
            CurrentBudgetIndex = CurrentViewModel.BudgetDataEntry.IndexOf(sel);
            CurrentViewModel.CurrentBudgetItem = sel;
            CurrentViewModel.ShowStats = false;

            switch (sel.Frequency)
            {
                case FrequencyOfIncomeExpenses.Weekly:
                    FrequencyPicker.SelectedIndex = 0;
                    break;
                case FrequencyOfIncomeExpenses.Fortnightly:
                    FrequencyPicker.SelectedIndex = 1;
                    break;
                case FrequencyOfIncomeExpenses.Monthly:
                    FrequencyPicker.SelectedIndex = 2;
                    break;
                case FrequencyOfIncomeExpenses.Annually:
                    FrequencyPicker.SelectedIndex = 3;
                    break;
                default:
                    FrequencyPicker.SelectedIndex = 0;
                    break;
            }


            CurrentViewModel.ShowDetails = true;
            await OptionsGrid.TranslateTo(0, 0, duration, Easing.SinOut);
            //lv.SelectedItem = null;

        }

        private void PickerTypeChanged(object sender, EventArgs e)
        {
            if (!(sender is Picker pk)) return;
            CurrentViewModel.CurrentBudgetItem.Frequency = HelperShuffle.ParseEnum<FrequencyOfIncomeExpenses>(pk.Items[pk.SelectedIndex]);
        }

        private async void UpdateItemClicked(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.BudgetDataEntry[CurrentBudgetIndex] = CurrentViewModel.CurrentBudgetItem;
                var ord = CurrentViewModel.BudgetDataEntry.OrderByDescending(c => c.IncomeOrExpense).ThenBy(ed => ed.Frequency).ToList();
                CurrentViewModel.BudgetDataEntry.Replace(ord);
                CalculateTotals(currentFequencyUsedToCalculate);
                await OptionsGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
                CurrentViewModel.ShowDetails = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void CancelUpdateClicked(object sender, EventArgs e)
        {
            SpendListViewItems.SelectedItem = null;
            await OptionsGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
            CurrentViewModel.ShowDetails = false;
        }

        private void PickerTypeStatsChanged(object sender, EventArgs e)
        {


            try
            {
                if (!(sender is Picker pk)) return;
                currentFequencyUsedToCalculate = HelperShuffle.ParseEnum<FrequencyOfIncomeExpenses>(pk.Items[pk.SelectedIndex]);
                CalculateTotals(currentFequencyUsedToCalculate);

                var fre = pk.Items[pk.SelectedIndex];
                if (fre == "Annually")
                {
                    CurrentViewModel.BreakDownText = "(per Year)";
                    return;
                }


                CurrentViewModel.BreakDownText = $"(per {pk.Items[pk.SelectedIndex].Trim(new char[] { 'l', 'y' })})";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void ShowStatsClicked(object sender, EventArgs e)
        {
            CurrentViewModel.ShowStats = true;
        }
        private void HideStatsClicked(object sender, EventArgs e)
        {
            CurrentViewModel.ShowStats = false;
        }

        private async void ShowItemTapped(object sender, EventArgs e)
        {

            try
            {
                if (!(sender is Grid lv)) return;

                if (!(lv.BindingContext is BudgetDataMain sel)) return;
                CurrentBudgetIndex = CurrentViewModel.BudgetDataEntry.IndexOf(sel);
                CurrentViewModel.CurrentBudgetItem = sel;
                CurrentViewModel.ShowStats = false;
                CurrentViewModel.ShowAdd = false;
                switch (sel.Frequency)
                {
                    case FrequencyOfIncomeExpenses.Weekly:
                        FrequencyPicker.SelectedIndex = 0;
                        break;
                    case FrequencyOfIncomeExpenses.Fortnightly:
                        FrequencyPicker.SelectedIndex = 1;
                        break;
                    case FrequencyOfIncomeExpenses.Monthly:
                        FrequencyPicker.SelectedIndex = 2;
                        break;
                    case FrequencyOfIncomeExpenses.Annually:
                        FrequencyPicker.SelectedIndex = 3;
                        break;
                    default:
                        FrequencyPicker.SelectedIndex = 0;
                        break;
                }


                CurrentViewModel.ShowDetails = true;
                await OptionsGrid.TranslateTo(0, 0, duration, Easing.SinOut);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void AddItemClicked(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.ShowStats = false;
                CurrentViewModel.ShowDetails = true;
                await OptionsGrid.TranslateTo(0, 0, duration, Easing.SinOut);
                CurrentViewModel.ShowAdd = true;
                CurrentViewModel.CurrentBudgetItem = new BudgetDataMain();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void SaveItemClicked(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.BudgetDataEntry.Add(CurrentViewModel.CurrentBudgetItem);
                var ord = CurrentViewModel.BudgetDataEntry.OrderByDescending(c => c.IncomeOrExpense).ThenBy(ed => ed.Frequency).ToList();
                CurrentViewModel.BudgetDataEntry.Replace(ord);
                CalculateTotals(currentFequencyUsedToCalculate);
                await OptionsGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
                CurrentViewModel.ShowDetails = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}