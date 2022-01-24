using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LazyBookworm.Models;
using LazyBookworm.Models.Common;
using LazyBookworm.Services;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;

namespace LazyBookworm.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MainWindow));

        private readonly SnackbarMessageQueue _snackbarMessageQueue = new(new TimeSpan(0, 0, 5));
        public readonly Settings Settings;

        public MainWindow()
        {
            Setup();
            Settings = SettingsService.LoadSettings();

            _logger.Info($"Start Program - Version: {typeof(MainWindow).Assembly.GetName().Version}");

            InitializeComponent();
            ParseSettingsToUi();
        }

        #region UiControls

        #region Settings Tab

        private void SaveSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            ParseSettingsFromUi();
            if (SettingsService.SaveSettings(Settings))
            {
                ShowSnackbar("Settings saved!");
            }
        }

        #endregion Settings Tab

        #region General

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion General

        #region WindowStates

        private void AppCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AppMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void AppMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion WindowStates

        #endregion UiControls

        #region Methods

        /// <summary>
        /// Parses Values from UI Controls to Settings
        /// </summary>
        private void ParseSettingsFromUi()
        {
            Settings.DatabaseSettings.DatabaseHost = ServerAdress_TextBox.Text;
            Settings.DatabaseSettings.DatabaseName = DatabseName_TextBox.Text;
            Settings.DatabaseSettings.DatabaseUser = DatabseUser_TextBox.Text;
            Settings.DatabaseSettings.DatabasePassword = DatabasePassword_PasswordBox.Password;
        }

        /// <summary>
        /// Parses Settings to UI Controls.
        /// Has to be called AFTER InitializeComponent()
        /// </summary>
        private void ParseSettingsToUi()
        {
            ServerAdress_TextBox.Text = Settings.DatabaseSettings.DatabaseHost;
            DatabseName_TextBox.Text = Settings.DatabaseSettings.DatabaseName;
            DatabseUser_TextBox.Text = Settings.DatabaseSettings.DatabaseUser;
            DatabasePassword_PasswordBox.Password = Settings.DatabaseSettings.DatabasePassword;
        }

        #endregion Methods

        #region Helper

        /// <summary>
        /// Sets Parameters for Logger etc ...
        /// </summary>
        private void Setup()
        {
            // Initialize Logger
            var logFileName = Constants.LOG_PATH;
            GlobalContext.Properties["LogFile"] = logFileName;
            string s = new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "log4net.config")).LocalPath;
            XmlConfigurator.Configure(new FileInfo(s));

            // TODO ready for In App Console
            // var appender = new LazyBookwormAppender();
            // appender.OnMessageLogged += Logger_OnAppenderMessage;
            // ((IAppenderAttachable)((Hierarchy)LogManager.GetRepository()).Root).AddAppender(appender);

            // Setup Unhandled Exception Catch
            AppDomain.CurrentDomain.UnhandledException += (o, args) =>
            {
                _logger.Error($"Exception terminated Program: {args.IsTerminating}");
                _logger.Error(args.ExceptionObject);
            };
        }

        /// <summary>
        /// Shows a Snackbar at the bottom of the Screen
        /// </summary>
        /// <param name="message"></param>
        public void ShowSnackbar(string message)
        {
            //TODO Add ActionButton
            if (Snackbar.MessageQueue is { } messageQueue)
            {
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        #endregion Helper
    }
}