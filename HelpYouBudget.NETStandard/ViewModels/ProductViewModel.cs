using System.Collections.Generic;
using HelpYouBudget.NETStandard.Data.Entities;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {

        private List<BudgetData> watchListItems = new List<BudgetData>();

        public List<BudgetData> WatchListItems
        {
            get => watchListItems;
            set
            {
                if (WatchListItems == value) return;
                watchListItems = value;
                RaisePropertyChanged();
            }
        }




        private BudgetData data;

        public BudgetData Data
        {
            get => data;
            set
            {
                if (Data == value) return;
                data = value;
                RaisePropertyChanged();
            }
        }

        public ProductViewModel()
        {
            if (Data == null)
            {
                Data = new BudgetData();
            }
        }
    }
}