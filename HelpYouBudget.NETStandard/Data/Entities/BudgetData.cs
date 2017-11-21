using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using HelpYouBudget.NETStandard.ViewModels;

namespace HelpYouBudget.NETStandard.Data.Entities
{
    public class BudgetData : NotifyProp
    {
        private string id;

        public string Id
        {
            get => id;
            set
            {
                if (Id == value) return;
                id = value;
                RaisePropertyChanged();
            }
        }


        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ClientNumber { get; set; }
        public string DateOfIntrest { get; set; }
        public string HasSubmitted { get; set; }
        public string OtherClientDetails { get; set; }
        public int Position { get; set; }
        public int ScoreForSorting { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string StoreOne { get; set; }
        public string StoreOnePrice { get; set; }
        public string StoreTwo { get; set; }
        public string StoreTwoPrice { get; set; }
        public string StoreThree { get; set; }
        public string StoreThreePrice { get; set; }
        public string StoreFour { get; set; }
        public string StoreFourPrice { get; set; }
        public string StoreOneWeight { get; set; }
        public string StoreTwoWeight { get; set; }
        public string StoreThreeWeight { get; set; }
        public string StoreFourWeight { get; set; }
        public string StoreOneQuantity { get; set; }
        public string StoreTwoQuantity { get; set; }
        public string StoreThreeQuantity { get; set; }
        public string StoreFourQuantity { get; set; }
        public bool IsOwnItem { get; set; }
        public string StoreOneDescription { get; set; }
        public string StoreTwoDescription { get; set; }
        public string StoreThreeDescription { get; set; }
        public string StoreFourDescription { get; set; }
        public string ShowStoreOne { get; set; }
        public string ShowStoreTwo { get; set; }
        public string ShowStoreThree { get; set; }
        public string ShowStoreFour { get; set; }
        public string DateOfSale { get; set; }
        public string DateOfExpiry { get; set; }
        public string MiscDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public bool IsInAllStates { get; set; }
        public string StatesProductAvailable { get; set; }
        public bool FlagForFutureUse { get; set; }
        public bool FlagOneForFutureUse { get; set; }
        public bool IsProductExactlyTheSame { get; set; }
        public bool HasExpired { get; set; }
        public bool CanRemoveFromTable { get; set; }
        public string WeightRange { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDescriptionData { get; set; }
        public string CheckedInStore { get; set; }
        public string LastChecked { get; set; }
        public string UpdateChecked { get; set; }
        public bool IsValid { get; set; }
        public bool IsFeature { get; set; }
        [IgnoreDataMember]
        private bool isFavourite;

        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                if (IsFavourite == value) return;
                isFavourite = value;
                RaisePropertyChanged();
            }
        }

        public string CustomerReview { get; set; }
        public string StoreOneUrl { get; set; }
        public string StoreTwoUrl { get; set; }
        public string StoreThreeUrl { get; set; }
        public string StoreFourUrl { get; set; }
        public bool IsCache { get; set; }

        [IgnoreDataMember]
        private string lowestStore;

        [IgnoreDataMember]
        public string LowestStore
        {
            get => lowestStore;
            set
            {
                if (LowestStore == value) return;
                lowestStore = value;
                RaisePropertyChanged();
            }
        }


        [IgnoreDataMember]
        private string lowestPrice;

        [IgnoreDataMember]
        public string LowestPrice
        {
            get => lowestPrice;
            set
            {
                if (LowestPrice == value) return;
                lowestPrice = value;
                RaisePropertyChanged();
            }
        }

        public void SetLowestPrices()
        {

            try
            {
                double.TryParse(StoreOnePrice, out var strOne);
                double.TryParse(StoreTwoPrice, out var strTwo);
                double.TryParse(StoreThreePrice, out var strThree);
                double.TryParse(StoreFourPrice, out var strFour);
                var lst = new List<double>();
                if (strOne > 0) lst.Add(strOne);
                if (strTwo > 0) lst.Add(strTwo);
                if (strThree > 0) lst.Add(strThree);
                if (strFour > 0) lst.Add(strFour);

                LowestPrice = lst.Min().ToString();

                if (!string.IsNullOrEmpty(StoreOnePrice) && LowestPrice == StoreOnePrice)
                {
                    LowestStore = StoreOne;
                }

                if (!string.IsNullOrEmpty(StoreTwoPrice) && LowestPrice == StoreTwoPrice)
                {
                    LowestStore = StoreTwo;
                }

                if (!string.IsNullOrEmpty(StoreThreePrice) && LowestPrice == StoreThreePrice)
                {
                    LowestStore = StoreThree;
                }

                if (!string.IsNullOrEmpty(StoreFourPrice) && LowestPrice == StoreFourPrice)
                {
                    LowestStore = StoreFour;
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}