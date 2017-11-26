using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoreView : ContentPage
	{
		public MoreView ()
		{
			InitializeComponent ();
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

	            case "More...":
	                await Navigation.PushAsync(new MoreView() { Title = "More Options" });
	                break;
	        }
	    }
    }
}