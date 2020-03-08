using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MvvmDialogs;
using System.IO;
using System.Reflection;

namespace WeightBalancePlus.ViewModels
{
    public class AboutViewModel : ViewModelBase, IModalDialogViewModel
    {

        public string AppVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Revision}";
            }
        }

        public string License
        {
            get
            {
                return GetTextResource("License.txt");
            }
        }

        public string ThirdPartyComponents
        {
            get
            {
                return GetTextResource("ThirdPartyComponents.txt");
            }
        }

        private static string GetTextResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = $"WeightBalancePlus.Resources.TextFiles.{resourceName}";
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ?? (closeCommand = new RelayCommand(() =>
                {
                    DialogResult = true;
                }));
            }
        }

        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            private set => Set(nameof(DialogResult), ref dialogResult, value);
        }
    }
}
