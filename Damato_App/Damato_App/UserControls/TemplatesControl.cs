using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damato_App.UserControls
{
    public partial class TemplatesControl : UserControl
    {
        public TemplatesControl()
        {
            InitializeComponent();
        }

        public string Token { get; set; }
        public Dictionary<Label, int> keyValuePairs = new Dictionary<Label, int>();

        private async void button7_ClickAsync(object sender, EventArgs e)
        {
            await API.DeletePresets(Token, keyValuePairs[(((sender as Button).Parent as TableLayoutPanel).Controls[0] as Label)].ToString(), "");
            ((sender as Button).Parent as TableLayoutPanel).Dispose();
        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            NewPlan();
        }

        private void NewPlan()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesControl));
            TableLayoutPanel tableLayoutPanel1x = new TableLayoutPanel();
            Button button7x = new Button();
            TextBox textBox1x = new TextBox();
            TextBox textBox1xx = new TextBox();
            // 
            // button7
            // 
            button7x.Cursor = System.Windows.Forms.Cursors.Hand;
            button7x.Dock = System.Windows.Forms.DockStyle.Right;

            button7x.FlatAppearance.BorderSize = 0;
            button7x.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button7x.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            button7x.Location = new System.Drawing.Point(611, 0);
            button7x.Margin = new System.Windows.Forms.Padding(0);
            button7x.Name = "button7";
            button7x.Size = new System.Drawing.Size(24, 30);
            button7x.TabIndex = 5;
            button7x.UseVisualStyleBackColor = true;
            button7x.Click += new System.EventHandler(this.button8_ClickAsync);
            // 
            // textBox1
            // 
            textBox1x.Anchor = System.Windows.Forms.AnchorStyles.None;
            textBox1x.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            textBox1x.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1x.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            textBox1x.ForeColor = System.Drawing.Color.DarkGray;
            textBox1x.Location = new System.Drawing.Point(19, 7);
            textBox1x.MaxLength = 20;
            textBox1x.Name = "textBox1";
            textBox1x.Size = new System.Drawing.Size(403, 16);
            textBox1x.TabIndex = 7;
            textBox1x.Text = "";
            // 
            // textBox1
            // 
            textBox1xx.Anchor = System.Windows.Forms.AnchorStyles.None;
            textBox1xx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            textBox1xx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1xx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            textBox1xx.ForeColor = System.Drawing.Color.DarkGray;
            textBox1xx.Location = new System.Drawing.Point(19, 7);
            textBox1xx.MaxLength = 20;
            textBox1xx.Name = "textBox1";
            textBox1xx.Size = new System.Drawing.Size(403, 16);
            textBox1xx.TabIndex = 7;
            textBox1xx.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1x.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            tableLayoutPanel1x.ColumnCount = 3;
            tableLayoutPanel1x.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tableLayoutPanel1x.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.67F));
            tableLayoutPanel1x.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1x.Controls.Add(textBox1x, 0, 0);
            tableLayoutPanel1x.Controls.Add(textBox1xx, 0, 0);
            tableLayoutPanel1x.Controls.Add(button7x, 2, 0);
            tableLayoutPanel1x.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1x.Location = new System.Drawing.Point(5, 5);
            tableLayoutPanel1x.Name = "tableLayoutPanel1";
            tableLayoutPanel1x.RowCount = 1;
            tableLayoutPanel1x.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1x.Size = new System.Drawing.Size(635, 30);
            tableLayoutPanel1x.TabIndex = 1;
        }

        private async void button8_ClickAsync(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesControl));
            string s = (((sender as Button).Parent as TableLayoutPanel).Controls[0] as TextBox).Text;
            string s2 = (((sender as Button).Parent as TableLayoutPanel).Controls[1] as TextBox).Text;
            ((sender as Button).Parent as TableLayoutPanel).Controls[0].Dispose();
            ((sender as Button).Parent as TableLayoutPanel).Controls[1].Dispose();

            Label label1x = new Label();
            Label label2x = new Label();

            // 
            // label1
            // 
            label1x.Dock = System.Windows.Forms.DockStyle.Fill;
            label1x.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label1x.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            label1x.ForeColor = System.Drawing.Color.DarkGray;
            label1x.Location = new System.Drawing.Point(3, 0);
            label1x.Name = "label1";
            label1x.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            label1x.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label1x.Size = new System.Drawing.Size(197, 30);
            label1x.TabIndex = 6;
            label1x.Text = "Passport";
            label1x.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2x.Dock = System.Windows.Forms.DockStyle.Fill;
            label2x.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label2x.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label2x.ForeColor = System.Drawing.Color.Gray;
            label2x.Location = new System.Drawing.Point(206, 0);
            label2x.Name = "label2";
            label2x.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            label2x.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label2x.Size = new System.Drawing.Size(401, 30);
            label2x.TabIndex = 7;
            label2x.Text = "Name*DOB*ID-No.";
            label2x.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


            ((sender as Button).Parent as TableLayoutPanel).Controls.Add(label1x, 0, 0);
            ((sender as Button).Parent as TableLayoutPanel).Controls.Add(label2x, 0, 0);

            int temp = await API.PostPresets(Token, "", new DataBase.Presets() { Name = (((sender as Button).Parent as TableLayoutPanel).Controls[1] as Label).Text, Feleds = (((sender as Button).Parent as TableLayoutPanel).Controls[2] as Label).Text });
            keyValuePairs.Add(((sender as Button).Parent as TableLayoutPanel).Controls[1] as Label, temp);
        }
    }
}
