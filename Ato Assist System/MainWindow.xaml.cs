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
using Newtonsoft.Json;
using System.Windows.Media.Animation;
using System.Reflection;
using Ato_Assist_System.window;

namespace Ato_Assist_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QrWindow qrWindow = new QrWindow();

        public MainWindow()
        {
            InitializeComponent();
            version.Text = $"Ato Assist V{AppConsts.appVersion}";
            RegisterBtnContent();
        }

        void RegisterBtnContent()
        {
            if(SystemSpec.id != null)
            {
                RegisterImg.Source = new BitmapImage(new Uri("assets/icons/check.png", UriKind.Relative));
                RegisterBtn.Text = "System Registerd";

            }
            else
            {
                RegisterImg.Source = new BitmapImage(new Uri("assets/icons/Register.png", UriKind.Relative));
                RegisterBtn.Text = "Register System";
            }                

        }

        private void CloseWindow(object sender, MouseButtonEventArgs e) =>Application.Current.Shutdown();

        private void DragWindows(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void RegisterSystem(object sender, MouseButtonEventArgs e)
        {
            RegisterBtn.Text = "Registring System ...";
            if (SystemSpec.id != null) {
                RegisterBtnContent();
                return;
            }
            ServerCom.RegisterSystem(RegisterBtnContent);
        }

        private void OpenQrCode(object sender, MouseButtonEventArgs e)
        {
            if (SystemSpec.id == null) return;
            qrWindow.QRIMG.Source = new BitmapImage(new Uri(AppConsts.qrImgFileName, UriKind.Absolute));
            switch (qrWindow.Visibility)
            {
                case Visibility.Hidden:
                    qrWindow.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    qrWindow.Visibility = Visibility.Hidden;
                    break;
                case Visibility.Collapsed:
                    qrWindow.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
