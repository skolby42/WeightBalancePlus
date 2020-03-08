using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using OxyPlot;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using WeightBalancePlus.Models;
using WeightBalancePlus.Tools;

namespace WeightBalancePlus.ViewModels
{
    public class EditEnvelopeViewModel : ViewModelBase, IModalDialogViewModel
    {
        public EditEnvelopeViewModel(AircraftViewModel aircraftViewModel)
        {
            this.aircraftViewModel = aircraftViewModel;

            LoadNormalPlotPoints();
            LoadUtilityPoints();

            NormalPlotPoints.CollectionChanged += PlotPoints_CollectionChanged;
            UtilityPlotPoints.CollectionChanged += PlotPoints_CollectionChanged;

            UpdatePlot();
        }

        private AircraftViewModel aircraftViewModel;
        private DataPointExt defaultPoint = new DataPointExt();

        #region Private Methods

        private void PlotPoints_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdatePlot();
        }

        private void LoadNormalPlotPoints()
        {
            foreach (var p in aircraftViewModel.NormalPlotPoints)
                NormalPlotPoints.Add((DataPointExt)p.Clone());
            RaisePropertyChanged(nameof(NormalPlotPoints));
        }

        private void LoadUtilityPoints()
        {
            foreach (var p in aircraftViewModel.UtilityPlotPoints)
                UtilityPlotPoints.Add((DataPointExt)p.Clone());
            RaisePropertyChanged(nameof(UtilityPlotPoints));
        }

        private void RaisePlotPropertiesChanged()
        {
            RaisePropertyChanged(nameof(NormalPlotPoints));
            RaisePropertyChanged(nameof(UtilityPlotPoints));
            RaisePropertyChanged(nameof(MaxX));
            RaisePropertyChanged(nameof(MaxY));
            RaisePropertyChanged(nameof(MinX));
            RaisePropertyChanged(nameof(MinY));
        }

        #endregion

        #region Public Methods

        public void UpdatePlot()
        {
            RaisePlotPropertiesChanged();
        }

        #endregion

        #region Properties

        public DataPointExt SelectedNormalPlotPoint { get; set; }

        public DataPointExt SelectedUtilityPlotPoint { get; set; }

        private ObservableCollection<DataPointExt> normalPlotPoints = new ObservableCollection<DataPointExt>();
        public ObservableCollection<DataPointExt> NormalPlotPoints
        {
            get
            {
                return normalPlotPoints;
            }
            set
            {
                if (normalPlotPoints == value)
                    return;

                normalPlotPoints = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<DataPointExt> utilityPlotPoints = new ObservableCollection<DataPointExt>();
        public ObservableCollection<DataPointExt> UtilityPlotPoints
        {
            get
            {
                return utilityPlotPoints;
            }
            set
            {
                if (utilityPlotPoints == value)
                    return;

                utilityPlotPoints = value;
                RaisePropertyChanged();
            }
        }
        
        public double MaxX
        {
            get
            {
                double max = GetMinOrMaxX(false);
                return PlotHelper.AddPlotMaxBuffer(max);
            }
        }

        public double MinX
        {
            get
            {
                double min = GetMinOrMaxX(true);
                return PlotHelper.AddPlotMinBuffer(min);
            }
        }

        public double MaxY
        {
            get
            {
                double max = GetMinOrMaxY(false);
                return PlotHelper.AddPlotMaxBuffer(max);
            }
        }

        public double MinY
        {
            get
            {
                double min = GetMinOrMaxY(true);
                return PlotHelper.AddPlotMinBuffer(min);
            }
        }

        public Color NormalColor { get; set; } = Colors.Green;
        public Color UtilityColor { get; set; } = Colors.Orange;
        public LineStyle NormalLineStyle { get; set; } = LineStyle.Solid;
        public LineStyle UtilityLineStyle { get; set; } = LineStyle.Dot;
        public bool ShowLegend { get; set; }


        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            private set => Set(nameof(DialogResult), ref dialogResult, value);
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Commands

        private RelayCommand okCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(() =>
                {
                    aircraftViewModel.NormalPlotPoints = NormalPlotPoints;
                    aircraftViewModel.UtilityPlotPoints = UtilityPlotPoints;

                    DialogResult = true;
                }));
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


        private RelayCommand addNormalRowCommand;
        public RelayCommand AddNormalRowCommand
        {
            get
            {
                return addNormalRowCommand ?? (addNormalRowCommand = new RelayCommand(() =>
                {
                    var lastPoint = NormalPlotPoints.LastOrDefault() ?? new DataPointExt();
                    NormalPlotPoints.Add((DataPointExt)lastPoint.Clone());
                }));
            }
        }

        private RelayCommand addUtilityRowCommand;
        public RelayCommand AddUtilityRowCommand
        {
            get
            {
                return addUtilityRowCommand ?? (addUtilityRowCommand = new RelayCommand(() =>
                {
                    var lastPoint = UtilityPlotPoints.LastOrDefault() ?? new DataPointExt();
                    UtilityPlotPoints.Add((DataPointExt)lastPoint.Clone());
                }));
            }
        }

        private RelayCommand removeNormalRowCommand;
        public RelayCommand RemoveNormalRowCommand
        {
            get
            {
                return removeNormalRowCommand ?? (removeNormalRowCommand = new RelayCommand(() =>
                {
                    NormalPlotPoints.Remove(SelectedNormalPlotPoint);
                }, () => SelectedNormalPlotPoint != null));
            }
        }

        private RelayCommand removeUtilityRowCommand;
        public RelayCommand RemoveUtilityRowCommand
        {
            get
            {
                return removeUtilityRowCommand ?? (removeUtilityRowCommand = new RelayCommand(() =>
                {
                    UtilityPlotPoints.Remove(SelectedUtilityPlotPoint);
                }, () => SelectedUtilityPlotPoint != null));
            }
        }

        #endregion
    }
}
