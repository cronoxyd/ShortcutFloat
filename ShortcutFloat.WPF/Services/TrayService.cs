using ShortcutFloat.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutFloat.WPF.Services
{
    public class TrayService
    {
        private NotifyIcon icon { get; }
        private ContextMenuStrip iconContextMenuStrip { get; } 
        private ToolStripMenuItem settingsItem { get; }
        private ToolStripMenuItem quitItem { get; }

        public event EventHandler ShowSettings;
        public event EventHandler Quit;

        public TrayService()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.SetHighDpiMode(HighDpiMode.PerMonitor);

            icon = new();
            iconContextMenuStrip = new();
            settingsItem = new();
            quitItem = new();

            settingsItem.Text = "Settings";
            settingsItem.Click += SettingsItem_Click;
            iconContextMenuStrip.Items.Add(settingsItem);

            quitItem.Text = "Quit";
            quitItem.Click += QuitItem_Click;
            iconContextMenuStrip.Items.Add(quitItem);

            icon.ContextMenuStrip = iconContextMenuStrip;
            icon.Text = "Shortcut Float";
            icon.Icon = SystemIcons.Information;
            icon.DoubleClick += Icon_DoubleClick;
        }

        private void Icon_DoubleClick(object sender, EventArgs e) =>
            ShowSettings(this, new());

        private void QuitItem_Click(object sender, EventArgs e) =>
            Quit(this, new());

        private void SettingsItem_Click(object sender, EventArgs e) =>
            ShowSettings(this, new());

        public void Start() =>
            icon.Visible = true;

        public void Stop() =>
            icon.Dispose();
    }
}
