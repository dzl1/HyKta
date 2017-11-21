using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace HelpYouBudget.NETStandard.Data.Converters
{
    public class EmptyStringToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            try
            {
                var val = value?.ToString();



                if (parameter + "" == "invert")
                {
                    return string.IsNullOrEmpty(val);
                }


                return !string.IsNullOrEmpty(val);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EmptyStringToEnabledConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            try
            {
                var val = value?.ToString();
                return !string.IsNullOrEmpty(val);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




}