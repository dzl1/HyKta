using System.Collections.ObjectModel;
using HelpYouBudget.NETStandard.Data.Entities;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class WatchlistViewModel : BaseViewModel
    {
        private ObservableCollection<BudgetData> resultsData = new ObservableCollection<BudgetData>();

        public ObservableCollection<BudgetData> ResultsData
        {
            get => resultsData;
            set
            {
                if (ResultsData == value) return;
                resultsData = value;
                RaisePropertyChanged();
            }
        }
    }
}