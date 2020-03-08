using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeightBalancePlus.Models
{
    [XmlInclude(typeof(Baggage))]
    [XmlInclude(typeof(EmptyAircraft))]
    [XmlInclude(typeof(Fuel))]
    [XmlInclude(typeof(Pilot))]
    [XmlInclude(typeof(Passenger))]
    [Serializable]
    public abstract class LoadItem : INotifyPropertyChanged, ICloneable
    {
        public abstract string Name { get; set; }

        public virtual LoadCategory LoadCategory => LoadCategory.Aircraft;

        private double weight;
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight == value)
                    return;
                weight = value;
                RaisePropertyChanged();
            }
        }

        private double centerGravity;
        public double CenterGravity
        {
            get
            {
                return centerGravity;
            }
            set
            {
                if (centerGravity == value)
                    return;
                centerGravity = value;
                CalculateMoment();
                RaisePropertyChanged();
            }
        }

        private double moment;
        public double Moment
        {
            get
            {
                return moment;
            }
            set
            {
                if (moment == value)
                    return;
                moment = value;
                CalculateCenterGravity();
                RaisePropertyChanged();
            }
        }

        public virtual bool IsReadOnly { get; set; }

        public abstract object Clone();

        #region Private Methods

        private void CalculateMoment()
        {
            moment = weight * centerGravity;
        }

        private void CalculateCenterGravity()
        {
            centerGravity = (weight > 0) ? moment / weight : 0;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
