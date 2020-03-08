using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Seat
    {
        public Seat()
        {

        }

        public string Name { get; set; }
        public double DatumDistance { get; set; }
        public bool PilotSeat { get; set; }
    }
}
