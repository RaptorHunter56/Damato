using Damato_App.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damato_App
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            try
            {
                string json = File.ReadAllText("ApplicationSettings.json");
                applicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
            }
            catch { applicationSettings = new ApplicationSettings() { LoginSettings = new LoginSettings()}; }
            if (applicationSettings.LoginSettings.KeepLogdIn)
            {
                UpdateLogin(applicationSettings.LoginSettings.UserName, applicationSettings.LoginSettings.password, applicationSettings.LoginSettings.KeepLogdIn);
                Login();
            }
        }

        private bool temp2Text = true;

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            temp2Text = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (applicationSettings.LoginSettings.UserName == textBox1.Text && applicationSettings.LoginSettings.password == textBox2.Text)
                {
                    UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
                    Login();
                }
                else
                {
                    label4.Visible = true;
                }
                //First Time Fix
                //UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
            }
        }

        public ApplicationSettings applicationSettings;

        private void Login()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            MainForm main = new MainForm();
            this.Hide();
            main.ShowDialog();
            try
            {
                this.Show();
            }
            catch { }
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void UpdateLogin(string UserName, string password, bool KeepLogdIn)
        {
            applicationSettings = new ApplicationSettings()
            {
                LoginSettings = new LoginSettings()
                {
                    UserName = UserName,
                    password = password,
                    KeepLogdIn = KeepLogdIn
                }
            };
            string json = JsonConvert.SerializeObject(applicationSettings);
            File.WriteAllText("ApplicationSettings.json", json);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
                temp2Text = true;
            if (temp2Text)
            {
                textBox2.Text = "Enter Password";
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (temp2Text)
            {
                textBox2.Text = "";
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private bool temp1Text = true;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            temp1Text = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                temp1Text = true;
            if (temp1Text)
                textBox1.Text = "Enter User";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (temp1Text)
                textBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
