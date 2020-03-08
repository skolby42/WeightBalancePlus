using OxyPlot;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WeightBalancePlus.Models;

namespace WeightBalancePlus.Converters
{
    public class DataPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ObservableCollection<DataPointExt> dataPointExtCollection ? 
                new ObservableCollection<DataPoint>(dataPointExtCollection.Select(p => p.DataPoint)) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
