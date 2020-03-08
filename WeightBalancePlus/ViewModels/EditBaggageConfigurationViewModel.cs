using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightBalancePlus.Models;

namespace WeightBalancePlus.ViewModels
{
    public class EditBaggageConfigurationViewModel: ViewModelBase, IModalDialogViewModel
    {
        #region Properties

        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            private set => Set(nameof(DialogResult), ref dialogResult, value);
        }

        public ObservableCollection<BaggageCompartment> BaggageCompartments { get; set; } = new ObservableCollection<BaggageCompartment>();
        public BaggageCompartment SelectedRow { get; set; }

        #endregion

        #region Private Methods

        private void SetRowNumbers()
        {
            int rowNumber = 1;
            foreach (var row in BaggageCompartments)
            {
                row.CompartmentNumber = rowNumber;
                rowNumber++;
            }
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
                    SetRowNumbers();
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


        private RelayCommand addRowCommand;
        public RelayCommand AddRowCommand
        {
            get
            {
                return addRowCommand ?? (addRowCommand = new RelayCommand(() =>
                {
                    BaggageCompartments.Add(new BaggageCompartment());
                    RaisePropertyChanged(nameof(BaggageCompartments));
                }));
            }
        }


        private RelayCommand removeRowCommand;
        public RelayCommand RemoveRowCommand
        {
            get
            {
                return removeRowCommand ?? (removeRowCommand = new RelayCommand(() =>
                {
                    BaggageCompartments.Remove(SelectedRow);
                    RaisePropertyChanged(nameof(BaggageCompartments));
                }, () => SelectedRow != null));
            }
        }

        #endregion
    }
}
