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
        private bool FloatWindowActive => FloatWindow?.IsVisible ?? false;
        private ShortcutConfiguration ActiveConfiguration { get; set; } = null;
        private IntPtr? TargetWindowHandle { get; set; } = null;
        private static Process CurrentProcess { get; } = Process.GetCurrentProcess();
        private Rectangle MaxScreenBounds { get; } = GetMaxScreenBounds();
        private bool FloatWindowPositionSemaphone { get; set; } = false;

        protected static Rectangle GetMaxScreenBounds()
        {
            int? minLeft = null;
            int? minTop = null;
            int? maxRight = null;
            int? maxBottom = null;

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (minLeft == null || screen.Bounds.Left < minLeft) minLeft = screen.Bounds.Left;
                if (minTop == null || screen.Bounds.Top < minTop) minTop = screen.Bounds.Top;
                if (maxRight == null || screen.Bounds.Right > maxRight) maxRight = screen.Bounds.Right;
                if (maxBottom == null || screen.Bounds.Bottom > maxBottom) maxBottom = screen.Bounds.Bottom;
            }

            return new(
                minLeft.Value, minTop.Value,
                maxRight.Value - minLeft.Value, maxBottom.Value - minTop.Value
            );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            LoadSettings();

            EnvironmentMonitor.ForegroundWindowChanged += EnvironmentMonitor_ForegroundWindowChanged;
            EnvironmentMonitor.ForegroundWindowBoundsChanged += EnvironmentMonitor_ForegroundWindowBoundsChanged;

            EnvironmentMonitor.Start();
            TrayService.Start();

            TrayService.ShowSettings += TrayService_ShowSettings;
            TrayService.Quit += TrayService_Quit;
        }

        private void EnvironmentMonitor_ForegroundWindowBoundsChanged(object sender, EnvironmentMonitor.ForegroundWindowBoundsChangedEventArgs e)
        {
            if (!FloatWindowActive) return;
            if (!ActiveConfiguration?.StickyFloatWindow ?? !Settings.StickyFloatWindow) return;
            // Ignore own process
            if (EnvironmentMonitor.ForegroundWindowProcess.Id == CurrentProcess.Id) return;
            Dispatcher.Invoke(PositionFloatWindow);
        }

        private void PositionFloatWindow()
        {
            FloatWindowPositionSemaphone = true;
            FloatWindow.Left = Math.Clamp(
                EnvironmentMonitor.ForegroundWindowBounds.Value.X + ActiveConfiguration.FloatWindowOffset.Value.X,
                MaxScreenBounds.X,
                MaxScreenBounds.Right - FloatWindow.Width
            );
            FloatWindow.Top = Math.Clamp(
                EnvironmentMonitor.ForegroundWindowBounds.Value.Y + ActiveConfiguration.FloatWindowOffset.Value.Y,
                MaxScreenBounds.Y,
                MaxScreenBounds.Bottom - FloatWindow.Height
            );
            FloatWindowPositionSemaphone = false;
        }

        private void EnvironmentMonitor_ForegroundWindowChanged(object sender, EnvironmentMonitor.ForegroundWindowChangedEventArgs e)
        {
            // Ignore own process
            if (e.WindowProcess.Id == CurrentProcess.Id) return;

            ShortcutConfiguration matchingConfiguration = null;

            foreach (var config in Settings.ShortcutConfigurations)
            {
                if (!config.Enabled) continue;

                if (IsNotEmptyAndValidRegex(config.Target.WindowText))
                {
                    if (Regex.IsMatch(e.WindowText ?? string.Empty, config.Target.WindowText, RegexOptions.IgnoreCase))
                    {
                        matchingConfiguration = config;
                        break;
                    }
                }

                if (IsNotEmptyAndValidRegex(config.Target.ProcessName))
                {
                    if (Regex.IsMatch(e.WindowProcess.ProcessName ?? string.Empty, config.Target.ProcessName, RegexOptions.IgnoreCase))
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
                    CloseFloat();
            }
            else 
            {
                Dispatcher.Invoke(() =>
                {
                    CloseFloat();

                    FloatWindow = new(ActiveConfiguration);
                    FloatWindow.SendKeysRequested += FloatWindow_SendKeysRequested;
                    FloatWindow.LocationChanged += FloatWindow_LocationChanged;

                        PositionFloatWindow();

                    FloatWindow.Show();
                });

                if (TargetWindowHandle != null)
                    InteropServices.SetForegroundWindow(TargetWindowHandle.Value);
            }
        }

        private void CloseFloat()
        {
            if (FloatWindow == null) return;

            Dispatcher.Invoke(() =>
            {
                FloatWindow.Close();
                FloatWindow = null;
            });
        }

        private void FloatWindow_LocationChanged(object sender, EventArgs e)
        {
            Debug.Assert(ActiveConfiguration.FloatWindowOffset != PointF.Empty);
            if (FloatWindowPositionSemaphone) return;
            if (FloatWindow == null) return;
            Debug.WriteLine($"Float window position: {{{FloatWindow.Left}, {FloatWindow.Top}}}");
            Debug.WriteLine($"Foreground window bounds: {EnvironmentMonitor.ForegroundWindowBounds}");
            ActiveConfiguration.FloatWindowOffset = new PointF(
                (float)(FloatWindow.Left - EnvironmentMonitor.ForegroundWindowBounds.Value.X),
                (float)(FloatWindow.Top - EnvironmentMonitor.ForegroundWindowBounds.Value.Y)
            );
            Debug.WriteLine($"Float window offset changed: {ActiveConfiguration.FloatWindowOffset}");
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
            try
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
            } catch
            {
                MessageBox.Show("Failed to load settings", "Shortcut Float", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveSettings()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Settings, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                File.WriteAllText(SettingsFilePath, json);
            } catch
            {
                MessageBox.Show("Failed to save settings", "Shortcut Float", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TrayService_ShowSettings(object sender, EventArgs e)
        {
            if (SettingsForm == null)
            {
                SettingsForm = new(Settings);
                SettingsForm.Closed += (object sender, EventArgs e) => { SettingsForm = null; };
                SettingsForm.Show();
            }
            else
            {
                _ = SettingsForm.Activate();
            }
        }
    }
}
