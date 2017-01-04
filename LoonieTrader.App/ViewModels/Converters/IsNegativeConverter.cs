using System;
using System.Globalization;
using System.Windows.Data;
using Xceed.Wpf.DataGrid;

namespace LoonieTrader.App.ViewModels.Converters
{
    public class IsNegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dataCell = value as DataCell;
            if (dataCell!= null)
            {
                if (dataCell.Content is decimal)
                {
                    return (decimal)dataCell.Content < 0m;
                }

                decimal dValue;
                if (decimal.TryParse(dataCell.Content.ToString(), out dValue))
                {
                    return dValue < 0.0m;
                    // return (double)value < 0.5d;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}