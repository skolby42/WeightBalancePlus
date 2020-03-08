using System.ComponentModel.DataAnnotations;
using WeightBalancePlus.Properties;

namespace WeightBalancePlus.Models
{
    public enum LoadCategory
    {
        [Display(Name = "Aircraft")]
        Aircraft,
        [Display(Name = "Baggage")]
        Baggage,
        [Display(Name = "Passengers")]
        Passengers,
        [Display(Name = "Pilots")]
        Pilots
    }

    public enum FlightPhase
    {
        [Display(Name = "Takeoff")]
        Takeoff,
        [Display(Name = "Landing")]
        Landing
    }
}
