using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeightBalancePlus.Models
{
    [XmlInclude(typeof(Pilot))]
    [XmlInclude(typeof(Passenger))]
    [Serializable]
    public abstract class Person : LoadItem
    {
        public bool RequiredCrew { get; set; }
        [XmlIgnore]
        public SeatRow SeatRow { get; set; }
    }
}
