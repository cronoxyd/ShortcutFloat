using Newtonsoft.Json;
using PropertyChanged;
using ShortcutFloat.Common.Extensions;
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
        private Rectangle MaxScreenBounds { get; } = GetMaxScreenBounds();
        private bool FloatWindowPositionSemaphore { get; set; } = false;

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
            if (EnvironmentMonitor.ForegroundWindowProcess.Id == EnvironmentMonitor.CurrentProcess.Id) return;

            Debug.WriteLine($"Foreground window bounds changed:\n\t{e.WindowBounds}\n\t(Δ {e.Delta})");
            
            Dispatcher.Invoke(PositionFloatWindow);
        }

        private void PositionFloatWindow()
        {
            FloatWindowPositionSemaphore = true;

            if (EnvironmentMonitor.ForegroundWindowBounds == null)
            {
                Debug.Fail($"{nameof(EnvironmentMonitor.ForegroundWindowBounds)} unexpectedly null");
                return;
            }

            PointF? newPosition = null;
            FloatWindowPositionReference positionReference = ActiveConfiguration.FloatWindowPositionReference == null ? Settings.FloatWindowPositionReference : ActiveConfiguration.FloatWindowPositionReference.Value;

            switch (positionReference)
            {
                default:
                case FloatWindowPositionReference.Absolute:
                    {
                        newPosition = new(
                            EnvironmentMonitor.ForegroundWindowBounds.Value.X + ActiveConfiguration.FloatWindowAbsoluteOffset.X,
                            EnvironmentMonitor.ForegroundWindowBounds.Value.Y + ActiveConfiguration.FloatWindowAbsoluteOffset.Y
                        );
                        break;
                    }
                case FloatWindowPositionReference.Relative:
                    {
                        PointF ForegroundWindowRelativeCenter = new(
                            EnvironmentMonitor.ForegroundWindowBounds.Value.X + (EnvironmentMonitor.ForegroundWindowBounds.Value.Width / 2),
                            EnvironmentMonitor.ForegroundWindowBounds.Value.Y + (EnvironmentMonitor.ForegroundWindowBounds.Value.Height / 2)
                        );

                        //PointF newFloatWindowRelativeCenter = ForegroundWindowRelativeCenter.Add(ActiveConfiguration.FloatWindowRelativeOffset);
                        //PointF newFloatWindowCenter = GetAbsoluteScreenPosition(newFloatWindowRelativeCenter);

                        //newPosition = new(
                        //    (float)(newFloatWindowCenter.X - (FloatWindow.Width / 2)),
                        //    (float)(newFloatWindowCenter.Y - (FloatWindow.Height / 2))
                        //);

                        newPosition = ForegroundWindowRelativeCenter.Add(ActiveConfiguration.FloatWindowRelativeOffset);

                        break;
                    }
            }


            FloatWindow.Left = Math.Clamp(
                newPosition.Value.X,
                MaxScreenBounds.X,
                MaxScreenBounds.Right - FloatWindow.Width
            );
            FloatWindow.Top = Math.Clamp(
                newPosition.Value.Y,
                MaxScreenBounds.Y,
                MaxScreenBounds.Bottom - FloatWindow.Height
            );


            FloatWindowPositionSemaphore = false;
        }

        private void EnvironmentMonitor_ForegroundWindowChanged(object sender, EnvironmentMonitor.ForegroundWindowChangedEventArgs e)
        {
            // Ignore own process
            if (e.WindowProcess.Id == EnvironmentMonitor.CurrentProcess.Id) return;
            // Ignore "Idle" process
            if (e.WindowProcess.Id == 0) return;
            Debug.WriteLine($"Foreground window changed\n\tWindow text:\t\"{e.WindowText}\"\n\tProcess:\t\t\"{e.WindowProcess.ProcessName}\" ({e.WindowProcess.Id})");

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

                if (EnvironmentMonitor.ForegroundWindowHandle != null)
                    InteropServices.SetForegroundWindow(EnvironmentMonitor.ForegroundWindowHandle.Value);
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
            if (FloatWindowPositionSemaphore) return;
            if (FloatWindow == null) return;
            if (EnvironmentMonitor.ForegroundWindowBounds == null)
            {
                Debug.Fail($"{nameof(EnvironmentMonitor.ForegroundWindowBounds)} unexpectedly null");
                return;
            }

            Debug.WriteLine($"Float window position: {{{FloatWindow.Left}, {FloatWindow.Top}}}");
            Debug.WriteLine($"Foreground window bounds: {EnvironmentMonitor.ForegroundWindowBounds}");

            PointF FloatWindowPosition = new((float)FloatWindow.Left, (float)FloatWindow.Top);

            // Absolute

            PointF AbsoluteOffset = FloatWindowPosition.Subtract(EnvironmentMonitor.ForegroundWindowBounds.Value.Location);

            ActiveConfiguration.FloatWindowAbsoluteOffset = AbsoluteOffset;

            // Relative

            //PointF FloatWindowRelativeCenter = GetRelativeScreenPosition(new(
            //    (float)(FloatWindow.Left + (FloatWindow.Width / 2)), 
            //    (float)(FloatWindow.Top + (FloatWindow.Height / 2))
            //));

            PointF ForegroundWindowCenter = new(
                EnvironmentMonitor.ForegroundWindowBounds.Value.X + (EnvironmentMonitor.ForegroundWindowBounds.Value.Width / 2),
                EnvironmentMonitor.ForegroundWindowBounds.Value.Y + (EnvironmentMonitor.ForegroundWindowBounds.Value.Height / 2)
            );

            PointF RelativeOffset = FloatWindowPosition.Subtract(ForegroundWindowCenter);

            ActiveConfiguration.FloatWindowRelativeOffset = RelativeOffset;

            Debug.WriteLine($"Float window offset changed:\n\tAbsolute: {AbsoluteOffset}\n\tRelative: {RelativeOffset}");
        }

        public PointF GetRelativeScreenPosition(PointF input) =>
            new(
                (float)map(input.X, MaxScreenBounds.Left, MaxScreenBounds.Right, 0, 1),
                (float)map(input.Y, MaxScreenBounds.Top, MaxScreenBounds.Bottom, 0, 1)
            );

        public PointF GetAbsoluteScreenPosition(PointF input) =>
            new(
                (float)map(input.X, 0, 1, MaxScreenBounds.Left, MaxScreenBounds.Right),
                (float)map(input.Y, 0, 1, MaxScreenBounds.Top, MaxScreenBounds.Bottom)
            );

        static long map(long x, long in_min, long in_max, long out_min, long out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        static double map(double x, double in_min, double in_max, double out_min, double out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        private void FloatWindow_SendKeysRequested(object sender, Common.ViewModels.SendKeysEventArgs e)
        {
            if (EnvironmentMonitor.ForegroundWindowHandle == null) return;
            InteropServices.SetForegroundWindow(EnvironmentMonitor.ForegroundWindowHandle.Value);
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
            }
            catch
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
            }
            catch
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
