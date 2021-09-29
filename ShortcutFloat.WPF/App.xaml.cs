using ShortcutFloat.Common.Models;
using ShortcutFloat.WPF.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string SettingsFilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "cronoxyd\\ShortcutFloat",
            "settings.json"
        );

        public ShortcutFloatSettings Settings { get; set; } = new();
        private EnvironmentMonitor EnvironmentMonitor { get; } = new();
        private TrayService TrayService { get; } = new();
        public ShortcutFloatSettingsForm SettingsForm { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFilePath));

            if (File.Exists(SettingsFilePath))
                Settings = JsonSerializer.Deserialize<ShortcutFloatSettings>(File.ReadAllText(SettingsFilePath));

            EnvironmentMonitor.Start();

            TrayService.ShowSettings += TrayService_ShowSettings;
            TrayService.Quit += TrayService_Quit;
        }

        private void TrayService_Quit(object sender, EventArgs e)
        {
            TrayService.Close();
            Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(Settings));
        }

        private void TrayService_ShowSettings(object sender, EventArgs e)
        {
            SettingsForm = new(new(Settings));
            SettingsForm.Show();
        }
    }
}
