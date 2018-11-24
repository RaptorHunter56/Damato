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
            _ApplicationSettings = applicationSettings;
            if (_ApplicationSettings.SearchSettings == null)
                _ApplicationSettings.SearchSettings = new SearchSettings() { ReturnAmount = 10 };
            InitializeComponent();
            pictureBox1.Height = pictureBox1.Width;
            numericUpDown1.Controls.RemoveAt(0);
        }

        public bool checkBox1checked;
        public bool checkBox1Checked
        {
            get
            {
                return checkBox1checked;
            }
            set
            {
                checkBox1checked = value;
                if (checkBox1checked)
                    pictureBox1.Image = global::Damato_App.Properties.Resources.icons8_Toggle_On_32px_1;
                else
                    pictureBox1.Image = global::Damato_App.Properties.Resources.icons8_Toggle_Off_32px_1;
            }
        }

        public ApplicationSettings _ApplicationSettings;

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            checkBox1Checked = _ApplicationSettings.LoginSettings.KeepLogdIn;
            numericUpDown1.Value = _ApplicationSettings.SearchSettings.ReturnAmount;
        }

        public void update()
        {
            string json = JsonConvert.SerializeObject(_ApplicationSettings);
            File.WriteAllText("ApplicationSettings.json", json);
            (this.Parent.Parent.Parent as MainForm).ApplicationSettings = _ApplicationSettings;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _ApplicationSettings.LoginSettings.KeepLogdIn = checkBox1Checked;
            update();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            checkBox1Checked = !checkBox1Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _ApplicationSettings.SearchSettings.ReturnAmount = Int32.Parse(numericUpDown1.Value.ToString());
        }
    }
}
