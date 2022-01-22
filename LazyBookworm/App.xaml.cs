using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LazyBookworm.Windows;
using log4net;

namespace LazyBookworm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var window = new MainWindow();
                window.Show();
            }
            catch (Exception exception)
            {
                var logger = LogManager.GetLogger(typeof(App));
                logger.Error($"Error while initializing {nameof(MainWindow)}: {exception.Message}", exception);
                logger.Error(exception.Message, exception);
            }
        }
    }
}