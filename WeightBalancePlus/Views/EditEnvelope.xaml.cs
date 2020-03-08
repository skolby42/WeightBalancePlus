using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeightBalancePlus.ViewModels;

namespace WeightBalancePlus.Views
{
    /// <summary>
    /// Interaction logic for EditEnvelope.xaml
    /// </summary>
    public partial class EditEnvelope : Window
    {
        public EditEnvelope()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is EditEnvelopeViewModel vm)
                vm.UpdatePlot();
        }
    }
}
