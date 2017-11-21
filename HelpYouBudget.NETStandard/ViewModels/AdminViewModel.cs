using System.Collections.ObjectModel;
using HelpYouBudget.NETStandard.Data.Entities;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class AdminViewModel : BaseViewModel
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




        private BudgetData newData;

        public BudgetData NewData
        {
            get => newData;
            set
            {
                if (NewData == value) return;
                newData = value;
                RaisePropertyChanged();
            }
        }

        public AdminViewModel()
        {
            if (NewData == null)
            {
                NewData = new BudgetData();
            }
        }

    }
}