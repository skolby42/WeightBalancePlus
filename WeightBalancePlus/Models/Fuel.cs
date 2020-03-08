using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Fuel : LoadItem
    {
        public Fuel()
        {

        }

        public Fuel(double weight, string name = "")
        {
            Weight = weight;
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

        public override string Name { get; set; } = Resources.Fuel;
        public override LoadCategory LoadCategory => LoadCategory.Aircraft;
        public override bool IsReadOnly => true;

        public override object Clone()
        {
            return new Fuel(Weight, Name);
        }
    }
}
