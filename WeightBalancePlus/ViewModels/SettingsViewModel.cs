using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using OxyPlot;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WeightBalancePlus.Models;
using WeightBalancePlus.Tools;
using Xceed.Wpf.Toolkit;

namespace WeightBalancePlus.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IModalDialogViewModel
    {
        public SettingsViewModel()
        {
            LoadSettings();
            PopulateColorList();
            PopulateLineList();
        }

        #region Properties

        Settings TargetSettings { get; set; } = new Settings();

        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            private set => Set(nameof(DialogResult), ref dialogResult, value);
        }

        private ObservableCollection<AircraftViewModel> aircraftList = new ObservableCollection<AircraftViewModel>();
        public ObservableCollection<AircraftViewModel> AircraftList
        {
            get
            {
                return aircraftList;
            }
            set
            {
                if (aircraftList == value)
                    return;
                aircraftList = value;
                RaisePropertyChanged();
            }
        }


        private AircraftViewModel selectedAircraft;
        public AircraftViewModel SelectedAircraft
        {
            get
            {
                return selectedAircraft;
            }
            set
            {
                if (selectedAircraft == value)
                    return;
                selectedAircraft = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<LineStyle> LineList { get; set; } = new ObservableCollection<LineStyle>();

        public LineStyle NormalLineStyle
        {
            get
            {
                return TargetSettings.NormalLineStyle;
            }
            set
            {
                if (TargetSettings.NormalLineStyle == value)
                    return;
                TargetSettings.NormalLineStyle = value;
                RaisePropertyChanged();
            }
        }

        public LineStyle UtilityLineStyle
        {
            get
            {
                return TargetSettings.UtilityLineStyle;
            }
            set
            {
                if (TargetSettings.UtilityLineStyle == value)
                    return;
                TargetSettings.UtilityLineStyle = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ColorItem> ColorList { get; set; } = new ObservableCollection<ColorItem>();


        public Color NormalColor
        {
            get
            {
                return TargetSettings.NormalColor;
            }
            set
            {
                if (TargetSettings.NormalColor == value)
                    return;
                TargetSettings.NormalColor = value;
                RaisePropertyChanged();
            }
        }

        public Color UtilityColor
        {
            get
            {
                return TargetSettings.UtilityColor;
            }
            set
            {
                if (TargetSettings.UtilityColor == value)
                    return;
                TargetSettings.UtilityColor = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowLegend
        {
            get
            {
                return TargetSettings.ShowLegend;
            }
            set
            {
                if (TargetSettings.ShowLegend == value)
                    return;
                TargetSettings.ShowLegend = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowAnnotationDetail
        {
            get
            {
                return TargetSettings.ShowAnnotationDetail;
            }
            set
            {
                if (TargetSettings.ShowAnnotationDetail == value)
                    return;
                TargetSettings.ShowAnnotationDetail = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Private Methods

        private void LoadSettings()
        {
            var settingsXml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.AppName, "Settings.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(settingsXml));

            TargetSettings = XmlTools.LoadFromXml<Settings>(settingsXml) ?? new Settings();
        }

        private void PopulateColorList()
        {
            ColorList.Add(new ColorItem(Colors.LightBlue, "LightBlue"));
            ColorList.Add(new ColorItem(Colors.Blue, "Blue"));
            ColorList.Add(new ColorItem(Colors.DarkBlue, "Dark Blue"));
            ColorList.Add(new ColorItem(Colors.LightGreen, "LightGreen"));
            ColorList.Add(new ColorItem(Colors.Green, "Green"));
            ColorList.Add(new ColorItem(Colors.DarkGreen, "Dark Green"));
            ColorList.Add(new ColorItem(Colors.Orange, "Orange"));
            ColorList.Add(new ColorItem(Colors.DarkOrange, "Dark Orange"));
            ColorList.Add(new ColorItem(Colors.Red, "Red"));
            ColorList.Add(new ColorItem(Colors.DarkRed, "Dark Red"));
            ColorList.Add(new ColorItem(Colors.Violet, "Violet"));
            ColorList.Add(new ColorItem(Colors.DarkViolet, "Dark Violet"));
            ColorList.Add(new ColorItem(Colors.Yellow, "Yellow"));
        }

        private void PopulateLineList()
        {
            LineList.Add(LineStyle.Dash);
            LineList.Add(LineStyle.DashDashDot);
            LineList.Add(LineStyle.DashDashDotDot);
            LineList.Add(LineStyle.DashDot);
            LineList.Add(LineStyle.DashDotDot);
            LineList.Add(LineStyle.Dot);
            LineList.Add(LineStyle.LongDash);
            LineList.Add(LineStyle.LongDashDot);
            LineList.Add(LineStyle.LongDashDotDot);
            LineList.Add(LineStyle.Solid);
        }

        private bool ValidateAircraftNames()
        {
            var duplicates = AircraftList.Select(x => x.Name).ToList().GroupBy(n => n).Any(c => c.Count() > 1);
            return !duplicates;
        }

        private string GetNextAircraftName(string name)
        {
            var count = AircraftList.Count(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            if (count == 0)
                return name;

            return $"{name}_{count}";
        }

        #endregion

        #region Commands

        private RelayCommand okCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(() =>
                {
                    foreach (var aircraft in AircraftList)
                    {
                        aircraft.UpdateLoadItems();
                        aircraft.SaveToXml();
                    }

                    TargetSettings.SaveToXml();

                    DialogResult = true;
                }, () => ValidateAircraftNames()));
            }
        }


        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(() =>
                {
                    DialogResult = false;
                }));
            }
        }


        private RelayCommand addAircraftCommand;
        public RelayCommand AddAircraftCommand
        {
            get
            {
                return addAircraftCommand ?? (addAircraftCommand = new RelayCommand(() =>
                {
                    var aircraft = new Aircraft();
                    aircraft.Name = GetNextAircraftName(aircraft.Name);
                    AircraftList.Add(new AircraftViewModel(aircraft));
                    RaisePropertyChanged(nameof(AircraftList));
                }));
            }
        }


        private RelayCommand removeAircraftCommand;
        public RelayCommand RemoveAircraftCommand
        {
            get
            {
                return removeAircraftCommand ?? (removeAircraftCommand = new RelayCommand(() =>
                {
                    AircraftList.Remove(SelectedAircraft);
                    RaisePropertyChanged(nameof(AircraftList));
                }, () => SelectedAircraft != null));
            }
        }

        private RelayCommand copyAircraftCommand;
        public RelayCommand CopyAircraftCommand
        {
            get
            {
                return copyAircraftCommand ?? (copyAircraftCommand = new RelayCommand(() =>
                {
                    var aircraft = SelectedAircraft.TargetAircraft.Clone();
                    aircraft.Name = GetNextAircraftName(aircraft.Name);
                    AircraftList.Add(new AircraftViewModel(aircraft));
                    RaisePropertyChanged(nameof(AircraftList));
                }, () => SelectedAircraft != null));
            }
        }


        private RelayCommand editLoadEnvelopeCommand;
        public RelayCommand EditLoadEnvelopeCommand
        {
            get
            {
                return editLoadEnvelopeCommand ?? (editLoadEnvelopeCommand = new RelayCommand(() =>
                {
                    var editEnvelopeViewModel = new EditEnvelopeViewModel(SelectedAircraft)
                    {
                        NormalColor = NormalColor,
                        UtilityColor = UtilityColor,
                        NormalLineStyle = NormalLineStyle,
                        UtilityLineStyle = UtilityLineStyle,
                        ShowLegend = ShowLegend
                    };

                    DialogServiceHelper.Instance.ShowDialog(this, editEnvelopeViewModel);
                }, () => SelectedAircraft != null));
            }
        }

        private RelayCommand editSeatsCommand;
        public RelayCommand EditSeatsCommand
        {
            get
            {
                return editSeatsCommand ?? (editSeatsCommand = new RelayCommand(() =>
                {
                    var editSeatConfigurationViewModel = new EditSeatConfigurationViewModel()
                    {
                        SeatRows = new ObservableCollection<SeatRow>(SelectedAircraft.SeatRows.Select(x => (SeatRow)x.Clone()))
                    };

                    if (DialogServiceHelper.Instance.ShowDialog(this, editSeatConfigurationViewModel) == true)
                    {
                        SelectedAircraft.SeatRows = new ObservableCollection<SeatRow>(editSeatConfigurationViewModel.SeatRows.Select(x => (SeatRow)x.Clone()));
                    }
                }, () => SelectedAircraft != null));
            }
        }

        private RelayCommand editBaggageCommand;
        public RelayCommand EditBaggageCommand
        {
            get
            {
                return editBaggageCommand ?? (editBaggageCommand = new RelayCommand(() =>
                {
                    var editBaggageConfigurationViewModel = new EditBaggageConfigurationViewModel()
                    {
                        BaggageCompartments = new ObservableCollection<BaggageCompartment>(SelectedAircraft.BaggageCompartments.Select(x => (BaggageCompartment)x.Clone()))
                    };

                    if (DialogServiceHelper.Instance.ShowDialog(this, editBaggageConfigurationViewModel) == true)
                    {
                        SelectedAircraft.BaggageCompartments = new ObservableCollection<BaggageCompartment>(editBaggageConfigurationViewModel.BaggageCompartments.Select(x => (BaggageCompartment)x.Clone()));
                    }
                }, () => SelectedAircraft != null));
            }
        }

        #endregion

    }
}
