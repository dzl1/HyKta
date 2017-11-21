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
    public partial class AdminView : ContentPage
    {
        public AdminViewModel CurrentViewModel { get; set; }
        public AdminView()
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new AdminViewModel();
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {

                if (string.IsNullOrEmpty(CurrentViewModel.NewData.Id))
                {
                    CurrentViewModel.NewData = new BudgetData();
                }

                CurrentViewModel.IsBusy = true;
                await CheckLocalData();

                if (!CurrentViewModel.ResultsData.Any())
                {
                    await CheckLatestData();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        public async Task CheckLocalData()
        {

            try
            {
                var res = await CurrentViewModel.RetrieveFile<ObservableCollection<BudgetData>>(Constants.AllProductsFileName);
                if (res != null && res.Any())
                {
                    CurrentViewModel.ResultsData.Replace(res);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task CheckLatestData()
        {

            try
            {
                var res = await App.DataTable.ToListAsync();
                Constants.LocalListOfBudgetData.Replace(res);
                if (res != null && res.Any())
                {
                    foreach (var item in res)
                    {
                        item.SetLowestPrices();
                    }
                    CurrentViewModel.ResultsData.Replace(res);
                    var lstUpdated = DateTime.Now.ToString("F");
                    var ser = await CurrentViewModel.SaveAnyString(lstUpdated, Constants.LastSavedFileName);
                }

                //serialize the data
                var str = CurrentViewModel.SerializeDataBase(CurrentViewModel.ResultsData);
                //cache the data
                var saved = await CurrentViewModel.SaveAnyString(str, Constants.AllProductsFileName);
                if (!saved)
                {
                    await DisplayAlert("Unable to cache", "Didn't cache data. Debugging needed", "OK");
                }
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
                if (string.IsNullOrEmpty(CurrentViewModel.NewData.ProductName)) return;
                CurrentViewModel.IsBusy = true;
                await App.DataTable.InsertAsync(CurrentViewModel.NewData);
                CurrentViewModel.NewData = new BudgetData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;

        }

        private void ShowSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (lv == null) return;

            var sel = lv.SelectedItem as BudgetData;
            if (sel == null) return;

            CurrentViewModel.NewData = sel;
            lv.SelectedItem = null;
        }

        private async void DeleteItemClicked(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.IsBusy = true;
                await App.DataTable.DeleteAsync(CurrentViewModel.NewData);
                CurrentViewModel.NewData = new BudgetData();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private async void UpdateItemClicked(object sender, EventArgs e)
        {
            try
            {
                CurrentViewModel.IsBusy = true;
                await App.DataTable.UpdateAsync(CurrentViewModel.NewData);
                CurrentViewModel.NewData = new BudgetData();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private void ClearItemClicked(object sender, EventArgs e)
        {
            try
            {
                CurrentViewModel.IsBusy = true;
                CurrentViewModel.NewData = new BudgetData();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private async void ReloadReportsRefreshPulled(object sender, EventArgs e)
        {
            await CheckLatestData();
            ListViewProducts.IsRefreshing = false;
        }
    }

    public class ModifiedLabel : Label
    {
        public ModifiedLabel()
        {
            HeightRequest = 40;
        }
    }

    public class ModifiedEntry : DoneEntry
    {
        public ModifiedEntry()
        {
            this.HeightRequest = 40;
            this.Focused += ModifiedEntry_Focused;
        }

        private void ModifiedEntry_Focused(object sender, FocusEventArgs e)
        {
            if (this.Text == "0")
            {
                this.Text = string.Empty;
            }
        }
    }
}