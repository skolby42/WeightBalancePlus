using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using WeightBalancePlus.Models;
using WeightBalancePlus.Properties;
using WeightBalancePlus.Tools;

namespace WeightBalancePlus.ViewModels
{
    public class AircraftViewModel : ViewModelBase
    {
        public AircraftViewModel(Aircraft aircraft)
        {
            TargetAircraft = aircraft;

            InitializeLoadItems();
            UpdateSeatOccupants();
            UpdateLoadItems();

            LoadItems.CollectionChanged += LoadItems_CollectionChanged;
        }

        private readonly DataPointExt defaultPoint = new DataPointExt();
        private const double Gallon_Weight = 6;

        #region Properties

        public string Name
        {
            get
            {
                return TargetAircraft.Name;
            }
            set
            {
                if (TargetAircraft.Name == value)
                    return;
                TargetAircraft.Name = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string AircraftID
        {
            get
            {
                return TargetAircraft.AircraftID;
            }
            set
            {
                if (TargetAircraft.AircraftID == value)
                    return;
                TargetAircraft.AircraftID = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName => string.IsNullOrEmpty(AircraftID) ? Name : $"{Name} ({AircraftID})";

        public ObservableCollection<LoadItem> LoadItems
        {
            get
            {
                return TargetAircraft.LoadItems;
            }
            set
            {
                if (TargetAircraft.LoadItems == value)
                    return;
                TargetAircraft.LoadItems = value;
                RaisePropertyChanged();
                UpdatePlot();
            }
        }

        public ObservableCollection<DataPointExt> NormalPlotPoints
        {
            get
            {
                return TargetAircraft.NormalPlotPoints;
            }
            set
            {
                if (TargetAircraft.NormalPlotPoints == value)
                    return;

                TargetAircraft.NormalPlotPoints = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<DataPointExt> UtilityPlotPoints
        {
            get
            {
                return TargetAircraft.UtilityPlotPoints;
            }
            set
            {
                if (TargetAircraft.UtilityPlotPoints == value)
                    return;

                TargetAircraft.UtilityPlotPoints = value;
                RaisePropertyChanged();
            }
        }

        public LoadItem SelectedLoadItem { get; set; }


        public int Seats
        {
            get
            {
                return TargetAircraft.MaxSeats;
            }
            set
            {
                if (TargetAircraft.MaxSeats == value)
                    return;
                TargetAircraft.MaxSeats = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<SeatRow> SeatRows
        {
            get
            {
                return TargetAircraft.SeatRows;
            }
            set
            {
                if (TargetAircraft.SeatRows == value)
                    return;
                TargetAircraft.SeatRows = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BaggageCompartment> BaggageCompartments
        {
            get
            {
                return TargetAircraft.BaggageCompartments;
            }
            set
            {
                if (TargetAircraft.BaggageCompartments == value)
                    return;
                TargetAircraft.BaggageCompartments = value;
                RaisePropertyChanged();
            }
        }

        public int RequiredCrew
        {
            get
            {
                return TargetAircraft.RequiredCrew;
            }
            set
            {
                if (TargetAircraft.RequiredCrew == value)
                    return;
                TargetAircraft.RequiredCrew = value;
                RaisePropertyChanged();
            }
        }

        public int AvailableSeats => TargetAircraft.AvailableSeats;

        public int AvailableBaggage => TargetAircraft.AvailableBaggage;

        public double EmptyWeight
        {
            get
            {
                return TargetAircraft.EmptyWeight;
            }
            set
            {
                if (TargetAircraft.EmptyWeight == value)
                    return;
                TargetAircraft.EmptyWeight = value;
                RaisePropertyChanged();
            }
        }

        public double Moment
        {
            get
            {
                return TargetAircraft.Moment;
            }
            set
            {
                if (TargetAircraft.Moment == value)
                    return;
                TargetAircraft.Moment = value;
                RaisePropertyChanged();
            }
        }

        public double CenterGravity
        {
            get
            {
                return TargetAircraft.CenterGravity;
            }
            set
            {
                if (TargetAircraft.CenterGravity == value)
                    return;
                TargetAircraft.CenterGravity = value;
                RaisePropertyChanged();
            }
        }

        public double MaxRampWeight
        {
            get
            {
                return TargetAircraft.MaxRampWeight;
            }
            set
            {
                if (TargetAircraft.MaxRampWeight == value)
                    return;
                TargetAircraft.MaxRampWeight = value;
                RaisePropertyChanged();
            }
        }

        public double MaxTakeoffWeight
        {
            get
            {
                return TargetAircraft.MaxTakeoffWeight;
            }
            set
            {
                if (TargetAircraft.MaxTakeoffWeight == value)
                    return;
                TargetAircraft.MaxTakeoffWeight = value;
                RaisePropertyChanged();
            }
        }

        public double FuelCapacity
        {
            get
            {
                return TargetAircraft.FuelCapacity;
            }
            set
            {
                if (TargetAircraft.FuelCapacity == value)
                    return;
                TargetAircraft.FuelCapacity = value;
                RaisePropertyChanged();
            }
        }

        public double FuelCenterGravity
        {
            get
            {
                return TargetAircraft.FuelCenterGravity;
            }
            set
            {
                if (TargetAircraft.FuelCenterGravity == value)
                    return;
                TargetAircraft.FuelCenterGravity = value;
                RaisePropertyChanged();
            }
        }

        public double FuelMoment
        {
            get
            {
                return TargetAircraft.FuelMoment;
            }
            set
            {
                if (TargetAircraft.FuelMoment == value)
                    return;
                TargetAircraft.FuelMoment = value;
                RaisePropertyChanged();
            }
        }

        public double StartFuel
        {
            get
            {
                return TargetAircraft.StartFuel;
            }
            set
            {
                if (TargetAircraft.StartFuel == value)
                    return;

                TargetAircraft.StartFuel = value;
                RaisePropertyChanged();
                UpdateFuelLoad();
                UpdatePlot();
            }
        }

        public double TaxiBurn
        {
            get
            {
                return TargetAircraft.TaxiBurn;
            }
            set
            {
                if (TargetAircraft.TaxiBurn == value)
                    return;

                TargetAircraft.TaxiBurn = value;
                RaisePropertyChanged();
                UpdatePlot();
            }
        }

        public double EnrouteBurn
        {
            get
            {
                return TargetAircraft.EnrouteBurn;
            }
            set
            {
                if (TargetAircraft.EnrouteBurn == value)
                    return;

                TargetAircraft.EnrouteBurn = value;
                RaisePropertyChanged();
                UpdatePlot();
            }
        }

        public double TakeoffWeight => LoadItems.Sum(w => w.Weight) - (TaxiBurn * Gallon_Weight);
        public double LandingWeight => TakeoffWeight - (EnrouteBurn * Gallon_Weight);

        public double TakeoffCenterGravity
        {
            get
            {
                var fuelItem = LoadItems.OfType<Fuel>().FirstOrDefault();
                if (fuelItem == null)
                    return 0;
                return (LoadItems.Sum(l => l.Moment) - fuelItem.CenterGravity * TaxiBurn * Gallon_Weight) / TakeoffWeight;
            }
        }

        public double LandingCenterGravity
        {
            get
            {
                var fuelItem = LoadItems.OfType<Fuel>().FirstOrDefault();
                if (fuelItem == null)
                    return 0;
                return (LoadItems.Sum(l => l.Moment) -
                    (fuelItem.CenterGravity * TaxiBurn * Gallon_Weight) -
                    (fuelItem.CenterGravity * EnrouteBurn * Gallon_Weight)) / LandingWeight;
            }
        }
        public string TakeoffAnnotationText
        {
            get
            {
                string text = ShowAnnotationDetail ? $"{Resources.Takeoff}, ({TakeoffCenterGravity:N1}, {TakeoffWeight:N1})" : Resources.Takeoff;
                return ShowTakeoffAnnotation() ? text : string.Empty;
            }
        }
        public string LandingAnnotationText
        {
            get
            {
                string text = ShowAnnotationDetail ? $"{Resources.Landing}, ({LandingCenterGravity:N1}, {LandingWeight:N1})" : Resources.Landing;
                return ShowLandingAnnotation() ? text : string.Empty;
            }
        }

        public bool ShowAnnotationDetail { get; set; }

        public Color TakeoffColor => ShowTakeoffAnnotation() ? Colors.DarkGreen : Colors.Transparent;
        public Color LandingColor => ShowLandingAnnotation() ? Colors.DarkRed : Colors.Transparent;

        public double MaxX
        {
            get
            {
                double max;
                max = GetMinOrMaxX(false);
                max = GetMaxCenterGravity(max);
                return PlotHelper.AddPlotMaxBuffer(max);
            }
        }

        public double MinX
        {
            get
            {
                double min;
                min = GetMinOrMaxX(true);
                min = GetMinCenterGravity(min);
                return PlotHelper.AddPlotMinBuffer(min);
            }
        }

        public double MaxY
        {
            get
            {
                double max = GetMinOrMaxY(false);
                max = Math.Max(max, TakeoffWeight);
                return PlotHelper.AddPlotMaxBuffer(max);
            }
        }

        public double MinY
        {
            get
            {
                double min = GetMinOrMaxY(true);
                if (TakeoffWeight > 0)
                    min = Math.Min(min, TakeoffWeight);
                return PlotHelper.AddPlotMinBuffer(min);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeLoadItems()
        {
            if (LoadItems.Count > 0 || TargetAircraft == null)
                return;

            LoadItems.Add(new EmptyAircraft(TargetAircraft.EmptyWeight));
            LoadItems.Add(new Fuel(TargetAircraft.StartFuel * Gallon_Weight));
            UpdateRequiredCrew();
        }

        private double GetMinOrMaxX(bool min)
        {
            double value;

            if (NormalPlotPoints.Count == 0)
            {
                value = min ?
                    UtilityPlotPoints.DefaultIfEmpty(defaultPoint).Min(p => p.X) :
                    UtilityPlotPoints.DefaultIfEmpty(defaultPoint).Max(p => p.X);
            }
            else if (UtilityPlotPoints.Count == 0)
            {
                value = min ?
                    NormalPlotPoints.DefaultIfEmpty(defaultPoint).Min(p => p.X) :
                    NormalPlotPoints.DefaultIfEmpty(defaultPoint).Max(p => p.X);
            }
            else
            {
                value = min ?
                    Math.Min(NormalPlotPoints.Min(p => p.X), UtilityPlotPoints.Min(p => p.X)) :
                    Math.Max(NormalPlotPoints.Max(p => p.X), UtilityPlotPoints.Max(p => p.X));
            }

            return value;
        }

        private double GetMinOrMaxY(bool min)
        {
            double value;

            if (NormalPlotPoints.Count == 0)
            {
                value = min ?
                    UtilityPlotPoints.DefaultIfEmpty(defaultPoint).Min(p => p.Y) :
                    UtilityPlotPoints.DefaultIfEmpty(defaultPoint).Max(p => p.Y);
            }
            else if (UtilityPlotPoints.Count == 0)
            {
                value = min ?
                    NormalPlotPoints.DefaultIfEmpty(defaultPoint).Min(p => p.Y) :
                    NormalPlotPoints.DefaultIfEmpty(defaultPoint).Max(p => p.Y);
            }
            else
            {
                value = min ?
                    Math.Min(NormalPlotPoints.Min(p => p.Y), UtilityPlotPoints.Min(p => p.Y)) :
                    Math.Max(NormalPlotPoints.Max(p => p.Y), UtilityPlotPoints.Max(p => p.Y));
            }

            return value;
        }

        private double GetMaxCenterGravity(double plotMax)
        {
            var maxCenterGravity = Math.Max(TakeoffCenterGravity, LandingCenterGravity);
            return Math.Max(plotMax, maxCenterGravity);
        }

        private double GetMinCenterGravity(double plotMin)
        {
            var minCenterGravity = Math.Max(TakeoffCenterGravity, LandingCenterGravity);
            if (minCenterGravity == 0)
                return plotMin;
            return Math.Min(plotMin, minCenterGravity);
        }

        private void UpdateSeatOccupants()
        {
            var occupants = LoadItems.OfType<Person>();
            foreach (var occupant in occupants)
                occupant.SeatRow = SeatRows.FirstOrDefault(x => x.CenterGravity == occupant.CenterGravity);

            foreach (var seatRow in SeatRows)
                seatRow.Occupants = occupants.Count(x => x.SeatRow == seatRow);
        }

        private void UpdateRequiredCrew()
        {
            if (TargetAircraft == null)
                return;

            var pilots = LoadItems.OfType<Pilot>().ToList();
            if (pilots.Count == TargetAircraft.RequiredCrew)
            {
                var standardNames = new string[] { Resources.Pilot, Resources.Copilot };
                for (int i = 0; i < pilots.Count; i++)
                {
                    var pilot = pilots[i];
                    if (!standardNames.Contains(pilot.Name))
                        continue;

                    if (i == 0)
                        pilots[i].Name = Resources.Pilot;
                    else
                        pilots[i].Name = Resources.Copilot;
                }
            }
            else if (pilots.Count > TargetAircraft.RequiredCrew)
            {
                for (int i = pilots.Count - 1; i > TargetAircraft.RequiredCrew - 1; i--)
                    LoadItems.Remove(pilots[i]);
            }
            else
            {
                for (int i = pilots.Count; i < TargetAircraft.RequiredCrew; i++)
                {
                    var seatRow = SeatRows.FirstOrDefault(x => x.PilotSeats);
                    if (seatRow == null)
                        return;

                    string name = Resources.Pilot;
                    if (i > 1)
                        name = Resources.Copilot;
                    LoadItems.Add(new Pilot(170, name)
                    {
                        CenterGravity = seatRow.CenterGravity,
                        SeatRow = seatRow
                    });

                    UpdateSeatOccupants();
                }
            }

            RaisePropertyChanged(nameof(LoadItems));
        }

        private void AddPassenger()
        {
            var seatRow = SeatRows.FirstOrDefault(x => x.Seats - x.Occupants > 0);
            if (seatRow == null)
                return;
            LoadItems.Add(new Passenger(170) 
            {
                CenterGravity = seatRow.CenterGravity,
                SeatRow = seatRow 
            });
            UpdateSeatOccupants();
            RaisePropertyChanged(nameof(LoadItems));
            UpdatePlot();
        }

        private void AddBaggage()
        {
            if (BaggageCompartments.Count == 0)
                return;

            var index = LoadItems.OfType<Baggage>().Count();
            var baggageCompartment = BaggageCompartments.ElementAt(index);
            LoadItems.Add(new Baggage(10)
            {
                CenterGravity = baggageCompartment.CenterGravity
            });
            RaisePropertyChanged(nameof(LoadItems));
            UpdatePlot();
        }

        private void RemoveLoadItem()
        {
            if (SelectedLoadItem == null)
                return;

            if (SelectedLoadItem is Person person)
                person.SeatRow.Occupants--;

            LoadItems.Remove(SelectedLoadItem);
            RaisePropertyChanged(nameof(LoadItems));
            UpdatePlot();
        }

        private void UpdateFuelLoad()
        {
            var fuelItem = LoadItems.OfType<Fuel>().FirstOrDefault();
            if (fuelItem == null)
                return;

            FuelMoment = StartFuel * Gallon_Weight * FuelCenterGravity;
            if (FuelCenterGravity == 0 && StartFuel > 0)
                FuelCenterGravity = FuelMoment / (StartFuel * Gallon_Weight);

            fuelItem.Weight = StartFuel * Gallon_Weight;
            fuelItem.Moment = FuelMoment;
            fuelItem.CenterGravity = FuelCenterGravity;
        }

        private void UpdateEmptyLoad()
        {
            var emptyAircraftItem = LoadItems.OfType<EmptyAircraft>().FirstOrDefault();
            if (emptyAircraftItem == null)
                return;

            Moment = EmptyWeight * CenterGravity;
            if (CenterGravity == 0 && EmptyWeight > 0)
                CenterGravity = Moment / EmptyWeight;

            emptyAircraftItem.Weight = EmptyWeight;
            emptyAircraftItem.Moment = Moment;
            emptyAircraftItem.CenterGravity = CenterGravity;
        }

        private void LoadItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdatePlot();
        }

        private bool ShowTakeoffAnnotation()
        {
            return TakeoffCenterGravity != 0 && TakeoffWeight != 0;
        }

        private bool ShowLandingAnnotation()
        {
            return EnrouteBurn > 0 && LandingCenterGravity != 0 && LandingWeight != 0;
        }

        #endregion

        public void UpdatePlot()
        {
            RaisePropertyChanged(nameof(TakeoffColor));
            RaisePropertyChanged(nameof(TakeoffAnnotationText));
            RaisePropertyChanged(nameof(TakeoffCenterGravity));
            RaisePropertyChanged(nameof(TakeoffWeight));

            RaisePropertyChanged(nameof(LandingColor));
            RaisePropertyChanged(nameof(LandingAnnotationText));
            RaisePropertyChanged(nameof(LandingCenterGravity));
            RaisePropertyChanged(nameof(LandingWeight));

            RaisePropertyChanged(nameof(MaxX));
            RaisePropertyChanged(nameof(MinX));
            RaisePropertyChanged(nameof(MaxY));
            RaisePropertyChanged(nameof(MinY));
        }

        public void UpdateLoadItems()
        {
            UpdateEmptyLoad();
            UpdateFuelLoad();
            UpdateRequiredCrew();
            UpdateSeatOccupants();
            RaisePropertyChanged(nameof(LoadItems));
        }

        public void SaveToXml()
        {
            TargetAircraft.SaveToXml();
        }

        #region Commands

        private RelayCommand addPasssengerCommand;
        public RelayCommand AddPassengerCommand
        {
            get
            {
                return addPasssengerCommand ?? (addPasssengerCommand = new RelayCommand(
                    () => AddPassenger(),
                    () => AvailableSeats > 0
                ));
            }
        }

        private RelayCommand addBaggageCommand;
        public RelayCommand AddBaggageCommand
        {
            get
            {
                return addBaggageCommand ?? (addBaggageCommand = new RelayCommand(
                    () => AddBaggage(),
                    () => AvailableBaggage > 0
                ));
            }
        }

        private RelayCommand removeLoadItemCommand;
        public RelayCommand RemoveLoadItemCommand
        {
            get
            {
                return removeLoadItemCommand ?? (removeLoadItemCommand = new RelayCommand(
                    () => RemoveLoadItem(),
                    () => ((SelectedLoadItem is Person person) && person.RequiredCrew == false) ||
                         (SelectedLoadItem is Baggage)
                ));
            }
        }

        public Aircraft TargetAircraft { get; private set; }

        #endregion
    }
}
