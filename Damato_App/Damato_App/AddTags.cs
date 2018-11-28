using Damato_App.DataBase;
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
    public partial class AddTags : Form
    {
        public AddTags()
        {
            InitializeComponent();
            label1.Text = TopText;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((sender as Button).Parent as TableLayoutPanel).Dispose();
        }

        private List<bool> temp1Text = new List<bool>() { true };
        private List<string> temp2Text = new List<string>() { "Add Tag" };

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            temp1Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())] = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() == "")
                temp1Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())] = true;
            if (temp1Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())])
            {
                (sender as TextBox).Text = temp2Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())];
                (sender as TextBox).Tag = null;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (temp1Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())])
                (sender as TextBox).Text = "";
        }

        public int Ccount = 0;
        public string TopText { get { return label1.Text; } set { label1.Text = value; } }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temp1Text.Add(false);
            temp2Text.Add("Add Tag");
            Ccount++;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTags));

            TableLayoutPanel xtableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            Button xbutton2 = new System.Windows.Forms.Button();
            Button xbutton3 = new System.Windows.Forms.Button();
            TextBox xtextBox1 = new System.Windows.Forms.TextBox();
            // 
            // tableLayoutPanel2
            // 
            xtableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            xtableLayoutPanel2.ColumnCount = 3;
            xtableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            xtableLayoutPanel2.Controls.Add(xtextBox1, 1, 0);
            xtableLayoutPanel2.Controls.Add(xbutton2, 2, 0);
            xtableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            xtableLayoutPanel2.Name = "tableLayoutPanel2";
            xtableLayoutPanel2.RowCount = 1;
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            xtableLayoutPanel2.Size = new System.Drawing.Size(441, 30);
            xtableLayoutPanel2.TabIndex = 11;
            // 
            // button2
            // 
            xbutton2.Cursor = System.Windows.Forms.Cursors.Hand;
            xbutton2.FlatAppearance.BorderSize = 0;
            xbutton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            xbutton2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            xbutton2.Location = new System.Drawing.Point(446, 6);
            xbutton2.Margin = new System.Windows.Forms.Padding(5);
            xbutton2.Name = "button2";
            xbutton2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            xbutton2.Size = new System.Drawing.Size(18, 18);
            xbutton2.TabIndex = 5;
            xbutton2.UseVisualStyleBackColor = true;
            xbutton2.Click += new System.EventHandler(button2_Click);
            // 
            // textBox1
            // 
            xtextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            xtextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            xtextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            xtextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            xtextBox1.ForeColor = System.Drawing.Color.DarkGray;
            xtextBox1.Location = new System.Drawing.Point(29, 7);
            xtextBox1.MaxLength = 20;
            xtextBox1.Name = "textBox1";
            xtextBox1.Size = new System.Drawing.Size(408, 16);
            xtextBox1.TabIndex = 7;
            xtextBox1.Text = "";
            xtextBox1.Enter += new System.EventHandler(textBox1_Enter);
            xtextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
            xtextBox1.Leave += new System.EventHandler(textBox1_Leave);
            xtextBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            flowLayoutPanel1.Controls.Add(xtableLayoutPanel2);
            xtableLayoutPanel2.Tag = Ccount;
            xtextBox1.Focus();
        }
        public List<string> vss = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> vs = new List<string>();
            foreach (var item in flowLayoutPanel1.Controls)
            {
                string s = ((item as TableLayoutPanel).Controls[0] as TextBox).Text;
                if (s.Trim() != "" && s.Trim() != temp2Text[Int32.Parse((item as TableLayoutPanel).Tag.ToString())])
                    vs.Add(s.Trim());
                else
                {
                    ((item as TableLayoutPanel).Controls[0] as TextBox).Focus();
                    return;
                }
            }
            vss = vs;
            this.Close();
        }

        public string Token { get; set; }
        private void AddTags_Load(object sender, EventArgs e)
        {
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                var sss = await API.GetPresetss(Token, "");
                foreach (var item in sss)
                {
                    comboBox1.Items.Add(item);
                }
                this.Cursor = Cursors.Default;
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in (comboBox1.SelectedItem as Presets).Feleds.Split('*'))
            {
                Ccount++;
                TableLayoutPanel tableLayoutPanel2xx = new TableLayoutPanel();
                TextBox textBox1xx = new TextBox();
                tableLayoutPanel2xx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                tableLayoutPanel2xx.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                tableLayoutPanel2xx.ColumnCount = 3;
                tableLayoutPanel2xx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                tableLayoutPanel2xx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tableLayoutPanel2xx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
                tableLayoutPanel2xx.Controls.Add(textBox1xx, 1, 0);
                tableLayoutPanel2xx.Location = new System.Drawing.Point(3, 3);
                tableLayoutPanel2xx.Name = "tableLayoutPanel2";
                tableLayoutPanel2xx.RowCount = 1;
                tableLayoutPanel2xx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tableLayoutPanel2xx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
                tableLayoutPanel2xx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
                tableLayoutPanel2xx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
                tableLayoutPanel2xx.Size = new System.Drawing.Size(441, 30);
                tableLayoutPanel2xx.TabIndex = 11;
                tableLayoutPanel2xx.Tag = Ccount;
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
                textBox1xx.Text = item;
                textBox1xx.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
                textBox1xx.Enter += new System.EventHandler(this.textBox1_Enter);
                textBox1xx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
                textBox1xx.Leave += new System.EventHandler(this.textBox1_Leave);
                this.flowLayoutPanel1.Controls.Add(tableLayoutPanel2xx);

                temp1Text.Add(false);
                temp2Text.Add(item);
                flowLayoutPanel1.Refresh();
            }
            
        }
    }
}
