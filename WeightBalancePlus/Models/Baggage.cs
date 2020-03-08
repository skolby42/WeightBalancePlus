using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Baggage : LoadItem
    {
        public Baggage()
        {

        }

        public Baggage (double weight, string name = "")
        {
            Weight = weight;
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

        public override string Name { get; set; } = Resources.Baggage;
        public override LoadCategory LoadCategory => LoadCategory.Baggage;

        public override object Clone()
        {
            return new Baggage(Weight, Name);
        }
    }
}
