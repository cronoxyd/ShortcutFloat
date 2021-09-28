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
        private NotifyIcon icon { get; } = new();
        private ContextMenuStrip iconContextMenuStrip { get; } = new();
        private ToolStripMenuItem settingsItem { get; } = new();
        private ToolStripMenuItem quitItem { get; } = new();

        public TrayService()
        {
            settingsItem.Text = "Settings";
            settingsItem.Click += SettingsItem_Click;

            quitItem.Text = "Quit";
            quitItem.Click += QuitItem_Click;
            iconContextMenuStrip.Items.Add(quitItem);

            icon.ContextMenuStrip = iconContextMenuStrip;


            icon.Text = "Shortcut Float";
            icon.Visible = true;
            icon.Icon = SystemIcons.Information;
            icon.DoubleClick += SettingsItem_Click;
        }

        private void QuitItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void SettingsItem_Click(object sender, EventArgs e)
        {
            var SettingsForm = new ShortcutFloatSettingsForm();
            SettingsForm.Show();
        }
    }
}
