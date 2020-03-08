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
    public class SeatRow : ICloneable
    {
        public SeatRow()
        {

        }

        public SeatRow(int numSeats, bool pilotSeats)
        {
            Seats = numSeats;
            PilotSeats = pilotSeats;
        }

        public int Seats { get; set; } = 2;
        public bool PilotSeats { get; set; }
        public double CenterGravity { get; set; }
        public int Occupants { get; set; }
        public int RowNumber { get; set; }
        public string DisplayName => $"{Resources.Row}: {RowNumber} ({CenterGravity:N1}) {Resources.Seats}: {Seats - Occupants}";

        public object Clone()
        {
            return new SeatRow()
            {
                Seats = Seats,
                PilotSeats = PilotSeats,
                CenterGravity = CenterGravity,
                Occupants = Occupants,
                RowNumber = RowNumber
            };
        }
    }
}
