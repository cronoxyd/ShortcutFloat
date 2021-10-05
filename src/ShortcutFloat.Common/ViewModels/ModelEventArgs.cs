using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.ViewModels
{
    public class ModelEventArgs<TModel> : EventArgs
        where TModel: class, new()
    {
        public TModel Model { get; set; } = null;

        public ModelEventArgs(TModel Model) =>
            this.Model = Model;

        public ModelEventArgs() { }
    }
}
