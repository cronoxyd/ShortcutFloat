using AnyClone;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for ShortcutConfigurationWindow.xaml
    /// </summary>
    public partial class ShortcutConfigurationWindow : Window
    {
        public ShortcutConfigurationViewModel ViewModel { get; set; }

        public ShortcutConfigurationWindow(ShortcutConfiguration Model = null, bool IsDefaultConfiguration = false)
        {
            if (Model != null)
                ViewModel = new(Model);
            else
                ViewModel = new(new ShortcutConfiguration());

            ViewModel.IsDefaultConfiguration = IsDefaultConfiguration;

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
