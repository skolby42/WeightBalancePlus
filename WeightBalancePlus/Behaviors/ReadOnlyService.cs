using System.Windows;

namespace WeightBalancePlus.Behaviors
{
    // https://stackoverflow.com/questions/2030143/wpf-datagrid-with-some-read-only-rows
    public class ReadOnlyService : DependencyObject
    {
        #region IsReadOnly

        /// <summary>
        /// IsReadOnly Attached Dependency Property
        /// </summary>
        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(ReadOnlyService),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the IsReadOnly property.
        /// </summary>
        public static bool GetIsReadOnly(DependencyObject d)
        {
            return (bool)d.GetValue(BehaviorProperty);
        }

        /// <summary>
        /// Sets the IsReadOnly property.
        /// </summary>
        public static void SetIsReadOnly(DependencyObject d, bool value)
        {
            d.SetValue(BehaviorProperty, value);
        }

        #endregion IsReadOnly
    }
}
