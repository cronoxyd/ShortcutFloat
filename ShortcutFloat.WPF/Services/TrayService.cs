using ShortcutFloat.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutFloat.WPF.Services
{
    public class TrayService
    {
        private NotifyIcon Icon { get; }
        private ContextMenuStrip IconContextMenuStrip { get; } 
        private ToolStripMenuItem SettingsItem { get; }
        private ToolStripMenuItem QuitItem { get; }

        public event EventHandler ShowSettings;
        public event EventHandler Quit;

        public TrayService()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.SetHighDpiMode(HighDpiMode.PerMonitor);

            Icon = new();
            IconContextMenuStrip = new();
            SettingsItem = new();
            QuitItem = new();

            SettingsItem.Text = "Settings";
            SettingsItem.Click += SettingsItem_Click;
            IconContextMenuStrip.Items.Add(SettingsItem);

            QuitItem.Text = "Quit";
            QuitItem.Click += QuitItem_Click;
            IconContextMenuStrip.Items.Add(QuitItem);

            Icon.ContextMenuStrip = IconContextMenuStrip;
            Icon.Text = "Shortcut Float";
            Icon.Icon = Properties.Resources.ShortcutFloatIcon;
            Icon.DoubleClick += Icon_DoubleClick;

            Icon.ShowBalloonTip(1000, "Shortcut Float", "Shortcut Float is running", ToolTipIcon.Info);
        }

        private void Icon_DoubleClick(object sender, EventArgs e) =>
            ShowSettings(this, new());

        private void QuitItem_Click(object sender, EventArgs e) =>
            Quit(this, new());

        private void SettingsItem_Click(object sender, EventArgs e) =>
            ShowSettings(this, new());

        public void Start() =>
            Icon.Visible = true;

        public void Stop() =>
            Icon.Dispose();
    }
}
