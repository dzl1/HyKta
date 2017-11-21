using HelpYouBudget.NETStandard.Data;
using HelpYouBudget.NETStandard.ViewModels;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpYouBudget.NETStandard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculateByWeightView
    {

        public const string ProdCalc = "Product Calculator";
        public const string BulkCalc = "Bulk Buy Calculator";
        public CalculateByWeightViewModel CurrentViewModel { get; set; }
        public CalculateByWeightView()
        {
            if (CurrentViewModel == null)
            {
                CurrentViewModel = new CalculateByWeightViewModel();
            }
            //check for the calculator


            IsLoading = true;
            BindingContext = CurrentViewModel;
            InitializeComponent();
            CurrentViewModel.IsBusy = true;
        }

        private bool IsLoading { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                IsLoading = false;
                CurrentViewModel.IsBusy = true;
                var data = await CurrentViewModel.RetrieveFile<string>(Constants.TypeOfCalculator, false);
                switch (data)
                {
                    case ProdCalc:
                        WeightTypePicker.SelectedIndex = 0;
                        CurrentViewModel.ProductCalculator = true;
                        CurrentViewModel.BulkCalculator = false;
                        break;
                    case BulkCalc:
                        WeightTypePicker.SelectedIndex = 1;
                        CurrentViewModel.ProductCalculator = false;
                        CurrentViewModel.BulkCalculator = true;
                        break;
                }


                var pickerOne = await CurrentViewModel.RetrieveFile<string>(Constants.ProductOneVolumePicker, false);
                var pickerTwo = await CurrentViewModel.RetrieveFile<string>(Constants.ProductTwoVolumePicker, false);
                var pickerBulk = await CurrentViewModel.RetrieveFile<string>(Constants.ProductBulkVolumePicker, false);

                var showTwoProducts =
                    await CurrentViewModel.RetrieveFile<string>(Constants.CompareTwoProductsFileName, false);

                bool.TryParse(showTwoProducts, out var showProducts);
                ShowTwoProductsSwitch.IsToggled = showProducts;

                var count = 0;
                //set last saved picker for item one
                if (string.IsNullOrEmpty(pickerOne))
                {
                    WeightPicker.SelectedIndex = 0;
                }
                else
                {
                    foreach (var item in WeightPicker.Items)
                    {
                        if (item == pickerOne)
                        {
                            WeightPicker.SelectedIndex = count;
                            break;
                        }
                        count++;
                    }
                }



                //set last saved picker for item two
                count = 0;
                if (string.IsNullOrEmpty(pickerTwo))
                {
                    WeightPickerTwo.SelectedIndex = count;
                }
                else
                {
                    foreach (var item in WeightPickerTwo.Items)
                    {
                        if (item == pickerTwo)
                        {
                            WeightPickerTwo.SelectedIndex = count;
                            break;
                        }
                        count++;
                    }

                }


                //set last saved item for picker bulk
                count = 0;
                if (string.IsNullOrEmpty(pickerBulk))
                {
                    WeightBulkPicker.SelectedIndex = 0;
                }
                else
                {
                    foreach (var item in WeightBulkPicker.Items)
                    {
                        if (item == pickerBulk)
                        {
                            WeightBulkPicker.SelectedIndex = count;
                            break;
                        }
                        count++;
                    }
                }

                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        WeightBulkPicker.TextColor =
                            WeightBulkPicker.SelectedIndex == 0 ? Color.FromHex("#C0C0C0") : Color.Black;
                        WeightPickerTwo.TextColor =
                            WeightPickerTwo.SelectedIndex == 0 ? Color.FromHex("#C0C0C0") : Color.Black;
                        WeightPicker.TextColor = WeightPicker.SelectedIndex == 0 ? Color.FromHex("#C0C0C0") : Color.Black;
                        break;
                    case Device.iOS:
                        WeightBulkPicker.TextColor = WeightBulkPicker.SelectedIndex == 0 ? Color.FromHex("b3b3b3") : Color.Black;
                        WeightPickerTwo.TextColor = WeightPickerTwo.SelectedIndex == 0 ? Color.FromHex("b3b3b3") : Color.Black;
                        WeightPicker.TextColor = WeightPicker.SelectedIndex == 0 ? Color.FromHex("b3b3b3") : Color.Black;
                        break;
                }

                if (!CurrentViewModel.BulkCalculator && !CurrentViewModel.ProductCalculator)
                {
                    CurrentViewModel.ProductCalculator = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            CurrentViewModel.IsBusy = false;
        }

        private async void PickerChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is Picker picker)) return;

                //don't want to load the first item on initialization - let the onappearing figure it out, otherwise select default - 0
                if (IsLoading) return;

                if (Device.RuntimePlatform == Device.Android)
                    picker.TextColor = picker.SelectedIndex == 0 ? Color.FromHex("#C0C0C0") : Color.Black;
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    picker.TextColor = picker.SelectedIndex == 0 ? Color.FromHex("b3b3b3") : Color.Black;
                }


                switch (picker.StyleId)
                {
                    case "WeightPickerStyle":
                        if (picker.SelectedIndex != 0)
                        {
                            CurrentViewModel.MyData = HelperShuffle.ParseEnum<WeightType>(picker.Items[picker.SelectedIndex]);
                        }
                        await CurrentViewModel.SaveAnyString(picker.Items[picker.SelectedIndex],
                            Constants.ProductOneVolumePicker);
                        break;
                    case "WeightBulkPicker":
                        if (picker.SelectedIndex != 0)
                        {
                            CurrentViewModel.BulkData = HelperShuffle.ParseEnum<WeightType>(picker.Items[picker.SelectedIndex]);
                        }
                        await CurrentViewModel.SaveAnyString(picker.Items[picker.SelectedIndex],
                            Constants.ProductBulkVolumePicker);
                        break;
                    case "WeightPickerTwoStyle":
                        if (picker.SelectedIndex != 0)
                        {
                            CurrentViewModel.MyDataTwo = HelperShuffle.ParseEnum<WeightType>(picker.Items[picker.SelectedIndex]);
                        }
                        await CurrentViewModel.SaveAnyString(picker.Items[picker.SelectedIndex],
                            Constants.ProductTwoVolumePicker);
                        break;
                        await CurrentViewModel.SaveAnyString(picker.Items[picker.SelectedIndex],
                            Constants.ProductOneVolumePicker);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        private async void ShowHideTwoProducts(object sender, ToggledEventArgs e)
        {
            if (!(sender is Switch sw)) return;
            var show = sw.IsToggled;



            if (show)
            {
                GridSecondItem.IsVisible = true;
                GridSecondItem.Opacity = 0;
                await GridSecondItem.FadeTo(1, 2000);
            }
            else
            {
                GridSecondItem.IsVisible = false;
                GridSecondItem.Opacity = 1;
                await GridSecondItem.FadeTo(0, 4000);
            }

            await CurrentViewModel.SaveAnyString(sw.IsToggled.ToString(), Constants.CompareTwoProductsFileName);

        }

        private void CalculateSecondItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentViewModel.Price))
            {
                double pri;
                double.TryParse(CurrentViewModel.Price, out pri);


                double vol;
                double.TryParse(CurrentViewModel.Volume, out vol);


                if (pri > 0 && vol > 0)
                {
                    //calculate
                    var res = vol / pri;


                    Debug.WriteLine($"result: {res}");
                    var txt = $"Price per {CurrentViewModel.MyData} : {res}\nPrice per 10{CurrentViewModel.MyData} : {res * 10}";
                    CurrentViewModel.Stats = txt;
                }
            }
        }

        private async void CalculateFirstItem(object sender, EventArgs e)
        {

            if (CurrentViewModel.BulkCalculator)
            {
                if (!string.IsNullOrEmpty(CurrentViewModel.BulkPrice) &&
                    !string.IsNullOrEmpty(CurrentViewModel.BulkWeight) && CurrentViewModel.BulkData.ToString() != "")
                {
                    double.TryParse(CurrentViewModel.BulkPrice, out var pri);
                    double.TryParse(CurrentViewModel.BulkWeight, out var vol);
                    double.TryParse(CurrentViewModel.PerBulkWeight, out var perWeight);


                    var dt = perWeight <= 1 ? CurrentViewModel.BulkData.ToString().Trim('s') : CurrentViewModel.BulkData + "";
                    var orig = dt;
                    if (vol < perWeight)
                    {
                        var output = string.Empty;
                        switch (CurrentViewModel.BulkData)
                        {
                            case WeightType.Grams:
                                output = WeightType.Kilograms.ToString();
                                break;
                            case WeightType.Millilitres:
                                output = WeightType.Litres.ToString();
                                break;
                        }

                        if (!string.IsNullOrEmpty(output))
                        {
                            dt = output;
                        }


                        var recalc = await DisplayAlert("Continue?", $"Did you mean {vol} {output}?", "Yes", "No");
                        if (recalc)
                        {
                            vol = vol * 1000;
                        }
                        else
                        {
                            dt = orig;
                        }
                    }





                    if (!(pri > 0) || !(vol > 0) || !(perWeight > 0)) return;
                    //calculate
                    var res = (vol / perWeight) * pri;
                    var totalPrice = HelperShuffle.SignificantTruncate(res, 2).ToString(CultureInfo.InvariantCulture);
                    var pricePerThousand = HelperShuffle.SignificantTruncate((pri / perWeight * 1000), 2).ToString(CultureInfo.InvariantCulture);
                    Debug.WriteLine($"result: {res}");
                    var txt = $"Price per {CurrentViewModel.PerBulkWeight} {CurrentViewModel.BulkData} : ${CurrentViewModel.BulkPrice}\nTotal {dt} : {CurrentViewModel.BulkWeight}\nTotal Price : ${totalPrice}\nPrice per 1000 {CurrentViewModel.BulkData} : ${pricePerThousand}";
                    CurrentViewModel.Stats = txt;
                }
                else
                {
                    await DisplayAlert("Missing Information", "Please check all the required information is filled in", "OK");
                    return;
                }


                return;
            }
            
            if ((CurrentViewModel.MyData != CurrentViewModel.MyDataTwo) && CurrentViewModel.CompareTwoProducts)
            {
                var response = await DisplayAlert("Continue", "You are about to compare two different measurements. Continue?", "OK", "Cancel");

                if (!response) return;
            }




            if (!CurrentViewModel.CompareTwoProducts)
            {
                if (string.IsNullOrEmpty(CurrentViewModel.Price))
                {
                    await DisplayAlert("Missing Information", "Please check all the required information is filled in", "OK");
                    return;
                };
                double.TryParse(CurrentViewModel.Price, out var pri);


                double.TryParse(CurrentViewModel.Volume, out var vol);


                if (!(pri > 0) || !(vol > 0)) return;
                //calculate
                var res = pri / vol;


                //var totalPrice = HelperShuffle.SignificantTruncate(res, 2).ToString(CultureInfo.InvariantCulture);
                //var perHundred = HelperShuffle.SignificantTruncate((res * 100), 2).ToString(CultureInfo.InvariantCulture);

                Debug.WriteLine($"result: {res}");
                var txt = $"Price per {CurrentViewModel.MyData.ToString().TrimEnd('s')} : ${HelperShuffle.SignificantTruncate(res, 2).ToString(CultureInfo.InvariantCulture)}\nPrice per 10 {CurrentViewModel.MyData} : ${HelperShuffle.SignificantTruncate((res * 10), 2).ToString(CultureInfo.InvariantCulture)}\nPrice per 100 {CurrentViewModel.MyData} : ${HelperShuffle.SignificantTruncate((res * 100), 2).ToString(CultureInfo.InvariantCulture)}";
                CurrentViewModel.Stats = txt;
            }
            else
            {
                //Calculate Two Prices
                if (string.IsNullOrEmpty(CurrentViewModel.Price))
                {
                    await DisplayAlert("Missing Information", "Please check all the required information is filled in", "OK");
                    return;
                }; ;
                double.TryParse(CurrentViewModel.Price, out var pri);
                double.TryParse(CurrentViewModel.Volume, out var vol);
                if (!(pri > 0) || !(vol > 0)) return;
                //calculate
                var res = pri / vol;



                if (string.IsNullOrEmpty(CurrentViewModel.PriceTwo))
                {
                    await DisplayAlert("Missing Information", "Please check all the required information is filled in", "OK");
                    return;
                }; ;
                double.TryParse(CurrentViewModel.PriceTwo, out var prit);
                double.TryParse(CurrentViewModel.VolumeTwo, out var volt);
                if (!(pri > 0) || !(vol > 0))
                {
                    await DisplayAlert("Missing Information", "Please check all the required information is filled in", "OK");
                    return;
                }; ;
                //calculate
                var rest = prit / volt;
                //var trm = CurrentViewModel.MyData.ToString().TrimEnd('s');
                Debug.WriteLine($"result: {res}");
                var txt = $"First Item:\nPrice per {CurrentViewModel.MyData.ToString().TrimEnd('s')} : ${Math.Round(res, 4)}\nPrice per 10 {CurrentViewModel.MyData} : ${Math.Round(res * 10, 4)}\nPrice per 100 {CurrentViewModel.MyData} : ${Math.Round(res * 100, 4)}\n\n" +
                          $"Second Item:\nPrice per { CurrentViewModel.MyDataTwo.ToString().TrimEnd('s')} : ${ Math.Round(rest, 4)}\nPrice per 10 { CurrentViewModel.MyDataTwo} : ${ Math.Round(rest * 10, 4).ToString("#.00", CultureInfo.InvariantCulture)}\nPrice per 100 {CurrentViewModel.MyDataTwo} : ${Math.Round(rest * 100, 4).ToString("#.00", CultureInfo.InvariantCulture)}";
                CurrentViewModel.Stats = txt;
            }
        }

        private void ClearAndResetItems(object sender, EventArgs e)
        {
            WeightPicker.SelectedIndex = 0;
            WeightPickerTwo.SelectedIndex = 0;
            CurrentViewModel.Price = string.Empty;
            CurrentViewModel.PriceTwo = string.Empty;
            CurrentViewModel.Volume = string.Empty;
            CurrentViewModel.VolumeTwo = string.Empty;
            CurrentViewModel.CompareTwoProducts = false;
            CurrentViewModel.Stats = string.Empty;


        }
        private void PickerTypeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is Picker picker)) return;
                if (IsLoading) return;

                switch (picker.Items[picker.SelectedIndex])
                {
                    case ProdCalc:
                        CurrentViewModel.BulkCalculator = false;
                        CurrentViewModel.ProductCalculator = true;
                        SaveTypeOfCalculator("Product Calculator");
                        return;
                    case BulkCalc:
                        CurrentViewModel.ProductCalculator = false;
                        CurrentViewModel.BulkCalculator = true;
                        SaveTypeOfCalculator("Bulk Buy Calculator");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void SaveTypeOfCalculator(string bulkBuyCalculator)
        {

            try
            {
                await CurrentViewModel.SaveAnyString(bulkBuyCalculator, Constants.TypeOfCalculator);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }

    public class PickerModified : Picker
    {
        public PickerModified()
        {
            this.HeightRequest = 40;
        }
    }
}