using OxyPlot;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace WeightBalancePlus.Models
{
    /// <summary>
    /// Wrapper for DataPoint struct to allow serialization
    /// </summary>
    [XmlType(TypeName = "DataPoint")]
    [Serializable]
    public class DataPointExt : INotifyPropertyChanged, ICloneable
    {
        public DataPointExt()
        {

        }

        public DataPointExt(double x, double y)
        {
            X = x;
            Y = y;
        }

        public DataPointExt(DataPoint dataPoint)
            :this(dataPoint.X, dataPoint.Y)
        {
        }

        public double X { get; set; }
        public double Y { get; set; }
        
        [XmlIgnore]
        public DataPoint DataPoint => new DataPoint(X, Y);

        public object Clone()
        {
            return new DataPointExt(X, Y);
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
