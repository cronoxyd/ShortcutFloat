using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Triggers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Data;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutConfigurationViewModel : TypedViewModel<ShortcutConfiguration>
    {
        public ShortcutTarget Target { get => Model.Target; set => Model.Target = value; }
        public ObservableCollection<ShortcutDefinitionViewModel> ShortcutDefinitions { get; } = new();
        public ICollectionView ShortcutDefinitionsView { get; }
        public ShortcutDefinitionViewModel SelectedShortcutDefinition { get; set; } = null;
        public bool Enabled { get => Model.Enabled; set => Model.Enabled = value; }
        public bool? StickyFloatWindow { get => Model.StickyFloatWindow; set => Model.StickyFloatWindow = value; }
        public FloatWindowPositionReference? FloatWindowPositionReference { get => Model.FloatWindowPositionReference; set => Model.FloatWindowPositionReference = value; }
        public PointF FloatWindowAbsoluteOffset { get => Model.FloatWindowAbsoluteOffset; set => Model.FloatWindowAbsoluteOffset = value; }
        public PointF FloatWindowRelativeOffset { get => Model.FloatWindowRelativeOffset; set => Model.FloatWindowRelativeOffset = value; }
        public int? FloatWindowGridColumns { get => Model.FloatWindowGridColumns; set => Model.FloatWindowGridColumns = value; }
        public int? FloatWindowGridRows { get => Model.FloatWindowGridRows; set => Model.FloatWindowGridRows = value; }
        public bool IsDefaultConfiguration { get; set; } = false;

        public ICommand AddShortcutDefinitionCommand { get; }
        public ICommand EditShortcutDefinitionCommand { get; }
        public ICommand RemoveShortcutDefinitionCommand { get; }
        public ICommand MoveShortcutDefinitionUpCommand { get; }
        public ICommand MoveShortcutDefinitionDownCommand { get; }
        public ICommand NullFloatWindowPositionReference { get; }

        public event ShortcutDefinitionModelEventHandler NewShortcutDefinitionRequested = (sender, e) => { };
        public event ShortcutDefinitionModelEventHandler EditShortcutDefinitionRequested = (sender, e) => { };

        public ShortcutConfigurationViewModel(ShortcutConfiguration Model) : base(Model)
        {
            ShortcutDefinitions.AddRange(this.Model.ShortcutDefinitions.Select(def => new ShortcutDefinitionViewModel(def)));
            ShortcutDefinitions.CollectionChanged += ShortcutDefinitions_CollectionChanged;
            ShortcutDefinitionsView = CollectionViewSource.GetDefaultView(ShortcutDefinitions);

            AddShortcutDefinitionCommand = new RelayCommand(
                () =>
                {
                    var e = new ModelEventArgs<ShortcutDefinition>();
                    NewShortcutDefinitionRequested(this, e);
                    if (e.Model != null) ShortcutDefinitions.Add(new(e.Model));
                },
                () => true
            );

            EditShortcutDefinitionCommand = new RelayCommand(
                () =>
                {
                    var e = new ModelEventArgs<ShortcutDefinition>(SelectedShortcutDefinition.Model);
                    EditShortcutDefinitionRequested(this, e);
                    if (e.Model != null)
                    {
                        var vm = new ShortcutDefinitionViewModel(e.Model);
                        ShortcutDefinitions.Replace(SelectedShortcutDefinition, vm);
                        SelectedShortcutDefinition = vm;
                    }

                },
                () => SelectedShortcutDefinition != null
            );

            RemoveShortcutDefinitionCommand = new RelayCommand(
                () => ShortcutDefinitions.Remove(SelectedShortcutDefinition),
                () => SelectedShortcutDefinition != null
            );

            MoveShortcutDefinitionDownCommand = new RelayCommand(
                () =>
                {
                    var selectedIndex = ShortcutDefinitions.IndexOf(SelectedShortcutDefinition);
                    ShortcutDefinitions.Move(selectedIndex, selectedIndex + 1);
                },
                () =>
                {
                    if (SelectedShortcutDefinition == null) return false;
                    return ShortcutDefinitions.IndexOf(SelectedShortcutDefinition) < ShortcutDefinitions.Count - 1;
                }
            );

            MoveShortcutDefinitionUpCommand = new RelayCommand(
                () =>
                {
                    var selectedIndex = ShortcutDefinitions.IndexOf(SelectedShortcutDefinition);
                    ShortcutDefinitions.Move(selectedIndex, selectedIndex - 1);
                },
                () =>
                {
                    if (SelectedShortcutDefinition == null) return false;
                    return ShortcutDefinitions.IndexOf(SelectedShortcutDefinition) > 0;
                }
            );

            NullFloatWindowPositionReference = new RelayCommand(
                () => FloatWindowPositionReference = null,
                () => true
            );
        }

        private void ShortcutDefinitions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutDefinitions.Clear();
            Model.ShortcutDefinitions.AddRange(ShortcutDefinitions.Select(def => def.Model));
        }

        public delegate void ShortcutDefinitionModelEventHandler(object sender, ModelEventArgs<ShortcutDefinition> e);
    }
}
