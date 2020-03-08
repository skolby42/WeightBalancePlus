using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class BaggageCompartment : ICloneable
    {
        public BaggageCompartment()
        {

        }

        public BaggageCompartment(double centerGravity, double maxWeight = 0)
        {
            CenterGravity = centerGravity;
            MaxWeight = maxWeight;
        }

        public double CenterGravity { get; set; }
        public double MaxWeight { get; set; }

        public int CompartmentNumber { get; set; }
        public string DisplayName => $"{Resources.Baggage} {CompartmentNumber} ({CenterGravity:N1})";

        public object Clone()
        {
            return new BaggageCompartment()
            {
                CenterGravity = CenterGravity,
                MaxWeight = MaxWeight,
                CompartmentNumber = CompartmentNumber
            };
        }
    }
}
