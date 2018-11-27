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
                (sender as TextBox).Text = "Add Tag";
                (sender as TextBox).Tag = null;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (temp1Text[Int32.Parse(((sender as TextBox).Parent as TableLayoutPanel).Tag.ToString())])
                (sender as TextBox).Text = "";
        }

        public int Ccount = 1;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temp1Text.Add(false);
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
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            xtableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            xtableLayoutPanel2.Controls.Add(xbutton2, 2, 0);
            xtableLayoutPanel2.Controls.Add(xtextBox1, 1, 0);
            xtableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            xtableLayoutPanel2.Name = "tableLayoutPanel2";
            xtableLayoutPanel2.RowCount = 1;
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            xtableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            xtableLayoutPanel2.Size = new System.Drawing.Size(470, 30);
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
            xtextBox1.Text = "Add Tag";
            xtextBox1.Enter += new System.EventHandler(textBox1_Enter);
            xtextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
            xtextBox1.Leave += new System.EventHandler(textBox1_Leave);
            xtextBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            flowLayoutPanel1.Controls.Add(xtableLayoutPanel2);
        }
    }
}
