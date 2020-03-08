using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Passenger : Person
    {
        public Passenger()
        {

        }

        public Passenger(double weight, string name = "")
        {
            Weight = weight;
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

        public override string Name { get; set; } = "Passenger";
        public override LoadCategory LoadCategory => LoadCategory.Passengers;

        public override object Clone()
        {
            return new Passenger(Weight, Name);
        }
    }
}
