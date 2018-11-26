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
                if (applicationSettings.SearchSettings == null)
                    applicationSettings.SearchSettings = new SearchSettings() { ReturnAmount = 10};
            }
            catch { applicationSettings = new ApplicationSettings() { LoginSettings = new LoginSettings(), SearchSettings = new SearchSettings() }; }
            if (applicationSettings.LoginSettings.KeepLogdIn)
            {
                //UpdateLogin(applicationSettings.LoginSettings.UserName, applicationSettings.LoginSettings.password, applicationSettings.LoginSettings.KeepLogdIn);
                Login();
            }
        }

        private bool temp2Text = true;

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            temp2Text = false;
            if (e.KeyCode == Keys.Enter)
            {
                applicationSettings.LoginSettings.UserName = textBox1.Text;
                applicationSettings.LoginSettings.password = textBox2.Text;
                applicationSettings.LoginSettings.KeepLogdIn = checkBox1.Checked;
                //UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
                Login();
                //First Time Fix
                //UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
            }
        }

        public ApplicationSettings applicationSettings;

        private void Login()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            this.Hide();

            //label4.Visible = true;
            this.Cursor = Cursors.WaitCursor;
            button1.Cursor = Cursors.WaitCursor;
            button1.Click -= new System.EventHandler(button1_Click);
            //button1.Click -= 
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                string json = JsonConvert.SerializeObject(applicationSettings);
                File.WriteAllText("ApplicationSettings.json", json);
                string token;
                try
                {
                    token = await API.GetNewToken(applicationSettings.LoginSettings.UserName, applicationSettings.LoginSettings.Password);
                }
                catch
                {
                    token = "";
                }
                this.Cursor = Cursors.Default;
                button1.Cursor = Cursors.Default;
                button1.Click += new System.EventHandler(button1_Click);
                if (token.Length == 10)
                {
                    MainForm main = new MainForm(token);
                    this.Hide();
                    main.Show();
                }
                else
                {
                    label4.Visible = true;
                    this.Show();
                }
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
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

        private void button1_Click(object sender, EventArgs e)
        {
            temp2Text = false;
            applicationSettings.LoginSettings.UserName = textBox1.Text;
            applicationSettings.LoginSettings.password = textBox2.Text;
            applicationSettings.LoginSettings.KeepLogdIn = checkBox1.Checked;
            //UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
            Login();
            //First Time Fix
            //UpdateLogin(textBox1.Text, textBox2.Text, checkBox1.Checked);
        }

        private void OpenLink(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/RaptorHunter56/Damato/");
        }
    }
}
