using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.ViewModels;
using System;
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
        public event EventHandler<ShortcutDefinitionInvocation> ShortcutInvokeRequested = (sender, e) => { };

        public FloatWindow(ShortcutConfiguration model)
        {
            if (model != null)
                ViewModel = new(model);
            else
                throw new ArgumentNullException(nameof(model));

            InitializeComponent();
            DataContext = ViewModel;

            foreach (var def in ViewModel.ShortcutDefinitions)
                def.ShortcutInvokeRequested += (sender, e) => ShortcutInvokeRequested(this, e);
        }
    }
}
