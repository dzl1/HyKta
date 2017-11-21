using System.Collections.Generic;
using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.Data.Entities;
using HelpYouBudget.NETStandard.Views;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace HelpYouBudget.NETStandard
{
    public partial class App : Application
    {
        public static MobileServiceClient Client = new MobileServiceClient(Constants.ApplicationUrl);
        
        public static MobileServiceCollection<BudgetData, BudgetData> DataService;

        public static IMobileServiceTable<BudgetData> DataTable = Client.GetTable<BudgetData>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomeView() {Title = "HELP YOU"});
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
