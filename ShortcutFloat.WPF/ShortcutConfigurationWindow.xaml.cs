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
    /// Interaction logic for ShortcutConfigurationWindow.xaml
    /// </summary>
    public partial class ShortcutConfigurationWindow : Window
    {
        public ShortcutConfigurationViewModel ViewModel { get; set; } = new(null);

        public ShortcutConfigurationWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
