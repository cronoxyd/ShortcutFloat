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

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class ShortcutFloatSettingsForm : Window
    {
        public ShortcutFloatSettingsViewModel ViewModel { get; set; } = new(null);

        public ShortcutFloatSettingsForm()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void EditDefaultConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var shrtCfgWin = new ShortcutConfigurationWindow()
            {
                Owner = this,
                ViewModel = ViewModel.DefaultConfiguration
            };

            if (shrtCfgWin.ShowDialog() != true) return;
            ViewModel.DefaultConfiguration.Model = shrtCfgWin.ViewModel.Model;
        }
    }
}
