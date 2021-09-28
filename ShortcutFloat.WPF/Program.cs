using ShortcutFloat.WPF.Services;
using System.Threading;
using System.Windows;

namespace ShortcutFloat.WPF
{
    public static class Program
    {
        public static TrayService TrayService { get; private set; }

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThread()]
        public static void Main()
        {
            TrayService = new();
            App.Main();

            while (true)
                Thread.Sleep(100);
        }
    }
}
