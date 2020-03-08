using System.Windows.Controls;
using WeightBalancePlus.ViewModels;

namespace WeightBalancePlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ActiveAircraftViewModel?.UpdateLoadItems();
                mainViewModel.ActiveAircraftViewModel?.UpdatePlot();
            }
        }
    }
}
