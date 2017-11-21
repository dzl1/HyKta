using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class SaleCalculatorView : ContentPage
	{
        public SaleCalculatorViewModel CurrentViewModel { get; set; }
		public SaleCalculatorView ()
		{
		    if (CurrentViewModel == null)
		    {
		        CurrentViewModel = new SaleCalculatorViewModel();
		    }
		    BindingContext = CurrentViewModel;
			InitializeComponent ();
		}

	    private async void CalculateClicked(object sender, EventArgs e)
	    {

	        if (CurrentViewModel.Price <= 0)
	        {
	            await DisplayAlert("Price is too low", "Please enter in a higher price", "OK");
	            CurrentViewModel.EnableAddButton = false;
                return;
	        }
            if (CurrentViewModel.Percentage >= 100)
	        {
	            await DisplayAlert("Percentage is too high", "Please enter in a percentage lower than 100%", "OK");
	            CurrentViewModel.EnableAddButton = false;
                return;
	        }
	        if (CurrentViewModel.Percentage <= 0)
	        {
	            await DisplayAlert("Percentage is too low", "Please enter in a percentage higher than zero", "OK");
	            CurrentViewModel.EnableAddButton = false;
                return;
	        }
	        CurrentViewModel.EnableAddButton = true;
	        double percent = (CurrentViewModel.Price * CurrentViewModel.Percentage) / 100;

	        CurrentViewModel.DiscountedPrice = CurrentViewModel.Price - percent;


	    }

	    private async void AddItemToSpendList(object sender, EventArgs e)
	    {

            try
            {
                if (string.IsNullOrEmpty(CurrentViewModel.SpendData.ProductName) || CurrentViewModel.Price <= 0 ||
                    CurrentViewModel.DiscountedPrice <= 0)
                {
                    await DisplayAlert("Missing Information", "Please make sure there is sufficient information to add",
                        "OK");
                    return;
                }


                var result = await DisplayAlert("Confirm Add", "Confirm to add to your spend list", "OK", "Cancel");
                if (!result) return;

                await CurrentViewModel.AddItemToSpendList();

                await Navigation.PushAsync(new SpendListView() { Title = "Spending" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

	    private void CancelAddClicked(object sender, EventArgs e)
	    {
	        CurrentViewModel.ShowAdd = false;
	    }

	    private void ShowAddClicked(object sender, EventArgs e)
	    {
	        if (CurrentViewModel.DiscountedPrice <= 0)
	        {
	            CalculateClicked(sender, e);
	        }
	        CurrentViewModel.ShowAdd = true;
	    }
	}
}