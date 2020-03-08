using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WeightBalancePlus.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (sender is TextBox tb && tb.SelectionLength != 0)
                tb.SelectionLength = 0;
        }
    }
}
