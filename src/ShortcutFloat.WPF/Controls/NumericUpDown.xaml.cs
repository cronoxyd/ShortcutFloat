using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShortcutFloat.WPF.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    [TemplatePart(Name = nameof(UpButtonElement), Type = typeof(RepeatButton))]
    [TemplatePart(Name = nameof(DownButtonElement), Type = typeof(RepeatButton))]
    public partial class NumericUpDown : UserControl
    {
        #region Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double?), typeof(NumericUpDown),
                new PropertyMetadata(new PropertyChangedCallback(NumericUpDown_ValueChanged)));

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Direct, typeof(ValueChangedEventHandler<double?>),
                typeof(NumericUpDown));

        public event ValueChangedEventHandler<double?> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public double? Value
        {
            get => (double?)GetValue(ValueProperty);
            set
            {
                if (value != null)
                {
                    if (value < Minimum) return;
                    if (value > Maximum) return;
                }
                SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region Minimum

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(new PropertyChangedCallback(NumericUpDown_MinimumChanged)));

        public static readonly RoutedEvent MinimumChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(MinimumChanged), RoutingStrategy.Direct, typeof(ValueChangedEventHandler<double>),
                typeof(NumericUpDown));

        public event ValueChangedEventHandler<double> MinimumChanged
        {
            add { AddHandler(MinimumChangedEvent, value); }
            remove { RemoveHandler(MinimumChangedEvent, value); }
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set
            {
                if (Value != null)
                    Value = Math.Max(Value.Value, Minimum);

                SetValue(MinimumProperty, value);
            }
        }

        #endregion

        #region Maximum

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(new PropertyChangedCallback(NumericUpDown_MaximumChanged)));

        public static readonly RoutedEvent MaximumChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(MaximumChanged), RoutingStrategy.Direct, typeof(ValueChangedEventHandler<double>), 
                typeof(NumericUpDown));

        public event ValueChangedEventHandler<double> MaximumChanged
        {
            add { AddHandler(MaximumChangedEvent, value); }
            remove { RemoveHandler(MaximumChangedEvent, value); }
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set
            {
                if (Value != null)
                    Value = Math.Min(Value.Value, Maximum);

                SetValue(MaximumProperty, value);
            }
        }

        #endregion

        #region NullPlaceholder

        public static readonly DependencyProperty NullPlaceholderProperty =
            DependencyProperty.Register(nameof(NullPlaceholder), typeof(string), typeof(NumericUpDown),
                new PropertyMetadata(new PropertyChangedCallback(NumericUpDown_NullPlaceholderChanged)));

        public static readonly RoutedEvent NullPlaceholderChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(NullPlaceholderChanged), RoutingStrategy.Direct, typeof(ValueChangedEventHandler<string>), 
                typeof(NumericUpDown));

        public event ValueChangedEventHandler<string> NullPlaceholderChanged
        {
            add { AddHandler(NullPlaceholderChangedEvent, value); }
            remove { RemoveHandler(NullPlaceholderChangedEvent, value); }
        }

        public string NullPlaceholder
        {
            get => (string)GetValue(NullPlaceholderProperty);
            set => SetValue(NullPlaceholderProperty, value);
        }

        #endregion

        #region ValueIsNull

        public static readonly RoutedEvent ValueIsNullChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(ValueIsNullChanged), RoutingStrategy.Direct, typeof(ValueChangedEventHandler<bool>),
                typeof(NumericUpDown));

        public event ValueChangedEventHandler<string> ValueIsNullChanged
        {
            add { AddHandler(ValueIsNullChangedEvent, value); }
            remove { RemoveHandler(ValueIsNullChangedEvent, value); }
        }

        public bool ValueIsNull { get => Value == null; }

        #endregion

        #region Elements

        private RepeatButton _upButtonElement;

        private RepeatButton UpButtonElement
        {
            get => _upButtonElement;
            set
            {
                if (_upButtonElement != null)
                    _upButtonElement.Click -= new RoutedEventHandler(_upButtonElement_Click);

                _upButtonElement = value;

                if (_upButtonElement != null)
                    _upButtonElement.Click += new RoutedEventHandler(_upButtonElement_Click);
            }
        }

        private RepeatButton _downButtonElement { get; set; }

        private RepeatButton DownButtonElement
        {
            get => _downButtonElement;
            set
            {
                if (_downButtonElement != null)
                    _downButtonElement.Click -= new RoutedEventHandler(_downButtonElement_Click);

                _downButtonElement = value;

                if (_downButtonElement != null)
                    _downButtonElement.Click += new RoutedEventHandler(_downButtonElement_Click);
            }
        }

        #endregion

        public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

        public NumericUpDown()
        {
            InitializeComponent();
            UpButton.Click += new RoutedEventHandler(_upButtonElement_Click);
            DownButton.Click += new RoutedEventHandler(_downButtonElement_Click);
        }

        private static void NumericUpDown_ValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown ctrl = (NumericUpDown)sender;
            double? oldValue = (double?)e.OldValue;
            double? newValue = (double?)e.NewValue;
            ValueChangedEventArgs<double?> routedEventArgs = new(ValueChangedEvent, newValue);
            ctrl.RaiseEvent(routedEventArgs);

            ValueChangedEventArgs<bool> valueIsNullRoutedEventArgs = new(ValueIsNullChangedEvent, ctrl.ValueIsNull);
            if (oldValue == null && newValue != null)
                ctrl.RaiseEvent(valueIsNullRoutedEventArgs);
            else if (oldValue != null && newValue == null)
                ctrl.RaiseEvent(valueIsNullRoutedEventArgs);

        }

        private static void NumericUpDown_MinimumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown ctrl = (NumericUpDown)sender;
            double newValue = (double)e.NewValue;
            ValueChangedEventArgs<double> routedEventArgs = new(MinimumChangedEvent, newValue);
            ctrl.RaiseEvent(routedEventArgs);
        }

        private static void NumericUpDown_MaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown ctrl = (NumericUpDown)sender;
            double newValue = (double)e.NewValue;
            ValueChangedEventArgs<double> routedEventArgs = new(MaximumChangedEvent, newValue);
            ctrl.RaiseEvent(routedEventArgs);
        }

        private static void NumericUpDown_NullPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown ctrl = (NumericUpDown)sender;
            string newValue = (string)e.NewValue;
            ValueChangedEventArgs<string> routedEventArgs = new(NullPlaceholderChangedEvent, newValue);
            ctrl.RaiseEvent(routedEventArgs);
        }

        private void _upButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (Value != null)
                Value++;
            else
                Value = Minimum;
        }

        private void _downButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (Value == Minimum)
                Value = null;
            else if (Value != null)
                Value--;
            else
                Value = Minimum;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpButtonElement = GetTemplateChild(nameof(UpButton)) as RepeatButton;
            DownButtonElement = GetTemplateChild(nameof(DownButton)) as RepeatButton;
        }
    }

    public class ValueChangedEventArgs<T> : RoutedEventArgs
    {
        public T NewValue { get; }
        public ValueChangedEventArgs(RoutedEvent routedEvent, T newValue) : base(routedEvent)
        {
            NewValue = newValue;
        }
    }
}
