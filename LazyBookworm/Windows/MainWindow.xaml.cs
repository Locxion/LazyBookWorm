using LazyBookworm.Common.Enums;
using LazyBookworm.Common.Models;
using LazyBookworm.Common.Models.Common;
using LazyBookworm.Database;
using LazyBookworm.Models.Common;
using LazyBookworm.Services;
using log4net;
using log4net.Config;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LazyBookworm.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(MainWindow));

        private readonly LazyBookWormContext _context;
        private readonly UserService _userService;

        private readonly Settings _settings;

        private List<UserAccount> _userAccounts;

        public MainWindow()
        {
            // Initialize Database Context
            _context = new LazyBookWormContextDesignFactory().CreateDbContext(null);
            // Initialize Services
            _userService = new UserService(_context);

            SetupLogger();

            //Load Settings
            _settings = SettingsService.LoadSettings();

            _logger.Info($"Start Program - Version: {typeof(MainWindow).Assembly.GetName().Version}");

            InitializeComponent();

            ParseSettingsToUi();
        }

        #region UiControls

        #region Settings Tab

        private void SaveSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            ParseSettingsFromUi();
            if (SettingsService.SaveSettings(_settings))
            {
                ShowSnackbar("Settings saved!");
            }
        }

        #endregion Settings Tab

        #region Users Tab

        private void UserTab_GotFocus(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
        }

        private void SearchUserAccounts_TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var input = SearchUserAccounts_TextBox.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var output = _userAccounts.Where(x => x.LoginDetails.Username.Contains(input) ||
                                                      x.Name.Contains(input) ||
                                                      x.Forename.Contains(input) ||
                                                      x.MailAddress.Contains(input) ||
                                                      x.BirthDate.ToLongDateString().ToString().Contains(input) ||
                                                      x.Address.Contains(input) ||
                                                      x.Country.Contains(input) ||
                                                      x.Phone.Contains(input))
                    .Select(x => new
                    {
                        x.IsSelected,
                        x.LoginDetails.Username,
                        x.Name,
                        x.Forename,
                        x.AccountCreation,
                        x.PermissionLevel,
                        x.MailAddress,
                        x.IsSuspended,
                        x.LastLogin
                    }).ToList();
                UserAccounts_DataGrid.ItemsSource = output;
            }
            else
            {
                RefreshUserList();
            }
        }

        private void AddUser_Button_OnClick(object sender, RoutedEventArgs e)
        {
            var loginDetails = new LoginDetails(UserLogin_TextBox.Text, UserPassword_PasswordBox.Password);

            var newUser = new UserAccount()
            {
                LoginDetails = loginDetails,
                AccountCreation = DateTime.UtcNow,
                LastLogin = null,
                PermissionLevel = Enum.Parse<PermissionLevel>(UserPermissions_Combobox.Text),
                Name = UserName_TextBox.Text,
                Forename = UserForename_TextBox.Text,
                Gender = Enum.Parse<Gender>(UserGender_Combobox.Text),
                BirthDate = UserBirthDay_DatePicker.DisplayDate,
                Address = UserAddress_TextBox.Text,
                Country = UserCountry_TextBox.Text,
                MailAddress = UserEmail_TextBox.Text,
                Phone = UserPhone_TextBox.Text,
                Notes = UserNotes_TextBox.Text
            };

            if (!CheckInputForNewUser(newUser))
            {
                _logger.Warn("Could not create new User. Input Values wrong or missing!");
                return;
            }

            var createResult = _userService.CreateUserAsync(newUser);
            if (!createResult.IsSuccess)
            {
                ShowSnackbar(createResult.Message);
            }
            else
            {
                ShowSnackbar("User was created!");
                DialogHost.Close("CreateUser_DialogHost");
            }

            RefreshUserList();
        }

        private void RefreshUserList()
        {
            _userAccounts = _userService.GetAll();

            UserAccounts_DataGrid.ItemsSource = _userAccounts.Select(x => new { x.IsSelected, x.LoginDetails.Username, x.Name, x.Forename, x.AccountCreation, x.PermissionLevel, x.MailAddress, x.IsSuspended, x.LastLogin });
        }

        private void DeleteUser_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserAccounts_DataGrid.SelectedItem == null)
            {
                ShowSnackbar("Please select a User from the List!");
                return;
            }

            var selectedIndex = UserAccounts_DataGrid.SelectedIndex;
            var users = _userService.GetAll();

            var selectedUser = users[selectedIndex];

            bool? result =
                new MessageBoxCustom(
                    $"Do you really want to Delete the User {selectedUser.Name}, {selectedUser.Forename} - {selectedUser.PermissionLevel}",
                    MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result == null || !result.Value)
            {
                return;
            }

            var deleteResult = _userService.DeleteUser(selectedUser);
            if (!deleteResult.IsSuccess)
            {
                ShowSnackbar(deleteResult.Message);
            }
            else
            {
                ShowSnackbar("User was deleted!");
                RefreshUserList();
            }
        }

        private void PasswordInputChanged(object sender, RoutedEventArgs e)
        {
            bool passwordsMatch = CheckPasswords(UserPassword_PasswordBox.Password, UserPasswordConfirm_PasswordBox.Password);
            if (!passwordsMatch)
            {
                HintAssist.SetHelperText(UserPasswordConfirm_PasswordBox, "Passwords don't match!");
                UserPassword_PasswordBox.Foreground = Brushes.Red;
                UserPasswordConfirm_PasswordBox.Foreground = Brushes.Red;
            }
            else
            {
                HintAssist.SetHelperText(UserPasswordConfirm_PasswordBox, "");
                UserPassword_PasswordBox.Foreground = Brushes.Black;
                UserPasswordConfirm_PasswordBox.Foreground = Brushes.Black;
            }
        }

        private void UserEmail_TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsValidEmail(UserEmail_TextBox.Text))
            {
                HintAssist.SetHelperText(UserEmail_TextBox, "Email not valid!");
                UserEmail_TextBox.Foreground = Brushes.Red;
            }
            else
            {
                HintAssist.SetHelperText(UserEmail_TextBox, "");
                UserEmail_TextBox.Foreground = Brushes.Black;
            }
        }

        /// <summary>
        /// Clears all the Control Values of CreateUser DialogHost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        private void CreateUser_DialogHost_OnDialogOpened(object sender, DialogOpenedEventArgs eventargs)
        {
            UserLogin_TextBox.Clear();
            UserPassword_PasswordBox.Clear();
            UserPasswordConfirm_PasswordBox.Clear();
            UserName_TextBox.Clear();
            UserForename_TextBox.Clear();
            UserPermissions_Combobox.SelectedIndex = 0;
            UserEmail_TextBox.Clear();
            UserAddress_TextBox.Clear();
            UserGender_Combobox.SelectedIndex = 0;
            UserBirthDay_DatePicker.SelectedDate = DateTime.UtcNow;
            UserNotes_TextBox.Clear();
            UserPhone_TextBox.Clear();
            UserCountry_TextBox.Clear();
        }

        #endregion Users Tab

        #region General

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
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
            _settings.DatabaseSettings.DatabaseHost = ServerAdress_TextBox.Text;
            _settings.DatabaseSettings.DatabaseName = DatabseName_TextBox.Text;
            _settings.DatabaseSettings.DatabaseUser = DatabseUser_TextBox.Text;
            _settings.DatabaseSettings.DatabasePassword = DatabasePassword_PasswordBox.Password;
        }

        /// <summary>
        /// Parses Settings to UI Controls.
        /// Has to be called AFTER InitializeComponent()
        /// </summary>
        private void ParseSettingsToUi()
        {
            ServerAdress_TextBox.Text = _settings.DatabaseSettings.DatabaseHost;
            DatabseName_TextBox.Text = _settings.DatabaseSettings.DatabaseName;
            DatabseUser_TextBox.Text = _settings.DatabaseSettings.DatabaseUser;
            DatabasePassword_PasswordBox.Password = _settings.DatabaseSettings.DatabasePassword;
        }

        /// <summary>
        /// Checks required Values like existing Login or valid Email Adress
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        private bool CheckInputForNewUser(UserAccount userAccount)
        {
            // Check for existing LoginName in Databse
            var existingUserAccount = _userService.GetUserAccountByLogin(userAccount.LoginDetails.Username);
            if (existingUserAccount != null)
            {
                HintAssist.SetHelperText(UserLogin_TextBox, "Username already taken!");
                UserLogin_TextBox.Foreground = Brushes.Red;
                return false;
            }

            HintAssist.SetHelperText(UserLogin_TextBox, "");
            UserLogin_TextBox.Foreground = Brushes.Black;

            if (!IsValidEmail(userAccount.MailAddress))
            {
                HintAssist.SetHelperText(UserEmail_TextBox, "Email not valid!");
                UserEmail_TextBox.Foreground = Brushes.Red;

                return false;
            }

            HintAssist.SetHelperText(UserEmail_TextBox, "");
            UserEmail_TextBox.Foreground = Brushes.Black;

            return true;
        }

        #endregion Methods

        #region Helper

        /// <summary>
        /// Sets Parameters for Logger etc ...
        /// </summary>
        private void SetupLogger()
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

            // SetupLogger Unhandled Exception Catch
            AppDomain.CurrentDomain.UnhandledException += (o, args) =>
            {
                _logger.Error($"Exception terminated Program: {args.IsTerminating}");
                _logger.Error(args.ExceptionObject);
            };
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckPasswords(string password1, string password2)
        {
            return password1 == password2;
        }

        #endregion Helper
    }
}