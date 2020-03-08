using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Pilot : Person
    {
        public Pilot()
        {

        }

        public Pilot(double weight, string name = "")
        {
            RequiredCrew = true;
            Weight = weight;

            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

        public override string Name { get; set; } = Resources.Pilot;
        public override LoadCategory LoadCategory => LoadCategory.Pilots;

        public override object Clone()
        {
            return new Pilot(Weight, Name);
        }
    }
}
