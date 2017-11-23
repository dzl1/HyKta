using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HelpYouBudget.NETStandard.Data;

namespace HelpYouBudget.NETStandard.ViewModels
{
    public static class BudgetDataMainPrimer
    {
        public static List<BudgetDataMain> SetupBudgetDataMain()
        {

            try
            {
                var dataToReturn = new List<BudgetDataMain>
                {
                    new BudgetDataMain()
                    {
                        Name = "Wages or Salary",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = true,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Work and Income payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = true,
                        
                        Description = "(include benefit payments and any extra help such as the Accommodation Supplement, Temporary Additional Support, Disability Allowance, etc). Call them free on 0800 559 009 if you don’t know what your payments are."
                    },
                    new BudgetDataMain()
                    {
                        Name = "IRD Payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = true,
                        
                        Description = "(such as interest or dividends, rent or board payments)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Other Income",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = true,
                        Description = "(interest, dividends, rent/board payments)"
                    },

                    //Weekly Costs
                    new BudgetDataMain()
                    {
                        Name = "Groceries",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = "(include groceries you buy separately from your weekly shopping such as milk and bread)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Rent or board",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Transport fares or petrol",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Spending money ",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = "(such as cigarettes and entertainment)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Child Support payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Weekly debt payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = "(include benefit debts and hire purchases)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Other costs",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Weekly,
                        IncomeOrExpense = false,
                        
                        Description = "(such as donations and children’s pocket money)"
                    },
                    //Monthly Costs
                    
                    new BudgetDataMain()
                    {
                        Name = "Mortgage",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                    },
                    new BudgetDataMain()
                    {
                        Name = "Power",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        IncomeOrExpenseString = FrequencyOfIncomeExpenses.Monthly.ToString()
                    },
                    new BudgetDataMain()
                    {
                        Name = "Gas",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        IncomeOrExpenseString = FrequencyOfIncomeExpenses.Monthly.ToString()
                    },
                    new BudgetDataMain()
                    {
                        Name = "Phone",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = "(include mobile and toll costs)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Insurances",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = "(include vehicle, house, contents, life and medical)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Credit and store card payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Monthly debt payments",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = "(include personal loans and hire purchases)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Bank fees",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = ""
                    },
                    new BudgetDataMain()
                    {
                        Name = "Rental of goods",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Monthly,
                        IncomeOrExpense = false,
                        
                        Description = "(such as computer, TV and washing machine)"
                    },
                    //Annual Costs
                    new BudgetDataMain()
                    {
                        Name = "Rates",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(include water rates if any)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "House Maintenance",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as lawns, repairs and renovations)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Vehicle Costs",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(include registration, WOF, maintenance and repairs)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Fees and subscriptions",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as schools, clubs and magazines)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Medical costs",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as doctors, prescriptions, dentists, opticians)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Pet costs",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as vet fees, dog registrations and catteries)"
                    },
                    new BudgetDataMain()
                    {
                        Name = "Clothes and shoes",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = " "
                    },
                    new BudgetDataMain()
                    {
                        Name = "Household goods",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as kitchen items and bedding) "
                    },
                    new BudgetDataMain()
                    {
                        Name = "Ohter costs",
                        Amount = 0.00,
                        Frequency = FrequencyOfIncomeExpenses.Annually,
                        IncomeOrExpense = false,
                        
                        Description = "(such as holidays and gifts)"
                    },
                };

                return dataToReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<BudgetDataMain>();
        }
    }
    public class OverallBudgetViewModel : BudgetViewModel
    {
        public OverallBudgetViewModel()
        {

            try
            {
                if (CurrentBudgetItem == null)
                {
                    CurrentBudgetItem = new BudgetDataMain();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }

    public class BudgetViewModel : BaseViewModel
    {
         private ObservableCollection<BudgetDataMain> budgetDataEntry = new ObservableCollection<BudgetDataMain>();

        public ObservableCollection<BudgetDataMain> BudgetDataEntry
        {
            get => budgetDataEntry;
            set
            {
                if (BudgetDataEntry == value) return;
                budgetDataEntry = value;
                RaisePropertyChanged();
            }
        }

        private bool showDetails;

        public bool ShowDetails
        {
            get => showDetails;
            set
            {
                if (ShowDetails == value) return;
                showDetails = value;
                RaisePropertyChanged();
            }
        }


        private double totalIncome;

        public double TotalIncome
        {
            get => totalIncome;
            set
            {
                if (TotalIncome == value) return;
                totalIncome = value;
                RaisePropertyChanged();
            }
        }
        private double annualCosts;

        public double AnnualCosts
        {
            get => annualCosts;
            set
            {
                if (AnnualCosts == value) return;
                annualCosts = value;
                RaisePropertyChanged();
            }
        }

        private double monthlyCosts;

        public double MonthlyCosts
        {
            get => monthlyCosts;
            set
            {
                if (MonthlyCosts == value) return;
                monthlyCosts = value;
                RaisePropertyChanged();
            }
        }

        private double weeklyCosts;

        public double WeeklyCosts
        {
            get => weeklyCosts;
            set
            {
                if (WeeklyCosts == value) return;
                weeklyCosts = value;
                RaisePropertyChanged();
            }
        }

        private double fortnightlyCosts;

        public double FortnightlyCosts
        {
            get => fortnightlyCosts;
            set
            {
                if (FortnightlyCosts == value) return;
                fortnightlyCosts = value;
                RaisePropertyChanged();
            }
        }


        private BudgetDataMain currentBudgetItem;

        public BudgetDataMain CurrentBudgetItem
        {
            get => currentBudgetItem;
            set
            {
                if (CurrentBudgetItem == value) return;
                currentBudgetItem = value;
                RaisePropertyChanged();
            }
        }

        private string breakDownText;

        public string BreakDownText
        {
            get
            {
                if (string.IsNullOrEmpty(breakDownText))
                {
                    return "(per Week)";
                }
                return breakDownText;
            }
            set
            {
                if (BreakDownText == value) return;
                breakDownText = value;
                RaisePropertyChanged();
            }
        }



      

        public void SetDefaultBudgetDataMain()
        {
            if (!BudgetDataEntry.Any())
            {
                BudgetDataEntry.Replace(BudgetDataMainPrimer.SetupBudgetDataMain());
            }
        }
    }
}