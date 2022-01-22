using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LazyBookworm.Models;
using LazyBookworm.Models.Common;
using log4net;
using Newtonsoft.Json;

namespace LazyBookworm.Services
{
    public static class SettingsService
    {
        private static ILog _logger = LogManager.GetLogger(typeof(SettingsService));

        /// <summary>
        /// Checks for Existing File and Loads it. Returns new Settings if no File is found
        /// </summary>
        /// <returns></returns>
        public static Settings LoadSettings()
        {
            if (File.Exists(Constants.SETTINGS_PATH))
            {
                return LoadSettings(Constants.SETTINGS_PATH);
            }

            return new Settings();
        }

        private static Settings LoadSettings(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                _logger.Error("Could not Load Settings from File!");
                _logger.Error(ex.Message);
                _logger.Error("Loading blank Settings instead.");
                return new Settings();
            }
        }

        /// <summary>
        /// Saves Settings to File
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(Settings settings)
        {
            try
            {
                File.WriteAllText(Constants.SETTINGS_PATH, JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            catch (Exception ex)
            {
                _logger.Error("Could not write to Settings File!");
                _logger.Error(ex.Message);
            }
        }
    }
}