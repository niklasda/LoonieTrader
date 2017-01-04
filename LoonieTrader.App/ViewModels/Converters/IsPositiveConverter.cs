using System;
using System.Globalization;
using System.Windows.Data;
using Xceed.Wpf.DataGrid;

namespace LoonieTrader.App.ViewModels.Converters
{
    public class IsPositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dataCell = value as DataCell;
            return (dataCell?.Content as decimal?) > 0m;

            //decimal dValue;
            //if (decimal.TryParse(dataCell.Content.ToString(), out dValue))
            //{
            //    return dValue > 0.0m;
            //    // return (double)value < 0.5d;
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}