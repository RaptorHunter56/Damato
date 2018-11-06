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
    public partial class Form1 : Form
    {
        int panelWidth;
        bool Hidden;
        public Form1()
        {
            InitializeComponent();
            panelWidth = 150;
            Hidden = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                PanelSlide.Width = PanelSlide.Width + 10;
                if (PanelSlide.Width >= panelWidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                PanelSlide.Width = PanelSlide.Width - 10;
                if (PanelSlide.Width <= 0)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            File file = new File()
            {
                Path = FileList.FirstOrDefault()
            };

            //// 
            //// fileDisplay1
            //// 
            //FileDisplay fileDisplay = new FileDisplay(file);
            //fileDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            //fileDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            //fileDisplay.Location = new System.Drawing.Point(0, 0);
            //fileDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            //fileDisplay.Name = "fileDisplay1";
            ////fileDisplay.ProfilePic = ((System.Drawing.Image)(resources.GetObject("fileDisplay1.ProfilePic")));
            //fileDisplay.Size = new System.Drawing.Size(465, 28);
            //fileDisplay.TabIndex = 0;
            ////fileDisplay.txtName = "css.Image.png";
            ////fileDisplay.txtPath = "...pp\\img\\FileType";
            //panel3.Controls.Add(fileDisplay);
        }

        private void panel3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private bool tempText = true;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            tempText = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                tempText = true;
            if (tempText)
                textBox1.Text = "Search DAM";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (tempText)
                textBox1.Text = "";
        }
    }
}
