using System;
using System.Collections.Generic;
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
    public partial class WatchlistView : ContentPage
    {
        public WatchlistViewModel CurrentViewModel { get; set; }
        public WatchlistView()
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new WatchlistViewModel();

            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                CurrentViewModel.IsBusy = true;
                await GetWatchlistFromFile();
                //await CurrentViewModel.CheckLastUpdated();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;

        }







        public async Task GetWatchlistFromFile()
        {

            try
            {

                var res = await CurrentViewModel.GetSavedWatchlist();
                if (res != null && res.Any())
                {

                    var data = new List<BudgetData>();

                    if (!Constants.LocalListOfBudgetData.Any())
                    {
                        //GetData from azure first
                        data = await App.DataTable.ToListAsync();
                        Constants.LocalListOfBudgetData.Replace(data);
                    }
                    else
                    {
                        data.Replace(Constants.LocalListOfBudgetData);
                    }
                    var matches = new List<BudgetData>();
                    if (data != null && data.Any())
                    {
                        //match up with our saved list and update the prices
                        foreach (var item in data)
                        {
                            var mat = (from m in res
                                       where m.Id == item.Id
                                       select m).FirstOrDefault();


                            if (mat == null) continue;
                            item.IsFavourite = true;
                            matches.Add(item);
                        }
                    }


                    if (matches.Any())
                    {
                        foreach (var item in matches)
                        {
                            item.SetLowestPrices();
                        }
                    }


                    CurrentViewModel.ResultsData.Replace(matches);
                    var lstUpdated = DateTime.Now.ToString("F");
                    var ser = await CurrentViewModel.SaveAnyString(lstUpdated, Constants.LastSavedFileName);
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

                if (!(lv.SelectedItem is BudgetData sel)) return;
                lv.SelectedItem = null;

                await Navigation.PushAsync(new ProductView(sel));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void ReloadReportsRefreshPulled(object sender, EventArgs e)
        {
            ListViewProducts.IsRefreshing = false;
        }
    }
}