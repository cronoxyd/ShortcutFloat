using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.ViewModels;
using System.Windows;

namespace ShortcutFloat.WPF
{
    /// <summary>
    /// Interaction logic for ShortcutDefinitionWindow.xaml
    /// </summary>
    public partial class ShortcutDefinitionWindow : Window
    {
        public ShortcutDefinitionViewModel ViewModel { get; set; }

        public ShortcutDefinitionWindow(ShortcutDefinition Model)
        {
            if (Model != null)
                ViewModel = new(Model);
            else
                ViewModel = new(new());

            InitializeComponent();
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;

        private void CancelButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = false;
    }
}
