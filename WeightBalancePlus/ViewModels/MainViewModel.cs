using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WeightBalancePlus.Models;
using WeightBalancePlus.Tools;

namespace WeightBalancePlus.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Initialize();
        }

        public SettingsViewModel SettingsViewModel { get; set; }
        
        private AircraftViewModel activeAircraftViewModel;
        public AircraftViewModel ActiveAircraftViewModel
        {
            get
            {
                return activeAircraftViewModel;
            }
            set
            {
                if (activeAircraftViewModel == value)
                    return;
                activeAircraftViewModel = value;
                UpdateLoadItems();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<AircraftViewModel> AircraftList { get; set; } = new ObservableCollection<AircraftViewModel>();

        private void Initialize()
        {
            LoadAircraftList();
            AddDefaultAircraft();            

            SettingsViewModel = new SettingsViewModel();
            UpdateAnnotationDetail();
        }

        private void LoadAircraftList()
        {
            var xmlDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.AppName, Constants.AircraftDir);
            Directory.CreateDirectory(xmlDir);
            var xmlFiles = Directory.GetFiles(xmlDir, "*.xml");

            AircraftList.Clear();
            foreach (var xmlFile in xmlFiles)
            {
                var aircraft = XmlTools.LoadFromXml<Aircraft>(xmlFile);
                AircraftList.Add(new AircraftViewModel(aircraft));
            }

            ActiveAircraftViewModel = AircraftList.FirstOrDefault();
        }

        private void AddDefaultAircraft()
        {
            if (AircraftList.Count > 0)
                return;

            AircraftList.Add(new AircraftViewModel(new Aircraft(4, 1)
            {
                Name = "C172",
                EmptyWeight = 1444,
                MaxRampWeight = 2300,
                MaxTakeoffWeight = 2300,
                FuelCapacity = 38,
                StartFuel = 38,
                NormalPlotPoints = new ObservableCollection<DataPointExt>
                {
                    new DataPointExt(35, 1500),
                    new DataPointExt(35, 1950),
                    new DataPointExt(38.5, 2300),
                    new DataPointExt(47.3, 2300),
                    new DataPointExt(47.3, 1500),
                    new DataPointExt(35, 1500),
                },
                UtilityPlotPoints = new ObservableCollection<DataPointExt>
                {
                    new DataPointExt(35, 1500),
                    new DataPointExt(35, 1950),
                    new DataPointExt(35.5, 2000),
                    new DataPointExt(40.5, 2000),
                    new DataPointExt(40.5, 1500),
                    new DataPointExt(35, 1500),
                }
            }));

            ActiveAircraftViewModel = AircraftList.FirstOrDefault();
        }

        private void UpdateLoadItems()
        {
            ActiveAircraftViewModel?.UpdateLoadItems();
        }

        private void UpdateAnnotationDetail()
        {
            if (SettingsViewModel == null)
                return;

            foreach (var aircraft in AircraftList)
               aircraft.ShowAnnotationDetail = SettingsViewModel.ShowAnnotationDetail;
            ActiveAircraftViewModel?.RaisePropertyChanged(nameof(ActiveAircraftViewModel.TakeoffAnnotationText));
            ActiveAircraftViewModel?.RaisePropertyChanged(nameof(ActiveAircraftViewModel.LandingAnnotationText));
        }

        #region Commands


        private RelayCommand aboutCommand;
        public RelayCommand AboutCommand
        {
            get
            {
                return aboutCommand ?? (aboutCommand = new RelayCommand(() =>
                {
                    AboutViewModel aboutViewModel = new AboutViewModel();
                    DialogServiceHelper.Instance.ShowDialog(this, aboutViewModel);
                }));
            }
        }


        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new RelayCommand(() =>
                {
                    SettingsViewModel = new SettingsViewModel
                    {
                        AircraftList = new ObservableCollection<AircraftViewModel>(AircraftList)
                    };

                    if (DialogServiceHelper.Instance.ShowDialog(this, SettingsViewModel) == true)
                    {
                        LoadAircraftList();
                        UpdateAnnotationDetail();
                        RaisePropertyChanged(nameof(SettingsViewModel));
                    }
                }));
            }
        }

        private RelayCommand exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new RelayCommand(
                    () => FireCloseApplication()
                ));
            }
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(() =>
                {
                    ActiveAircraftViewModel.SaveToXml();
                }));
            }
        }

        #endregion

        #region Delegates

        public delegate void CloseApplicationEvent();
        public event CloseApplicationEvent CloseApplication;
        public void FireCloseApplication()
        {
            CloseApplication?.Invoke();
        }

        #endregion
    }
}