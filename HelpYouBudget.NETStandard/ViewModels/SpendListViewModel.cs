using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public class SpendObject : NotifyProp
    {

        private bool isPermanent;

        public bool IsPermanent
        {
            get => isPermanent;
            set
            {
                if (IsPermanent == value) return;
                isPermanent = value;
                RaisePropertyChanged();
            }
        }



        private string description;

        public string Description
        {
            get => description;
            set
            {
                if (Description == value) return;
                description = value;
                RaisePropertyChanged();
            }
        }

        private string productPrice;

        public string ProductPrice
        {
            get => productPrice;
            set
            {
                if (ProductPrice == value) return;
                productPrice = value;
                RaisePropertyChanged();
            }
        }

        private string productName;

        public string ProductName
        {
            get => productName;
            set
            {
                if (ProductName == value) return;
                productName = value;
                RaisePropertyChanged();
            }
        }

    }

    public class BudgetDataMain : NotifyProp
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (Name == value) return;
                name = value;
                RaisePropertyChanged();
            }
        }
        private double amount;

        public double Amount
        {
            get => amount;
            set
            {
                if (Amount == value) return;
                amount = value;
                RaisePropertyChanged();
            }
        }

        private FrequencyOfIncomeExpenses frequency;

        public FrequencyOfIncomeExpenses Frequency
        {
            get => frequency;
            set
            {
                if (Frequency == value) return;
                frequency = value;
                IncomeOrExpenseString = value.ToString();
                RaisePropertyChanged();
            }
        }

        
        private bool incomeOrExpense;

        public bool IncomeOrExpense
        {
            get => incomeOrExpense;
            set
            {
                if (IncomeOrExpense == value) return;
                incomeOrExpense = value;
                RaisePropertyChanged();
            }
        }

        private string incomeOrExpenseString;

        public string IncomeOrExpenseString
        {
            get
            {
                if (string.IsNullOrEmpty(incomeOrExpenseString)) return Frequency.ToString();

                return incomeOrExpenseString;
            }
            set
            {
                if (IncomeOrExpenseString == value) return;
                incomeOrExpenseString = value;
                RaisePropertyChanged();
            }
        }
        private string description;

        public string Description
        {
            get => description;
            set
            {
                if (Description == value) return;
                description = value;
                RaisePropertyChanged();
            }
        }


    }



    public enum FrequencyOfIncomeExpenses
    {
        Weekly,
        Fortnightly,
        Monthly,
        Annually
    }


    public class SpendListViewModel : BaseViewModel
    {






        private FrequencyOfIncomeExpenses frequencyData;

        public FrequencyOfIncomeExpenses FrequencyData
        {
            get => frequencyData;
            set
            {
                if (FrequencyData == value) return;
                FrequencyDataString = value.ToString();
                frequencyData = value;
                RaisePropertyChanged();
            }
        }


        private string frequencyDataString;

        public string FrequencyDataString
        {
            get => frequencyDataString;
            set
            {
                if (FrequencyDataString == value) return;
                frequencyDataString = value;
                RaisePropertyChanged();
            }
        }



        private bool showFrequency;

        public bool ShowFrequency
        {
            get => showFrequency;
            set
            {
                if (ShowFrequency == value) return;
                showFrequency = value;
                RaisePropertyChanged();
            }
        }


        private double incomeRemaining;

        public double IncomeRemaining
        {
            get => incomeRemaining;
            set
            {
                if (IncomeRemaining == value) return;
                incomeRemaining = value;
                RaisePropertyChanged();
            }
        }
        private bool hasIncome;

        public bool HasIncome
        {
            get => hasIncome;
            set
            {
                if (HasIncome == value) return;
                hasIncome = value;
                RaisePropertyChanged();
            }
        }


        private double income;

        public double Income
        {
            get => income;
            set
            {
                if (Income == value) return;
                income = value;
                RaisePropertyChanged();
            }
        }
        private bool showIncome;

        public bool ShowIncome
        {
            get => showIncome;
            set
            {
                if (ShowIncome == value) return;
                showIncome = value;
                RaisePropertyChanged();
            }
        }

        private bool showAdd;

        public bool ShowAdd
        {
            get => showAdd;
            set
            {
                if (ShowAdd == value) return;
                showAdd = value;
                RaisePropertyChanged();
            }
        }

        private bool showOptions;

        public bool ShowOptions
        {
            get => showOptions;
            set
            {
                if (ShowOptions == value) return;
                showOptions = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<SpendObject> spendData = new ObservableCollection<SpendObject>();

        public ObservableCollection<SpendObject> SpendData
        {
            get => spendData;
            set
            {
                if (SpendData == value) return;
                spendData = value;
                RaisePropertyChanged();
            }
        }

        private SpendObject data;

        public SpendObject Data
        {
            get => data;
            set
            {
                if (Data == value) return;
                data = value;
                RaisePropertyChanged();
            }
        }

        public SpendListViewModel()
        {
            if (Data == null)
            {
                Data = new SpendObject();
            }


        }

        private double totalCost;
        public double TotalCost
        {
            get => totalCost;
            set
            {
                if (TotalCost == value) return;
                totalCost = value;
                RaisePropertyChanged();
            }
        }


        private int totalItems;

        public int TotalItems
        {
            get => totalItems;
            set
            {
                if (TotalItems == value) return;
                totalItems = value;
                RaisePropertyChanged();
            }
        }




    }
}