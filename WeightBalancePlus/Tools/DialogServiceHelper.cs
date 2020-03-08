using MvvmDialogs;

namespace WeightBalancePlus.Tools
{
    public static class DialogServiceHelper
    {
        private static IDialogService instance;

        public static IDialogService Instance
        {
            get
            {
                return instance ?? (instance = new MvvmDialogs.DialogService());
            }
        }
    }
}
