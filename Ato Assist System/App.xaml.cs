using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ato_Assist_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppConsts.CreateDataFolder();
            Guid guid = Guid.NewGuid();
            AppDomain.CurrentDomain.SetData("ApplicationId", guid.ToString());
        }
    }

}
