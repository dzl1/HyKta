using System;
using Xamarin.Forms;

namespace Babyfied.NETStandard.Data.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (string.IsNullOrEmpty(parameter + ""))
            {
                return value;
            }
            if (parameter + "" == "invert" && value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DefaultVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var str = parameter.ToString();
            var valStr = value.ToString();


            return str == valStr;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    //public class BitmapImageConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is string)
    //        {
    //            var img = new Image() {Source = new UriImageSource()
    //            {

    //            } };
    //        }


    //        if (value is Uri)
    //            return new BitmapImage((Uri)value);

    //        throw new NotSupportedException();
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotSupportedException();
    //    }
    //}
}
