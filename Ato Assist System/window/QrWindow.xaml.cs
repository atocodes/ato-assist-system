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
using System.IO;

namespace Ato_Assist_System.window
{
    /// <summary>
    /// Interaction logic for QrWindow.xaml
    /// </summary>
    public partial class QrWindow : Window
    {
        public QrWindow()
        {
            InitializeComponent();
        }

        void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e) => Visibility = Visibility.Hidden;
    }
}
