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
using System.Windows.Media.Animation;
using System.Reflection;
using System.Timers;
using Ato_Assist_System;
using IWshRuntimeLibrary;
using System.IO;

namespace ato_assist_system.window
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        MainWindow mainWindow = new MainWindow();
        Timer atimer = new Timer();

        public SplashScreen()
        {
            InitializeComponent();
            BuildLoadingContent(Main);
            AppLoadingInterval();
        }

        async void AppLoadingInterval()
        {
            if (!System.IO.File.Exists(AppConsts.appStartupShortcutPath))
                AppConsts.CreateStartupShortcut(()=>BuildLoadingContent(Main,loadingContent: "Creating Startup Shortcut..."));
            var registerSystem = await ServerCom.RegisterSystem(() => { });
            if (registerSystem == true) Console.WriteLine("System Registered"); else Console.WriteLine("System Not Registerd");
            BuildLoadingContent(Main);
            atimer.AutoReset = false;
            atimer.Interval = 5000;
            atimer.Elapsed += OpenMainWindow;
            atimer.Enabled = true;
        }

        void OpenMainWindow(Object source, ElapsedEventArgs e) => this.Dispatcher.Invoke(() =>
        {
            Visibility = Visibility.Hidden;
            mainWindow.Show();
        });

        void BuildLoadingContent(Grid parent, string loadingContent = "Loading Ato Assist System....")
        {
            parent.Children.Clear();

            TextBlock appVersion = new TextBlock()
            {
                Text = $"Version {AppConsts.appVersion}",
                Foreground = Brushes.Gray
            };

            Image logo = new Image()
            {
                Source = new BitmapImage(new Uri("../assets/icons/Send Request.png", UriKind.Relative)),
                Style = (Style)FindResource("AppLogo"),
                Name = "logo"
            };

            TextBlock text = new TextBlock()
            {
                Text = loadingContent,
                Style = (Style)FindResource("loadingTxt")
            };


            DoubleAnimation animation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, logo);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Image.OpacityProperty));

            storyboard.Children.Add(animation);
            storyboard.Begin();

            parent.Children.Add(appVersion);
            parent.Children.Add(logo);
            parent.Children.Add(text);

        }
    }
}
