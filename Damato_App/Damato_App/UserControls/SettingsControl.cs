using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Damato_App.Settings;
using Newtonsoft.Json;
using System.IO;

namespace Damato_App.UserControls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl(ApplicationSettings applicationSettings)
        {
            ApplicationSettings = applicationSettings;
            InitializeComponent();
        }

        public ApplicationSettings ApplicationSettings;

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            checkBox1.Width = checkBox1.Height;
            checkBox1.Checked = ApplicationSettings.LoginSettings.KeepLogdIn;
        }

        public void update()
        {
            string json = JsonConvert.SerializeObject(ApplicationSettings);
            File.WriteAllText("ApplicationSettings.json", json);
            (this.Parent.Parent.Parent as MainForm).ApplicationSettings = ApplicationSettings;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ApplicationSettings.LoginSettings.KeepLogdIn = checkBox1.Checked;
            update();
        }
    }
}
