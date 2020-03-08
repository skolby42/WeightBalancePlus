using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class EmptyAircraft : LoadItem
    {
        public EmptyAircraft()
        {

        }

        public EmptyAircraft(double weight, string name = "")
        {
            Weight = weight;
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

        public override string Name { get; set; } = Resources.EmptyWeight;
        public override LoadCategory LoadCategory => LoadCategory.Aircraft;
        public override bool IsReadOnly => true;

        public override object Clone()
        {
            return new EmptyAircraft(Weight, Name);
        }
    }
}
