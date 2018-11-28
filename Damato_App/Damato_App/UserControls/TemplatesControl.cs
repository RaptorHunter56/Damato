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
            TableLayoutPanel xtableLayoutPanel2 = new TableLayoutPanel();
            TextBox xtextBox2 = new TextBox();
            TextBox xtextBox1 = new TextBox();
            Button xbutton2 = new Button();
            // 
            // tableLayoutPanel2
            // 
            xtableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            xtableLayoutPanel2.ColumnCount = 3;
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.67F));
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            xtableLayoutPanel2.Controls.Add(xtextBox2, 1, 0);
            xtableLayoutPanel2.Controls.Add(xtextBox1, 0, 0);
            xtableLayoutPanel2.Controls.Add(xbutton2, 2, 0);
            xtableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            xtableLayoutPanel2.Location = new System.Drawing.Point(5, 35);
            xtableLayoutPanel2.Name = "tableLayoutPanel2";
            xtableLayoutPanel2.RowCount = 1;
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            xtableLayoutPanel2.Size = new System.Drawing.Size(440, 30);
            xtableLayoutPanel2.TabIndex = 2;
            // 
            // textBox1
            // 
            xtextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            xtextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            xtextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            xtextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            xtextBox1.ForeColor = System.Drawing.Color.DarkGray;
            xtextBox1.Location = new System.Drawing.Point(3, 3);
            xtextBox1.MaxLength = 20;
            xtextBox1.Name = "textBox1";
            xtextBox1.Size = new System.Drawing.Size(134, 23);
            xtextBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            xtextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            xtextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            xtextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            xtextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            xtextBox2.ForeColor = System.Drawing.Color.DarkGray;
            xtextBox2.Location = new System.Drawing.Point(143, 7);
            xtextBox2.MaxLength = 20;
            xtextBox2.Name = "textBox2";
            xtextBox2.Size = new System.Drawing.Size(274, 23);
            xtextBox2.TabIndex = 9;
            // 
            // button2
            // 
            xbutton2.Cursor = System.Windows.Forms.Cursors.Hand;
            xbutton2.Dock = System.Windows.Forms.DockStyle.Right;
            xbutton2.FlatAppearance.BorderSize = 0;
            xbutton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xbutton2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            xbutton2.Location = new System.Drawing.Point(421, 0);
            xbutton2.Margin = new System.Windows.Forms.Padding(5);
            xbutton2.Name = "button2";
            xbutton2.Size = new System.Drawing.Size(30, 30);
            xbutton2.TabIndex = 5;
            xbutton2.UseVisualStyleBackColor = true;
            xbutton2.Click += new System.EventHandler(this.button2_Click);

            panel1.Controls.Add(xtableLayoutPanel2);
            xtableLayoutPanel2.BringToFront();
            xtextBox1.Focus();
        }

        private void button8_ClickAsync(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string s1 = keyValuePairs[((sender as Button).Parent as TableLayoutPanel).Controls[1] as Label].ToString();
            keyValuePairs.Remove(((sender as Button).Parent as TableLayoutPanel).Controls[1] as Label);
            TableLayoutPanel ss = ((sender as Button).Parent as TableLayoutPanel);
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                await API.DeletePresets(Token, s1, "");
                ss.Dispose();
                this.Cursor = Cursors.Default;
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string s1 = (((sender as Button).Parent as TableLayoutPanel).Controls[1] as TextBox).Text;
            string s2 = (((sender as Button).Parent as TableLayoutPanel).Controls[0] as TextBox).Text;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                int temp = await API.PostPresets(Token, "", new DataBase.Presets() { Name = s1, Feleds = s2 });
                keyValuePairs.Add(((sender as Button).Parent as TableLayoutPanel).Controls[1] as Label, temp);
                this.Cursor = Cursors.Default;
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesControl));
            // 
            // button7
            // 
            (sender as Button).Cursor = System.Windows.Forms.Cursors.Hand;
            (sender as Button).Dock = System.Windows.Forms.DockStyle.Right;
            (sender as Button).FlatAppearance.BorderSize = 0;
            (sender as Button).FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            (sender as Button).Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            (sender as Button).Location = new System.Drawing.Point(421, 0);
            (sender as Button).Margin = new System.Windows.Forms.Padding(5);
            (sender as Button).Name = "button7";
            (sender as Button).Size = new System.Drawing.Size(24, 30);
            (sender as Button).TabIndex = 5;
            (sender as Button).UseVisualStyleBackColor = true;
            (sender as Button).Click -= new System.EventHandler(this.button2_Click);
            (sender as Button).Click += new System.EventHandler(this.button8_ClickAsync);

            Label xlabel2 = new Label();
            Label xlabel1 = new Label();
            // 
            // label2
            // 
            xlabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            xlabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xlabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            xlabel2.ForeColor = System.Drawing.Color.Gray;
            xlabel2.Location = new System.Drawing.Point(143, 0);
            xlabel2.Name = "label2";
            xlabel2.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            xlabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            xlabel2.Size = new System.Drawing.Size(274, 30);
            xlabel2.TabIndex = 7;
            xlabel2.Text = s2;
            xlabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            xlabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            xlabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xlabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            xlabel1.ForeColor = System.Drawing.Color.DarkGray;
            xlabel1.Location = new System.Drawing.Point(3, 0);
            xlabel1.Name = "label1";
            xlabel1.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            xlabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            xlabel1.Size = new System.Drawing.Size(134, 30);
            xlabel1.TabIndex = 6;
            xlabel1.Text = s1;
            xlabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            (((sender as Button).Parent as TableLayoutPanel).Controls[1] as TextBox).Dispose();
            (((sender as Button).Parent as TableLayoutPanel).Controls[0] as TextBox).Dispose();
            ((sender as Button).Parent as TableLayoutPanel).Controls.Add(xlabel2, 1, 0);
            ((sender as Button).Parent as TableLayoutPanel).Controls.Add(xlabel1, 0, 0);
        }

        private void TemplatesControl_Load(object sender, EventArgs e)
        {
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                var sss = await API.GetPresetss(Token, "");
                foreach (var item in sss)
                {
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesControl));
                    TableLayoutPanel xtableLayoutPanel2 = new TableLayoutPanel();
                    Label xlabel2 = new Label();
                    Label xlabel1 = new Label();
                    Button xbutton2 = new Button();
                    // 
                    // tableLayoutPanel2
                    // 
                    xtableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
                    xtableLayoutPanel2.ColumnCount = 3;
                    xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
                    xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.67F));
                    xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                    xtableLayoutPanel2.Controls.Add(xlabel2, 1, 0);
                    xtableLayoutPanel2.Controls.Add(xlabel1, 0, 0);
                    xtableLayoutPanel2.Controls.Add(xbutton2, 2, 0);
                    xtableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
                    xtableLayoutPanel2.Location = new System.Drawing.Point(5, 35);
                    xtableLayoutPanel2.Name = "tableLayoutPanel2";
                    xtableLayoutPanel2.RowCount = 1;
                    xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    xtableLayoutPanel2.Size = new System.Drawing.Size(440, 30);
                    xtableLayoutPanel2.TabIndex = 2;
                    // 
                    // label2
                    // 
                    xlabel2.Dock = System.Windows.Forms.DockStyle.Fill;
                    xlabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    xlabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
                    xlabel2.ForeColor = System.Drawing.Color.Gray;
                    xlabel2.Location = new System.Drawing.Point(143, 0);
                    xlabel2.Name = "label2";
                    xlabel2.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
                    xlabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
                    xlabel2.Size = new System.Drawing.Size(274, 30);
                    xlabel2.TabIndex = 7;
                    xlabel2.Text = item.Feleds;
                    xlabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    // 
                    // label1
                    // 
                    xlabel1.Dock = System.Windows.Forms.DockStyle.Fill;
                    xlabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    xlabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                    xlabel1.ForeColor = System.Drawing.Color.DarkGray;
                    xlabel1.Location = new System.Drawing.Point(3, 0);
                    xlabel1.Name = "label1";
                    xlabel1.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
                    xlabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
                    xlabel1.Size = new System.Drawing.Size(134, 30);
                    xlabel1.TabIndex = 6;
                    xlabel1.Text = item.Name;
                    xlabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    keyValuePairs.Add(xlabel1, item.ID);
                    // 
                    // button2
                    // 
                    xbutton2.Cursor = System.Windows.Forms.Cursors.Hand;
                    xbutton2.Cursor = System.Windows.Forms.Cursors.Hand;
                    xbutton2.Dock = System.Windows.Forms.DockStyle.Right;
                    xbutton2.FlatAppearance.BorderSize = 0;
                    xbutton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    xbutton2.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
                    xbutton2.Location = new System.Drawing.Point(421, 0);
                    xbutton2.Margin = new System.Windows.Forms.Padding(5);
                    xbutton2.Name = "button7";
                    xbutton2.Size = new System.Drawing.Size(24, 30);
                    xbutton2.TabIndex = 5;
                    xbutton2.UseVisualStyleBackColor = true;
                    xbutton2.Click -= new System.EventHandler(this.button2_Click);
                    xbutton2.Click += new System.EventHandler(this.button8_ClickAsync);

                    panel1.Controls.Add(xtableLayoutPanel2);
                    xtableLayoutPanel2.BringToFront();
                }
                this.Cursor = Cursors.Default;
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }
    }
}
