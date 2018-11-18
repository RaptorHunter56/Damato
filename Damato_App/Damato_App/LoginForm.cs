using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private bool temp2Text = true;

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            temp2Text = false;
            if (e.KeyCode == Keys.Enter)
                Login();
        }

        private void Login()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            MainForm main = new MainForm();
            this.Hide();
            main.ShowDialog();
            this.Close();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
                temp2Text = true;
            if (temp2Text)
                textBox2.Text = "Enter Password"; textBox2.UseSystemPasswordChar = false;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (temp2Text)
                textBox2.Text = ""; textBox2.UseSystemPasswordChar = true;
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
    }
}
