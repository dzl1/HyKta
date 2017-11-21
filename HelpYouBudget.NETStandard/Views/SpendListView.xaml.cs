using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpendListView : ContentPage
    {
        private uint duration = 200;
        private double LocalHeight { get; set; }
        public SpendListViewModel CurrentViewModel { get; set; }
        public SpendListView()
        {
            LocalHeight = Application.Current.MainPage.Height;
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new SpendListViewModel { IsBusy = true };
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
            AddGrid.TranslationY = LocalHeight;
            
            OptionsGrid.TranslationY = LocalHeight;
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                CurrentViewModel.IsBusy = true;
                if (CurrentViewModel.SpendData != null && CurrentViewModel.SpendData.Any())
                {
                    CurrentViewModel.IsBusy = false;
                    return;
                }

                //deserialize the string of saved data
                var data = await CurrentViewModel.RetrieveFile<string>(Constants.CurrentSpendList, false);

                if (!string.IsNullOrEmpty(data))
                {
                    var des = CurrentViewModel.DeserializeDataBase(CurrentViewModel.SpendData, data);

                    if (des != null && des.Any())
                    {
                        CurrentViewModel.SpendData.Replace(des);
                    }
                }

                //check for income

                var income = await CurrentViewModel.RetrieveFile<string>(Constants.IncomeFileName, false);
                if (!string.IsNullOrEmpty(income))
                {
                    double.TryParse(income, out var inc);
                    CurrentViewModel.Income = inc;
                }


                CalculateTotals();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private async void CalculateTotals()
        {

            try
            {
                if (CurrentViewModel.Income <= 0 || !CurrentViewModel.SpendData.Any())
                {
                    if (!(CurrentViewModel.Income <= 1))
                    {
                        CurrentViewModel.IncomeRemaining = CurrentViewModel.Income;
                        CurrentViewModel.HasIncome = true;
                        return;
                    }
                    else
                    {
                        var res = await DisplayAlert("Add Limit", "Would you like to add a spending limit?", "OK", "Cancel");
                        if (res)
                        {
                            ShowOptions(null, null);
                        }
                    }
                }
                var tl = 0.00;
                CurrentViewModel.TotalItems = CurrentViewModel.SpendData.Count;
                foreach (var item in CurrentViewModel.SpendData)
                {

                    double.TryParse(item.ProductPrice, out var ite);
                    if (ite > 0)
                    {
                        tl += ite;
                    }
                }
                CurrentViewModel.TotalCost = tl;
                if (CurrentViewModel.Income > 0 && CurrentViewModel.SpendData.Any())
                {
                    CurrentViewModel.HasIncome = true;
                    CurrentViewModel.IncomeRemaining = CurrentViewModel.Income - tl;
                }
                else
                {
                    CurrentViewModel.HasIncome = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void ShowSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {

            try
            {

                if (!(sender is ListView lv)) return;

                if (!(lv.SelectedItem is SpendObject sel)) return;

                var res = await DisplayAlert("Remove Item", "Are you sure you want to remove this item from the list?",
                                    "OK", "Cancel");

                if (!res)
                {
                    lv.SelectedItem = null;
                    return;
                }


                CurrentViewModel.SpendData.Remove(sel);

                CalculateTotals();

                var dt = CurrentViewModel.SerializeDataBase(CurrentViewModel.SpendData);
                var saved = await CurrentViewModel.SaveAnyString(dt, Constants.CurrentSpendList);

                lv.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void AddItemClicked(object sender, EventArgs e)
        {
            CurrentViewModel.ShowAdd = true;
            await AddGrid.TranslateTo(0, 0, duration, Easing.SinOut);

        }

        private async void ClearAndResetItems(object sender, EventArgs e)
        {

            try
            {
                var res = await DisplayAlert("Confirm Clear", "Are you sure you want to clear all items in the list?", "OK",
                    "Cancel");

                if (!res)
                {
                    return;
                }


                CurrentViewModel.SpendData = new ObservableCollection<SpendObject>();
                CurrentViewModel.SpendData.Clear();
                CalculateTotals();

                var dt = CurrentViewModel.SerializeDataBase(CurrentViewModel.SpendData);
                var saved = await CurrentViewModel.SaveAnyString(dt, Constants.CurrentSpendList);
                await OptionsGrid.TranslateTo(-LocalHeight/2, 0, duration, Easing.SinOut);

                CurrentViewModel.ShowOptions = false;
                CalculateTotals();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void CancelAddItemClicked(object sender, EventArgs e)
        {
            //await TasksGrid.TranslateTo(-LocalWidth, 0, duration, Easing.SinInOut);
            await AddGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
            CurrentViewModel.ShowAdd = false;

        }

        private async void ConfirmAddItemOkClicked(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(CurrentViewModel.Data.ProductPrice) ||
                    string.IsNullOrEmpty(CurrentViewModel.Data.ProductName))
                {

                    await DisplayAlert("Missing information",
                        "Please make sure you have filled out the product information to save", "OK");


                    return;
                }
                CurrentViewModel.SpendData.Add(CurrentViewModel.Data);
                CurrentViewModel.Data = new SpendObject();
                await AddGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
                CurrentViewModel.ShowAdd = false;
                CalculateTotals();

                var dt = CurrentViewModel.SerializeDataBase(CurrentViewModel.SpendData);
                var saved = await CurrentViewModel.SaveAnyString(dt, Constants.CurrentSpendList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await DisplayAlert("Failed", "Unable to save your list of items", "OK");
            }


        }

        private async void ShowOptions(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.ShowOptions = true;
                await OptionsGrid.TranslateTo(0, 0, duration, Easing.SinOut);
                //  await AddGrid.TranslateTo(0, 0, duration, Easing.SinOut);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }

        private async Task SaveIncome()
        {

            try
            {
                var saved = await CurrentViewModel.SaveAnyString(CurrentViewModel.Income.ToString(CultureInfo.InvariantCulture), Constants.IncomeFileName);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }


        private async void HideOptionsCancel(object sender, EventArgs e)
        {

            try
            {
                await SaveIncome();
                CalculateTotals();
                await OptionsGrid.TranslateTo(0, LocalHeight, duration, Easing.SinOut);
                CurrentViewModel.ShowOptions = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public const string Weekly = "Weekly";
        public const string Fortnightly = "Fortnightly";
        public const string Monthly = "Monthly";
        public const string Annually = "Annually";
        private async void PickerTypeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is Picker picker)) return;
                //save the selected frequency
                await SaveFrequencyOfIncomeExpenses(picker.Items[picker.SelectedIndex]);
                //set the selected frequency
                CurrentViewModel.FrequencyData = HelperShuffle.ParseEnum<FrequencyOfIncomeExpenses>(picker.Items[picker.SelectedIndex]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task SaveFrequencyOfIncomeExpenses(string freq)
        {

            try
            {
                var res = await CurrentViewModel.SaveAnyString(freq, Constants.FrequencyType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void HideShowFrequency(object sender, EventArgs e)
        {
            CurrentViewModel.ShowFrequency = !CurrentViewModel.ShowFrequency;
        }

        private void SetIsPermanentByToggle(object sender, ToggledEventArgs e)
        {
            var sw = sender as Switch;
            if (sw == null) return;

            CurrentViewModel.Data.IsPermanent = sw.IsToggled;
        }
    }


}