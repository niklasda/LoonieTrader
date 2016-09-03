using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace LoonieTrader.App.ViewModels.Converters
{
    public class InstrumentTypeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            IList<InstrumentViewModel> items = new List<InstrumentViewModel>();

            var persons = values[0] as InstrumentViewModel[] ?? new InstrumentViewModel[0];

            foreach (var p in persons)
            {
                items.Add(p);
            }
            return items;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot perform reverse-conversion");
        }
    }
}