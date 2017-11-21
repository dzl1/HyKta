using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Data.Entities;
using HelpYouBudget.NETStandard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompareProductsView
    {
        public CompareProductsViewModel CurrentViewModel { get; set; }
        public CompareProductsView()
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new CompareProductsViewModel();
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //return if we are navigating back from another page
                if (CurrentViewModel.ResultsData.Any()) return;

                CurrentViewModel.IsBusy = true;
                await CheckLocalData();

                await CheckLocalData();


                if (!CurrentViewModel.ResultsData.Any())
                {
                    await GetLatestData();
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
                    foreach (var item in res)
                    {
                        item.SetLowestPrices();
                    }
                    CurrentViewModel.ResultsData.Replace(res);
                    var lstUpdated = DateTime.Now.ToString("F");
                    var ser = await CurrentViewModel.SaveAnyString(lstUpdated, Constants.LastSavedFileName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }

        public async Task GetLatestData()
        {

            try
            {
                var res = await App.DataTable.ToListAsync();
                Constants.LocalListOfBudgetData.Replace(res);
                if (res == null || !res.Any()) return;
                try
                {
                    foreach (var item in res)
                    {
                        item.SetLowestPrices();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                CurrentViewModel.ResultsData.Replace(res);
                var lstUpdated = DateTime.Now.ToString("F");
                var ser = await CurrentViewModel.SaveAnyString(lstUpdated, Constants.LastSavedFileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        private async void ReloadReportsRefreshPulled(object sender, EventArgs e)
        {

            try
            {
                await GetLatestData();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            ListViewProducts.IsRefreshing = false;
        }

        private async void ShowSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {

            try
            {
                if (!(sender is ListView lv)) return;

                if (!(lv.SelectedItem is BudgetData sel)) return;
                lv.SelectedItem = null;

                await Navigation.PushAsync(new ProductView(sel));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}