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
    public class EditSeatConfigurationViewModel: ViewModelBase, IModalDialogViewModel
    {
        #region Properties

        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            private set => Set(nameof(DialogResult), ref dialogResult, value);
        }

        public ObservableCollection<SeatRow> SeatRows { get; set; } = new ObservableCollection<SeatRow>();
        public SeatRow SelectedRow { get; set; }

        #endregion

        #region Private Methods

        private void SetRowNumbers()
        {
            int rowNumber = 1;
            foreach (var row in SeatRows)
            {
                row.RowNumber = rowNumber;
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
                    if (SeatRows.Count == 0)
                        SeatRows.Add(new SeatRow(2, true));
                    else
                        SeatRows.Add(new SeatRow());
                    RaisePropertyChanged(nameof(SeatRows));
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
                    SeatRows.Remove(SelectedRow);
                    RaisePropertyChanged(nameof(SeatRows));
                }, () => SelectedRow != null));
            }
        }

        #endregion
    }
}
