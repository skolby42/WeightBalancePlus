using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using WeightBalancePlus.Properties;
using WeightBalancePlus.Tools;

namespace WeightBalancePlus.Models
{
    [Serializable]
    public class Aircraft
    {
        public Aircraft()
        {

        }

        public Aircraft(int maxSeats = 1, int requiredCrewMembers = 1)
        {
            MaxSeats = maxSeats;
            RequiredCrew = requiredCrewMembers;
        }

        public string Name { get; set; } = Resources.Aircraft;
        public string AircraftID { get; set; } = string.Empty;

        public double FuelCapacity { get; set; }
        public double FuelCenterGravity { get; set; }
        public double FuelMoment { get; set; }
        public double StartFuel { get; set; }
        public double EndFuel { get; set; }
        public double TaxiBurn { get; set; }
        public double EnrouteBurn { get; set; }

        public double MaxRampWeight { get; set; }
        public double MaxTakeoffWeight { get; set; }

        public double EmptyWeight { get; set; }
        public double Moment { get; set; }
        public double CenterGravity { get; set; }

        public int MaxSeats { get; set; } = 1;

        public int AvailableSeats => MaxSeats - LoadItems.OfType<Person>().Count();
        public int AvailableBaggage => BaggageCompartments.Count - LoadItems.OfType<Baggage>().Count();

        public int RequiredCrew { get; set; } = 1;

        public ObservableCollection<LoadItem> LoadItems { get; set; } = new ObservableCollection<LoadItem>();
        public ObservableCollection<SeatRow> SeatRows { get; set; } = new ObservableCollection<SeatRow>();
        public ObservableCollection<BaggageCompartment> BaggageCompartments { get; set; } = new ObservableCollection<BaggageCompartment>();
        public ObservableCollection<DataPointExt> NormalPlotPoints { get; set; } = new ObservableCollection<DataPointExt>();
        public ObservableCollection<DataPointExt> UtilityPlotPoints { get; set; } = new ObservableCollection<DataPointExt>();
    
        public Aircraft Clone()
        {
            return new Aircraft()
            {
                Name = Name,
                AircraftID = AircraftID,
                FuelCapacity = FuelCapacity,
                FuelCenterGravity = FuelCenterGravity,
                StartFuel = StartFuel,
                EndFuel = EndFuel,
                TaxiBurn = TaxiBurn,
                EnrouteBurn = EnrouteBurn,
                EmptyWeight = EmptyWeight,
                MaxRampWeight = MaxRampWeight,
                MaxTakeoffWeight = MaxTakeoffWeight,
                Moment = Moment,
                CenterGravity = CenterGravity,
                MaxSeats = MaxSeats,
                RequiredCrew = RequiredCrew,
                LoadItems = new ObservableCollection<LoadItem>(LoadItems.Select(x => (LoadItem)x.Clone()).ToList()),
                NormalPlotPoints = new ObservableCollection<DataPointExt>(NormalPlotPoints.Select(x => (DataPointExt)x.Clone()).ToList()),
                UtilityPlotPoints = new ObservableCollection<DataPointExt>(UtilityPlotPoints.Select(x => (DataPointExt)x.Clone()).ToList())
            };
        }

        public void SaveToXml()
        {
            string xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.AppName, Constants.AircraftDir, $"{Name}.xml");
            XmlTools.SaveToXml(this, xmlPath);
        }
    }
}
