using AnyClone;
using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ShortcutConfigurationWindow.xaml
    /// </summary>
    public partial class ShortcutConfigurationWindow : Window
    {
        public ShortcutConfigurationViewModel ViewModel { get; set; }

        public ShortcutConfigurationWindow(ShortcutConfiguration Model = null)
        {
            if (Model != null)
                ViewModel = new(Model);
            else
                ViewModel = new(new ShortcutConfiguration());

            InitializeComponent();
            DataContext = ViewModel;

            ViewModel.NewShortcutDefinitionRequested += (sender, e) => e.Model = ShowShortcutDefinitionWindow();
            ViewModel.EditShortcutDefinitionRequested += (sender, e) => e.Model = ShowShortcutDefinitionWindow(e.Model);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private ShortcutDefinition ShowShortcutDefinitionWindow(ShortcutDefinition m = null)
        {
            var mClone = m?.Clone();
            var win = new ShortcutDefinitionWindow(mClone) { Owner = this };

            if (win.ShowDialog() == true) 
                return win.ViewModel.Model;
            else 
                return null;
        }

        private void ShortcutDefinitionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.EditShortcutDefinitionCommand.CanExecute(null))
                ViewModel.EditShortcutDefinitionCommand.Execute(null);
        }
    }
}
