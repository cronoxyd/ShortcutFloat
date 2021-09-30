using ShortcutFloat.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Extensions;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class ShortcutFloatSettingsForm : Window
    {
        public ShortcutFloatSettingsViewModel ViewModel { get; set; }

        public ShortcutFloatSettingsForm(ShortcutFloatSettingsViewModel ViewModel = null)
        {
            if (ViewModel != null)
                this.ViewModel = ViewModel;
            else
                this.ViewModel = new(new ShortcutFloatSettings());

            InitializeComponent();
            DataContext = ViewModel;

            ViewModel.NewConfigurationRequested += (sender, e) =>
            {
                var newConfig = ShowShortcutConfigurationWindow(new(new()));
                if (newConfig != null) e.ViewModel = newConfig;
            };

            ViewModel.EditConfigurationRequested += (sender, e) =>
            {
                var editConfig = ShowShortcutConfigurationWindow(ViewModel.SelectedConfiguration);
                if (editConfig != null) e.ViewModel = editConfig;
            };
        }

        private void EditDefaultConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var shrtCfgWin = new ShortcutConfigurationWindow(ViewModel.DefaultConfiguration.DeepClone())
            {
                Owner = this
            };

            if (shrtCfgWin.ShowDialog() != true) return;
            ViewModel.DefaultConfiguration.Model = shrtCfgWin.ViewModel.Model;
        }

        private ShortcutConfigurationViewModel ShowShortcutConfigurationWindow(ShortcutConfigurationViewModel vm)
        {
            var shrtCfgWin = new ShortcutConfigurationWindow(vm.DeepClone())
            {
                Owner = this
            };

            if (shrtCfgWin.ShowDialog() != true)
                return null;
            else
                return shrtCfgWin.ViewModel;
        }
    }
}
