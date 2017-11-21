//using Babyfied.NETStandard.Data.Entities;

using System.Collections.Generic;
using HelpYouBudget.NETStandard.Data.Entities;

namespace HelpYouBudget.NETStandard.Data
{
    public class Constants
    {
        //public static FacebookData UserDetails { get; set; }
        //http://devmsduser.azurewebsites.net
        public static string ApplicationUrl = @"https://helpyoubudget.azurewebsites.net";
        public static bool HasBeenAccessed { get; set; }
        public static bool ShouldSave { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static bool UserSkippedLogin { get; set; }
        public const string LastSavedFileName = "LastSaved.txt";

        public const string SessionSavedFile = "SessionSavedFile.txt";

        public const string SaveLoginInfoFile = "ClientInfo.txt";

        public const string SessionCookieFile = "SessionCookie.txt";

        public const string PersonalLoginJsonFile = "JsonInfo.txt";
        
        public const string WatchListFileName = "WatchListFile.txt";

        public const string AllProductsFileName = "AllProducts.txt";

        public static List<BudgetData> LocalListOfBudgetData { get; set; }
        public const string BudgetDataFileName = "BudgetDataFileName.txt";

        public const string FrequencyType = "FrequencyOfIncomeExpenses.txt";

        public const string IncomeFileName = "IncomeFile.txt";

        public const string CurrentSpendList = "SpendList.txt";

        public const string CompareTwoProductsFileName = "CompareTwoProducts.txt";

        public const string TypeOfCalculator = "Calculator.txt";
        public const string ProductOneVolumePicker = "ProductOneVolumePicker.txt";
        public const string ProductTwoVolumePicker = "ProductTwoVolumePicker.txt";
        public const string ProductBulkVolumePicker = "ProductBulkVolumePicker.txt";
    }

    public class LocalMeasurements
    {
        public const double Mililitres = 1;

        public static double Litre = Mililitres * 1000;

        public const double Gram = 1;

        public static double Kilograms => Gram * 1000;

        public static double Tonne => Kilograms * 1000;

        public const double Ounce = 1;

        public static double Pound => Ounce * 16;

        public static double Stone => Pound * 14;

        public const double PoundToKilograms = 0.4536;
        public const double OuncesToKilograms = 0.0283;

        public const double TonneToKilograms = 1000;
        
    }
}
