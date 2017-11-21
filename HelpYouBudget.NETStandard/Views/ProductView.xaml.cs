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
    public partial class ProductView : ContentPage
    {
        public ProductViewModel CurrentViewModel { get; set; }
        public ProductView(BudgetData data = null)
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new ProductViewModel();
            }
            if (data != null)
            {
                CurrentViewModel.Data = data;
            }
            BindingContext = CurrentViewModel;
            InitializeComponent();
            CurrentViewModel.IsBusy = true;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                CurrentViewModel.IsBusy = true;
                await LoadWatchListToCheckForItem();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private async Task LoadWatchListToCheckForItem()
        {

            try
            {
                var res = await CurrentViewModel.GetSavedWatchlist();
                if (res != null && res.Any())
                {
                    CurrentViewModel.WatchListItems.Replace(res);
                }

                var foundData = (from e in CurrentViewModel.WatchListItems
                                 where e.Id == CurrentViewModel.Data.Id
                                 select e).FirstOrDefault();

                if (foundData != null)
                {
                    CurrentViewModel.Data.IsFavourite = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void AddProductToWatchlist(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.Data.IsFavourite = true;
                CurrentViewModel.WatchListItems.Add(CurrentViewModel.Data);

                //serialize and save
                var data = CurrentViewModel.SerializeDataBase(CurrentViewModel.WatchListItems);
                await CurrentViewModel.SaveAnyString(data, Constants.WatchListFileName);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;

        }

        private async void RemoveProductFromWatchlist(object sender, EventArgs e)
        {

            try
            {
                CurrentViewModel.Data.IsFavourite = false;
                var itemToRemove = CurrentViewModel.WatchListItems.Single(tr => tr.Id == CurrentViewModel.Data.Id);
                if (itemToRemove != null)
                {
                    CurrentViewModel.WatchListItems.Remove(itemToRemove);
                }

                //serialize and save
                var data = CurrentViewModel.SerializeDataBase(CurrentViewModel.WatchListItems);
                await CurrentViewModel.SaveAnyString(data, Constants.WatchListFileName);




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;


        }
    }
}