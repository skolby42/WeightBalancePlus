using OxyPlot;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WeightBalancePlus.Converters
{
    public class LineStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LineStyle lineStyle)
            {
                var dashArray = lineStyle.GetDashArray();
                return dashArray == null ? new DoubleCollection() : new DoubleCollection(dashArray);
            }
            return new DoubleCollection();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
