using System;
using System.Diagnostics;
using System.Globalization;
using HelpYouBudget.NETStandard.ViewModels;
using Xamarin.Forms;

namespace HelpYouBudget.NETStandard.Data.Converters
{
    public class BgColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = value as bool? ?? false;
            return Color.FromHex(param ? "#450091" : "#4286f4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }
    public class LightOrDarkTextColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = value as bool? ?? false;
            return Color.FromHex(param ? "#ffffff" : "#91a0ba");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }
    public class BudgetDatamainColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = value is FrequencyOfIncomeExpenses ? (FrequencyOfIncomeExpenses) value : FrequencyOfIncomeExpenses.Weekly;

            if (param == FrequencyOfIncomeExpenses.Weekly)
            {
                return Color.FromHex("#ffcc66");
            }

            if (param == FrequencyOfIncomeExpenses.Fortnightly)
            {
                return Color.FromHex("#cc9900");
            }

            if (param == FrequencyOfIncomeExpenses.Monthly)
            {
                return Color.FromHex("#ff9933");
            }

            //default return;
                return Color.FromHex("#ffcccc");
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }

    public class TextBudgetColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            double.TryParse(value.ToString(), out var param);


            return Color.FromHex(param > 0 ? "#000000" : "#ff0000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }

  
    public class BgListViewColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = value as bool? ?? false;
            return Color.FromHex(param ? "#339933" : "#dd99ff");
            //#4d4d4d
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }

    public class CellStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                if (value == null || !string.IsNullOrEmpty(value.ToString()) && value.ToString().ToLower() == "\n")
                {
                    return Color.Transparent;
                }

                var val = int.TryParse(value.ToString(), out var inte);

                if (!string.IsNullOrEmpty(value.ToString())  && inte >= 3)
                    return Color.Red;


                if (!string.IsNullOrEmpty(value.ToString()) && inte == 2)
                    return Color.Orange;


                if (!string.IsNullOrEmpty(value.ToString()) && inte == 1)
                    return Color.Yellow;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class ButtonColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = value as bool? ?? false;


            if (parameter + "" == "invert" && value is bool)
            {
                param = !param;
            }

            return Color.FromHex(param ? "#96d1ff" : "#4d4d4d");
            //#4d4d4d
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }

    public class ButtonTextColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = value as bool? ?? false;


            if (parameter + "" == "invert" && value is bool)
            {
                param = !param;
            }

            return param? Color.FromHex("#00717a"): Color.White;
            //#4d4d4d
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //{StaticResource ListViewTextColor}
    }
}