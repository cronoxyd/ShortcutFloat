using Newtonsoft.Json;
using PropertyChanged;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Runtime;
using ShortcutFloat.WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private ShortcutFloatSettingsForm SettingsForm { get; set; } = null;
        private FloatWindow FloatWindow { get; set; }
        private ShortcutConfiguration ActiveConfiguration { get; set; } = null;
        private IntPtr? TargetWindowHandle { get; set; } = null;
        private Process CurrentProcess => Process.GetCurrentProcess();

        protected override void OnStartup(StartupEventArgs e)
        {
            LoadSettings();

            EnvironmentMonitor.ForegroundWindowChanged += EnvironmentMonitor_ForegroundWindowChanged;

            EnvironmentMonitor.Start();
            TrayService.Start();

            TrayService.ShowSettings += TrayService_ShowSettings;
            TrayService.Quit += TrayService_Quit;
        }

        private void EnvironmentMonitor_ForegroundWindowChanged(object sender, EnvironmentMonitor.ForegroundWindowChangedEventArgs e)
        {
            // Ignore own process
            if (e.WindowProcess.ProcessName == CurrentProcess.ProcessName) return;

            ShortcutConfiguration matchingConfiguration = null;

            foreach (var config in Settings.ShortcutConfigurations)
            {
                if (!config.Enabled) continue;

                if (IsNotEmptyAndValidRegex(config.Target.WindowText))
                {
                    if (Regex.IsMatch(e.WindowText, config.Target.WindowText, RegexOptions.IgnoreCase))
                    {
                        matchingConfiguration = config;
                        break;
                    }
                }

                if (IsNotEmptyAndValidRegex(config.Target.ProcessName))
                {
                    if (Regex.IsMatch(e.WindowProcess.ProcessName, config.Target.ProcessName, RegexOptions.IgnoreCase))
                    {
                        matchingConfiguration = config;
                        break;
                    }
                }
            }

            if (matchingConfiguration != null)
                TargetWindowHandle = e.WindowHandle;

            ActiveConfiguration = matchingConfiguration;
            OnActiveConfigurationChanged();
        }

        private void OnActiveConfigurationChanged()
        {
            if (ActiveConfiguration == null)
            {
                if (FloatWindow != null)
                    Dispatcher.Invoke(() => {
                        FloatWindow.Close();
                        FloatWindow = null;
                    });
            }
            else if (FloatWindow == null)
            {
                Dispatcher.Invoke(() =>
                {
                    FloatWindow = new(ActiveConfiguration);
                    FloatWindow.SendKeysRequested += FloatWindow_SendKeysRequested;
                    FloatWindow.LocationChanged += FloatWindow_LocationChanged;

                    if (ActiveConfiguration.FloatWindowLocation != null)
                    {
                        FloatWindow.Left = ActiveConfiguration.FloatWindowLocation.Value.X;
                        FloatWindow.Top = ActiveConfiguration.FloatWindowLocation.Value.Y;
                    }

                    FloatWindow.Show();
                });

                if (TargetWindowHandle != null)
                    InteropServices.SetForegroundWindow(TargetWindowHandle.Value);
            }
        }

        private void FloatWindow_LocationChanged(object sender, EventArgs e)
        {
            if (FloatWindow == null) return;
            ActiveConfiguration.FloatWindowLocation = new PointF((float)FloatWindow.Left, (float)FloatWindow.Top);
        }

        private void FloatWindow_SendKeysRequested(object sender, Common.ViewModels.SendKeysEventArgs e)
        {
            if (TargetWindowHandle == null) return;
            InteropServices.SetForegroundWindow(TargetWindowHandle.Value);
            System.Windows.Forms.SendKeys.SendWait(e.SendKeysString);
        }

        private static bool IsNotEmptyAndValidRegex(string input)
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
            SaveSettings();
        }

        private void LoadSettings()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFilePath));

            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);

                if (File.Exists(SettingsFilePath))
                    Settings = JsonConvert.DeserializeObject<ShortcutFloatSettings>(json, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
            }
        }

        public void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(Settings, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(SettingsFilePath, json);
        }

        private void TrayService_ShowSettings(object sender, EventArgs e)
        {
            if (SettingsForm == null)
            {
                SettingsForm = new(Settings);
                SettingsForm.Closed += (object sender, EventArgs e) => { SettingsForm = null; };
                SettingsForm.Show();
            } else
            {
                _ = SettingsForm.Activate();
            }
        }
    }
}
