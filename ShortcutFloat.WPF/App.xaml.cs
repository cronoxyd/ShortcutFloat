using PropertyChanged;
using ShortcutFloat.Common.Models;
using ShortcutFloat.WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class App : Application, INotifyPropertyChanged
    {
        public static string SettingsFilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "cronoxyd\\ShortcutFloat",
            "settings.json"
        );

        public ShortcutFloatSettings Settings { get; set; } = new();
        private EnvironmentMonitor EnvironmentMonitor { get; } = new();
        private TrayService TrayService { get; } = new();
        private ShortcutFloatSettingsForm SettingsForm { get; set; } = null;
        private FloatWindow FloatWindow { get; set; } = null;
        private ShortcutConfiguration ActiveConfiguration { get; set; } = null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFilePath));

            if (File.Exists(SettingsFilePath))
                Settings = JsonSerializer.Deserialize<ShortcutFloatSettings>(File.ReadAllText(SettingsFilePath));

            EnvironmentMonitor.ForegroundWindowChanged += EnvironmentMonitor_ForegroundWindowChanged;

            EnvironmentMonitor.Start();
            TrayService.Start();

            TrayService.ShowSettings += TrayService_ShowSettings;
            TrayService.Quit += TrayService_Quit;

            PropertyChanged += App_PropertyChanged;
        }

        private void App_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ActiveConfiguration):
                    {

                        break;
                    }
            }
        }

        private void EnvironmentMonitor_ForegroundWindowChanged(object sender, EnvironmentMonitor.ForegroundWindowChangedEventArgs e)
        {
            ShortcutConfiguration matchingConfiguration = null;

            foreach (var config in Settings.ShortcutConfigurations)
            {
                if (IsNotEmptyAndValidRegex(config.Target.WindowText))
                {
                    if (Regex.IsMatch(e.WindowText, config.Target.WindowText))
                    {
                        matchingConfiguration = config;
                        break;
                    }
                }

                if (IsNotEmptyAndValidRegex(config.Target.ProcessName))
                {
                    if (Regex.IsMatch(e.WindowProcess.ProcessName, config.Target.ProcessName))
                    {
                        matchingConfiguration = config;
                        break;
                    }
                }
            }
        }

        private bool IsNotEmptyAndValidRegex(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            try
            {

                _ = Regex.Match("", input);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void TrayService_Quit(object sender, EventArgs e)
        {
            TrayService.Stop();
            EnvironmentMonitor.Stop();
            Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(Settings));
        }

        private void TrayService_ShowSettings(object sender, EventArgs e)
        {
            if (SettingsForm == null)
            {
                SettingsForm = new(new(Settings));
                SettingsForm.Closed += (object sender, EventArgs e) => { SettingsForm = null; };
                SettingsForm.Show();
            } else
            {
                _ = SettingsForm.Activate();
            }
        }
    }
}
