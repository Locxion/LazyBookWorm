using System;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using LazyBookworm.Models;
using LazyBookworm.Models.Common;
using LazyBookworm.Services;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json.Linq;

namespace LazyBookworm.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MainWindow));

        public readonly Settings Settings;

        public MainWindow()
        {
            Setup();
            Settings = SettingsService.LoadSettings();

            _logger.Info($"Start Program - Version: {typeof(MainWindow).Assembly.GetName().Version}");

            InitializeComponent();
        }

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

        #endregion Helper
    }
}