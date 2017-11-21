using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Data.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{
		public HomeView ()
		{
			InitializeComponent ();
		    NavigationPage.SetBackButtonTitle(this, "");
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
            

            try
            {
                if (Constants.LocalListOfBudgetData == null)
                {
                    Constants.LocalListOfBudgetData = new List<BudgetData>();
                }
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
                    await Navigation.PushAsync(new CalculateByWeightView() {Title = "Calculate by weight"});
                    break;
                case "View watchlist":
                    await Navigation.PushAsync(new WatchlistView(){Title = "My Watchlist"});
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