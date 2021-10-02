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
using AnyClone;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class ShortcutFloatSettingsForm : Window
    {
        public ShortcutFloatSettingsViewModel ViewModel { get; set; }

        public ShortcutFloatSettingsForm(ShortcutFloatSettings Model = null)
        {
            if (Model != null)
                ViewModel = new(Model);
            else
                ViewModel = new(new ShortcutFloatSettings());

            InitializeComponent();
            DataContext = ViewModel;

            ViewModel.NewConfigurationRequested += (sender, e) => e.Model = ShowShortcutConfigurationWindow();
            ViewModel.EditConfigurationRequested += (sender, e) => e.Model = ShowShortcutConfigurationWindow(e.Model);
        }

        private void EditDefaultConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var shrtCfgWin = new ShortcutConfigurationWindow(ViewModel.DefaultConfiguration.Model.Clone())
            {
                Owner = this
            };

            if (shrtCfgWin.ShowDialog() != true) return;
            ViewModel.DefaultConfiguration.Model = shrtCfgWin.ViewModel.Model;
        }

        private ShortcutConfiguration ShowShortcutConfigurationWindow(ShortcutConfiguration model = null)
        {
            var shrtCfgWin = new ShortcutConfigurationWindow(model?.Clone()) { Owner = this };

            if (shrtCfgWin.ShowDialog() != true)
                return null;
            else
                return shrtCfgWin.ViewModel.Model;
        }

        private void ConfigurationsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.EditConfigurationCommand.CanExecute(null))
                ViewModel.EditConfigurationCommand.Execute(null);
        }
    }
}
