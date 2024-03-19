using System.Windows;
using System.Windows.Controls;

namespace Autocomp.Nmea.TestApp.Controls
{

    public partial class PropertyViewer : AutoFormControl
    {
        public static readonly DependencyProperty ObjectToDisplayProperty =
            DependencyProperty.Register(nameof(ObjectToDisplay), typeof(object), typeof(PropertyViewer), new PropertyMetadata(null, MessageChanged));

        public PropertyViewer()
        {
            InitializeComponent();
        }

        public object? ObjectToDisplay
        {
            get => GetValue(ObjectToDisplayProperty);
            set { SetValue(ObjectToDisplayProperty, value); }
        }

        protected override StackPanel MainStackPanel => mainStackPanel;

        private static void MessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PropertyViewer)d;
            control.mainStackPanel.Children.Clear();
            if (e.NewValue == null)
                return;

            control.BindProperties(e.NewValue);
        }
    }
}
