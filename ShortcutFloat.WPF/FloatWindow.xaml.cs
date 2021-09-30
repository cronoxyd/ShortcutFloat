using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FloatWindow : Window
    {
        protected const double BUTTON_PADDING = 25;
        public ShortcutConfigurationViewModel ViewModel { get; set; }

        public FloatWindow()
        {
            InitializeComponent();

            ViewModel = new(new());

            ViewModel.ShortcutDefinitions.Add(new ShortcutDefinition(
                "Undo", 
                new KeystrokeDefinition("Undo", ModifierKeys.Control, Key.Z)
            ));

            ViewModel.ShortcutDefinitions.Add(new ShortcutDefinition(
                "Redo",
                new KeystrokeDefinition("Redo", ModifierKeys.Control, Key.Y)
            ));

            ViewModel.ShortcutDefinitions.Add(new ShortcutDefinition(
                "Brush",
                new KeystrokeDefinition("Brush", Key.B)
            ));

            ViewModel.ShortcutDefinitions.Add(new ShortcutDefinition(
                "Save",
                new KeystrokeDefinition("Save", ModifierKeys.Control, Key.S)
            ));

            ViewModel.Target = new() { ProcessName = "photo" };

            //if (def.Key == null) return;

            //var targetProcesses = Process.GetProcessesByName(ViewModel.Target.ProcessName);
            //var targetProcess = targetProcesses.FirstOrDefault();

            //if (targetProcess == null) return;

            //InteropServices.SetForegroundWindow(targetProcess.MainWindowHandle);
            //InteropServices.SetActiveWindow(targetProcess.MainWindowHandle);

            //var keyString = string.Join(
            //    string.Empty,
            //    (new string[] { def.ModifierKeys.ToSendKeysString(), def.Key.ToSendKeysString() }).NotNullOrEmpty()
            //);

            //SendKeys.SendWait(keyString);

            DataContext = ViewModel;
        }
    }
}
