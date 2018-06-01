using System;
using Xamarin.Forms;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using ShsotkaInfoV3.Annotations;
using ShsotkaInfoV3.Services;
using ShsotkaInfoV3.ViewModels;
using WordPressPCL.Models;

//All these converters create a single instance that can be reused for all bindings and avoids the need to create a dedicated Resource in XAML
namespace ShsotkaInfoV3
{
    public class NullIntValueConverter : IValueConverter
    {
        public static NullIntValueConverter Instance = new NullIntValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is string))
                return null;

            int i;
            if (int.TryParse((string)value, out i))
                return i;

            return null;
        }
    }

    public class InverseBoolConverter : IValueConverter
    {
        public static InverseBoolConverter Instance = new InverseBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    public class IsNotNullToBoolConverter : IValueConverter
    {
        public static IsNotNullToBoolConverter Instance = new IsNotNullToBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }
    }

    public class IsNullToBoolConverter : IValueConverter
    {
        public static IsNullToBoolConverter Instance = new IsNullToBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }
    }

    public class GetFeaturedUrl : IValueConverter, INotifyPropertyChanged
    {
        public static GetFeaturedUrl Instance = new GetFeaturedUrl();
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as Links;
            Image image = new Image();
            string imageUrl = "http://shostka.info/wp-content/themes/pt-shostka/img/headpiece-red.jpg";





            if (item.FeaturedMedia != null)
            {

                if (item.FeaturedMedia.Count() <= 1)
                {
                    return image.Source = imageUrl;
                }
                else
                {
                    if (string.IsNullOrEmpty(item.FeaturedMedia.ToList()[1].Href)) return image.Source = imageUrl;
                    else
                    {
                        return image.Source = item.FeaturedMedia.ToList()[1].Href;
                    }


                }


            }
            else
            {
                return image.Source = imageUrl;
            }








        }


        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class IsEmptyConverter : IValueConverter
    {
        public static IsEmptyConverter Instance = new IsEmptyConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as IList;

            if (list == null)
                return false;

            return list.Count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as IList;

            if (list == null)
                return false;

            return list.Count > 0;
        }
    }
}