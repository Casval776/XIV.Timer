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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFXIV_TimerV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TransparencyValueBox.Foreground = Brushes.White;
        }

        private void TransSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var debug = CalculateTransparency(e.NewValue);
            Background = new SolidColorBrush(
                Color.FromArgb(
                    CalculateTransparency((int)e.NewValue),
                    0, 0, 0));
        }

        private static byte CalculateTransparency(double percent)
        {
            var debug = percent/100;
            return (byte) (256*(percent/100));
        }

        private void Window_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}
