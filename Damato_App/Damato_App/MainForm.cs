using Damato_App.DataBase;
using Damato_App.Settings;
using Damato_App.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damato_App
{
    public partial class MainForm : Form
    {
        int panelWidth;
        bool Hidden;
        public ApplicationSettings ApplicationSettings;
        public string Token;
        public int level;
        public MainForm(string token)
        {
            string json = System.IO.File.ReadAllText("ApplicationSettings.json");
            ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
            InitializeComponent();
            panelWidth = 150;
            Hidden = false;
            Token = token.Trim('"');
            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                level = Int32.Parse((await API.Getlevel(Token)).Trim('"'));
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }
        public MainForm()
        {
            InitializeComponent();
            panelWidth = 150;
            Hidden = false;
            string json = System.IO.File.ReadAllText("ApplicationSettings.json");
            ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            List<string> vs = new List<string>();

            foreach (var item in FileList)
            {

                try
                {
                    string[] allfiles = System.IO.Directory.GetFiles(item, "*.*", System.IO.SearchOption.AllDirectories);
                    foreach (var item2 in allfiles)
                    { vs.Add(item2); }
                }
                catch
                {
                    vs.Add(item);
                }
            }

            foreach (var item in vs)
            {
                this.Cursor = Cursors.WaitCursor;
                MethodInvoker methodInvokerDelegate = async delegate ()
                {
                    AddTags a = new AddTags() { TopText = item, Token = Token };
                    a.ShowDialog();
                    if (a.vss.Count() == 0)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    try
                    {
                        await API.UploadFile(Token, item, a.vss);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        await API.UploadFile(Token, item, a.vss);
                    }
                    List<File> names;
                    try
                    {
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    names.Reverse();
                    panel3.Controls.Clear();
                    foreach (File item2 in names)
                    { addtocontrole(item2); }
                    this.Cursor = Cursors.Default;
                    foreach (var item2 in panel3.Controls)
                    { (item2 as TableLayoutPanel).Controls[0].Visible = isdonwnload; }
                    List<string> filetypes;
                    try
                    {
                        filetypes = await API.GetAllFilesTypes(Token);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        filetypes = await API.GetAllFilesTypes(Token);
                    }
                    filetypes.Sort();
                    filetypes.Reverse();
                    CheckTreeView view = new CheckTreeView();
                    // 
                    // checkTreeView1
                    // 
                    view.AutoSize = true;
                    view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                    view.Category = "File Types";
                    view.Dock = System.Windows.Forms.DockStyle.Top;
                    view.Location = new System.Drawing.Point(1, 1);
                    view.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
                    view.MinimumSize = new System.Drawing.Size(0, 42);
                    view.Name = "checkTreeView1";
                    view.Padding = new System.Windows.Forms.Padding(8);
                    view.Size = new System.Drawing.Size(126, 90);
                    view.Subcategory = filetypes;
                    view.TabIndex = 0;
                    panel6.Controls.Clear();
                    panel6.Controls.Add(view);
                    this.Cursor = Cursors.Default;
                    foreach (var item2 in panel3.Controls)
                    {
                        (item2 as TableLayoutPanel).Controls[0].Visible = false;
                    }

                    try
                    {
                        filetypes = await API.GetAllFilesTags(Token);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        filetypes = await API.GetAllFilesTags(Token);
                    }
                    filetypes.Sort();
                    filetypes.Reverse();
                    CheckTreeView view2 = new CheckTreeView();
                    // 
                    // checkTreeView1
                    // 
                    view2.AutoSize = true;
                    view2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                    view2.Category = "Tags";
                    view2.Dock = System.Windows.Forms.DockStyle.Top;
                    view2.Location = new System.Drawing.Point(1, 1);
                    view2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
                    view2.MinimumSize = new System.Drawing.Size(0, 42);
                    view2.Name = "checkTreeView1";
                    view2.Padding = new System.Windows.Forms.Padding(8);
                    view2.Size = new System.Drawing.Size(126, 90);
                    view2.Subcategory = filetypes;
                    view2.TabIndex = 0;
                    panel6.Controls.Clear();
                    panel6.Controls.Add(view2);
                    this.Cursor = Cursors.Default;
                    foreach (var item2 in panel3.Controls)
                    {
                        (item2 as TableLayoutPanel).Controls[0].Visible = false;
                    }
                };

                if (this.InvokeRequired)
                    this.Invoke(methodInvokerDelegate);
                else
                    methodInvokerDelegate();
            }
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

        public bool issettings = false;
        private void button5_Click(object sender, EventArgs e)
        {
            issettings = true;
            button1.Click -= new System.EventHandler(this.button1_Click);
            Hidden = false;
            timer1.Start();
            panel3.Controls.Clear();
            panel3.Controls.Add(new SettingsControl(ApplicationSettings) { Dock = DockStyle.Fill });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (issettings || ispre)
            {
                string json = JsonConvert.SerializeObject(ApplicationSettings);
                System.IO.File.WriteAllText("ApplicationSettings.json", json);
            }
            issettings = false;
            ispre = false;
            isdonwnload = false;
            button1.Click += new System.EventHandler(this.button1_Click);
            panel3.Controls.Clear();

            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                List<File> names;
                try
                {
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                names.Reverse();
                foreach (File item in names)
                { addtocontrole(item); }
                this.Cursor = Cursors.Default;
                foreach (var item in panel3.Controls)
                { (item as TableLayoutPanel).Controls[0].Visible = false; }
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        public static Image GetImage(string ext)
        {
            switch (ext.Split('.').Last())
            {
                case "cs":
                    return global::Damato_App.Properties.Resources.cs_Image;
                case "css":
                    return global::Damato_App.Properties.Resources.css_Image;
                case "csv":
                    return global::Damato_App.Properties.Resources.csv_Image;
                case "dll":
                    return global::Damato_App.Properties.Resources.dll_Image;
                case "dmg":
                    return global::Damato_App.Properties.Resources.dmg_Image;
                case "doc":
                    return global::Damato_App.Properties.Resources.doc_Image;
                case "eps":
                    return global::Damato_App.Properties.Resources.eps_Image;
                case "exe":
                    return global::Damato_App.Properties.Resources.exe_Image;
                case "flv":
                    return global::Damato_App.Properties.Resources.flv_Image;
                case "gif":
                    return global::Damato_App.Properties.Resources.gif_Image;
                case "gis":
                    return global::Damato_App.Properties.Resources.gis_Image;
                case "gpx":
                    return global::Damato_App.Properties.Resources.gpx_Image;
                case "html":
                    return global::Damato_App.Properties.Resources.html_Image;
                case "ico":
                    return global::Damato_App.Properties.Resources.ico_Image;
                case "jp2":
                    return global::Damato_App.Properties.Resources.jp2_Image;
                case "jpg":
                    return global::Damato_App.Properties.Resources.jpg_Image;
                case "kml":
                    return global::Damato_App.Properties.Resources.kml_Image;
                case "kmz":
                    return global::Damato_App.Properties.Resources.kmz_Image;
                case "mov":
                    return global::Damato_App.Properties.Resources.mov_Image;
                case "mp3":
                    return global::Damato_App.Properties.Resources.mp3_Image;
                case "mpg":
                    return global::Damato_App.Properties.Resources.mpg_Image;
                case "nmea":
                    return global::Damato_App.Properties.Resources.nmea_Image;
                case "ogg":
                    return global::Damato_App.Properties.Resources.ogg_Image;
                case "osm":
                    return global::Damato_App.Properties.Resources.osm_Image;
                case "otf":
                    return global::Damato_App.Properties.Resources.otf_Image;
                case "png":
                    return global::Damato_App.Properties.Resources.png_Image;
                case "ppt":
                    return global::Damato_App.Properties.Resources.ppt_Image;
                case "psd":
                    return global::Damato_App.Properties.Resources.psd_Image;
                case "rar":
                    return global::Damato_App.Properties.Resources.rar_Image;
                case "tar":
                    return global::Damato_App.Properties.Resources.tar_Image;
                case "tif":
                    return global::Damato_App.Properties.Resources.tif_Image;
                case "ttf":
                    return global::Damato_App.Properties.Resources.ttf_Image;
                case "txt":
                    return global::Damato_App.Properties.Resources.txt_Image;
                case "wav":
                    return global::Damato_App.Properties.Resources.wav_Image;
                case "wma":
                    return global::Damato_App.Properties.Resources.wma_Image;
                case "woff":
                    return global::Damato_App.Properties.Resources.woff_Image;
                case "zip":
                    return global::Damato_App.Properties.Resources.zip_Image;
                default:
                    return global::Damato_App.Properties.Resources._default_Image;
            }
        }
        public void addtocontrole(File item)
        {
            //panel3.Controls.Add(new SettingsControl(ApplicationSettings) { Dock = DockStyle.Fill });
            TableLayoutPanel tableLayoutPanelx = new TableLayoutPanel();
            PictureBox pictureBoxx = new PictureBox();
            PictureBox pictureBoxx2 = new PictureBox();
            Label label1x = new Label();
            Label label2x = new Label();
            Label label4x = new Label();
            // 
            // pictureBox2
            // 
            pictureBoxx.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxx.Image = GetImage(item.PathParts.Last());
            pictureBoxx.Location = new System.Drawing.Point(3, 3);
            pictureBoxx.Name = "pictureBox2";
            pictureBoxx.Size = new System.Drawing.Size(24, 24);
            pictureBoxx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxx.TabIndex = 0;
            pictureBoxx.TabStop = false;
            // 
            // label2
            // 
            label1x.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1x.AutoSize = true;
            label1x.ForeColor = System.Drawing.SystemColors.ControlDark;
            label1x.Location = new System.Drawing.Point(33, 3);
            label1x.Name = "label2";
            label1x.Padding = new System.Windows.Forms.Padding(0, 2, 2, 2);
            label1x.Size = new System.Drawing.Size(75, 24);
            label1x.TabIndex = 3;
            label1x.Text = item.PathParts.Last();
            // 
            // label3
            // 
            label2x.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            label2x.AutoSize = true;
            label2x.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label2x.Location = new System.Drawing.Point(411, 3);
            label2x.Name = "label3";
            label2x.Padding = new System.Windows.Forms.Padding(0, 2, 2, 2);
            label2x.Size = new System.Drawing.Size(91, 24);
            label2x.TabIndex = 4;
            label2x.Text = item.DateAdded.ToShortDateString();
            // 
            // pictureBoxx2
            // 
            pictureBoxx2.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxx2.Image = global::Damato_App.Properties.Resources.icons8_Down_Arrow_26px;
            pictureBoxx2.Tag = "Down";
            pictureBoxx2.Location = new System.Drawing.Point(508, 3);
            pictureBoxx2.Name = "pictureBox3";
            pictureBoxx2.Size = new System.Drawing.Size(24, 24);
            pictureBoxx2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxx2.TabIndex = 5;
            pictureBoxx2.TabStop = false;
            pictureBoxx2.Click += new System.EventHandler(pictureBox3_Click);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanelx.ColumnCount = 4;
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanelx.Controls.Add(pictureBoxx2, 3, 0);
            tableLayoutPanelx.Controls.Add(label2x, 2, 0);
            tableLayoutPanelx.Controls.Add(label1x, 1, 0);
            tableLayoutPanelx.Controls.Add(pictureBoxx, 0, 0);
            tableLayoutPanelx.Controls.Add(label4x, 1, 1);
            tableLayoutPanelx.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanelx.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelx.Name = "tableLayoutPanelx";
            tableLayoutPanelx.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelx.Size = new System.Drawing.Size(535, 30);
            tableLayoutPanelx.TabIndex = 0;
            label1x.MouseEnter += new System.EventHandler(this.tableLayoutPanel1_MouseEnter);
            label1x.MouseLeave += new System.EventHandler(this.tableLayoutPanel1_MouseLeave);
            // 
            // label4
            // 
            label4x.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label4x.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(this.label4, 2);
            label4x.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label4x.ForeColor = System.Drawing.SystemColors.ControlDark;
            label4x.Location = new System.Drawing.Point(33, 33);
            label4x.Name = "label4";
            label4x.Padding = new System.Windows.Forms.Padding(0, 2, 2, 2);
            label4x.Size = new System.Drawing.Size(67, 21);
            label4x.TabIndex = 6;
            string total = "";
            foreach (var item22 in item.MainTags)
            {
                total += " " + item22._Tag + ", ";
            }
            total = total.Trim().Trim(',');
            label4x.Text = total;

            panel3.Controls.Add(tableLayoutPanelx);
        }
        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            ((sender as Label).Parent as TableLayoutPanel).BackColor = Color.FromArgb(35, 35, 35);
            ((sender as Label).Parent as TableLayoutPanel).Size = new System.Drawing.Size(535, 58);
        }
        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            ((sender as Label).Parent as TableLayoutPanel).BackColor = Color.FromArgb(31, 31, 31);
            ((sender as Label).Parent as TableLayoutPanel).Size = new System.Drawing.Size(535, 30);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel3.Controls.Clear();

            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                List<File> names;
                try
                {
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                names.Reverse();
                foreach (File item in names)
                { addtocontrole(item); }

                List<string> filetypes;
                try
                {
                    filetypes = await API.GetAllFilesTypes(Token);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    filetypes = await API.GetAllFilesTypes(Token);
                }
                filetypes.Sort();
                filetypes.Reverse();
                CheckTreeView view = new CheckTreeView();
                // 
                // checkTreeView1
                // 
                view.AutoSize = true;
                view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                view.Category = "File Types";
                view.Dock = System.Windows.Forms.DockStyle.Top;
                view.Location = new System.Drawing.Point(1, 1);
                view.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
                view.MinimumSize = new System.Drawing.Size(0, 42);
                view.Name = "checkTreeView1";
                view.Padding = new System.Windows.Forms.Padding(8);
                view.Size = new System.Drawing.Size(126, 90);
                view.Subcategory = filetypes;
                view.TabIndex = 0;
                panel6.Controls.Clear();
                panel6.Controls.Add(view);
                this.Cursor = Cursors.Default;
                foreach (var item in panel3.Controls)
                {
                    (item as TableLayoutPanel).Controls[0].Visible = false;
                }

                try
                {
                    filetypes = await API.GetAllFilesTags(Token);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    filetypes = await API.GetAllFilesTags(Token);
                }
                filetypes.Sort();
                filetypes.Reverse();
                CheckTreeView view2 = new CheckTreeView();
                // 
                // checkTreeView1
                // 
                view2.AutoSize = true;
                view2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
                view2.Category = "Tags";
                view2.Dock = System.Windows.Forms.DockStyle.Top;
                view2.Location = new System.Drawing.Point(1, 1);
                view2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
                view2.MinimumSize = new System.Drawing.Size(0, 42);
                view2.Name = "checkTreeView1";
                view2.Padding = new System.Windows.Forms.Padding(8);
                view2.Size = new System.Drawing.Size(126, 90);
                view2.Subcategory = filetypes;
                view2.TabIndex = 0;
                panel6.Controls.Add(view2);
                this.Cursor = Cursors.Default;
                foreach (var item2 in panel3.Controls)
                {
                    (item2 as TableLayoutPanel).Controls[0].Visible = false;
                }
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        public bool isdonwnload = false;
        private void button4_Click(object sender, EventArgs e)
        {
            if (issettings || ispre)
            {
                string json = JsonConvert.SerializeObject(ApplicationSettings);
                System.IO.File.WriteAllText("ApplicationSettings.json", json);
                this.Cursor = Cursors.WaitCursor;
                MethodInvoker methodInvokerDelegate = async delegate ()
                {
                    panel3.Controls.Clear();
                    List<File> names;
                    try
                    {
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    names.Reverse();
                    foreach (File item in names)
                    { addtocontrole(item); }
                    this.Cursor = Cursors.Default;
                    foreach (var item in panel3.Controls)
                    { (item as TableLayoutPanel).Controls[0].Visible = true; }

                    List<string> outfiles;
                    try
                    {
                        outfiles = await API.GetOutFiles(Token);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        outfiles = await API.GetOutFiles(Token);
                    }
                    Dictionary<string, int> outfiles2 = new Dictionary<string, int>();
                    foreach (var item in outfiles)
                    {
                        outfiles2.Add(item.Substring(0, item.Length - item.Split('[').Last().Length + 1), Int32.Parse(item.Split('[').Last().TrimEnd(']')));
                    }
                    foreach (var item in panel3.Controls)
                    {
                        if (outfiles2.Keys.Contains(((item as TableLayoutPanel).Controls[2] as Label).Text))
                        {
                            if (outfiles2[((item as TableLayoutPanel).Controls[2] as Label).Text] >= level)
                            {
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Image = global::Damato_App.Properties.Resources.icons8_Up_26px;
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Tag = "Up";
                            }
                            else
                            {
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Visible = false;
                            }
                        }
                    }
                };

                if (this.InvokeRequired)
                    this.Invoke(methodInvokerDelegate);
                else
                    methodInvokerDelegate();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                MethodInvoker methodInvokerDelegate = async delegate ()
                {
                    foreach (var item in panel3.Controls)
                    { (item as TableLayoutPanel).Controls[0].Visible = true; }
                    List<string> outfiles;
                    try
                    {
                        outfiles = await API.GetOutFiles(Token);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        outfiles = await API.GetOutFiles(Token);
                    }
                    Dictionary<string, int> outfiles2 = new Dictionary<string, int>();
                    foreach (var item in outfiles)
                    {
                        outfiles2.Add(item.Substring(0, item.Length - item.Split('[').Last().Length + 1), Int32.Parse(item.Split('[').Last().TrimEnd(']')));
                    }
                    foreach (var item in panel3.Controls)
                    {
                        if (outfiles2.Keys.Contains(((item as TableLayoutPanel).Controls[2] as Label).Text))
                        {
                            if (outfiles2[((item as TableLayoutPanel).Controls[2] as Label).Text] >= level)
                            {
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Image = global::Damato_App.Properties.Resources.icons8_Up_26px;
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Tag = "Up";
                            }
                            else
                            {
                                ((item as TableLayoutPanel).Controls[0] as PictureBox).Visible = false;
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                };

                if (this.InvokeRequired)
                    this.Invoke(methodInvokerDelegate);
                else
                    methodInvokerDelegate();
            }
            isdonwnload = true;
            button1.Click -= new System.EventHandler(this.button1_Click);
            Hidden = false;
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if ((((sender as PictureBox).Parent as TableLayoutPanel).Controls[0] as PictureBox).Tag.ToString() == "Up")
            {
                this.Cursor = Cursors.WaitCursor;
                MethodInvoker methodInvokerDelegate1 = async delegate ()
                {
                    try
                    {
                        await API.UploadFile(Token, $"{ApplicationSettings.DownLoadedSettings.DownLoadFileLocation}\\{(((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text}", "true");
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        await API.UploadFile(Token, $"{ApplicationSettings.DownLoadedSettings.DownLoadFileLocation}\\{(((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text}", "true");
                    }
                    System.IO.File.Delete($"{ApplicationSettings.DownLoadedSettings.DownLoadFileLocation}\\{(((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text}");
                    List<File> names;
                    try
                    {
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                    }
                    names.Reverse();
                    panel3.Controls.Clear();
                    foreach (File item2 in names)
                    { addtocontrole(item2); }
                    this.Cursor = Cursors.Default;
                    foreach (var item2 in panel3.Controls)
                    { (item2 as TableLayoutPanel).Controls[0].Visible = isdonwnload; }
                    List<string> outfiles;
                    try
                    {
                        outfiles = await API.GetOutFiles(Token);
                    }
                    catch
                    {
                        Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                        outfiles = await API.GetOutFiles(Token);
                    }
                    foreach (var item in panel3.Controls)
                    {
                        if (outfiles.Contains(((item as TableLayoutPanel).Controls[2] as Label).Text))
                        {
                            ((item as TableLayoutPanel).Controls[0] as PictureBox).Image = global::Damato_App.Properties.Resources.icons8_Up_26px;
                            ((item as TableLayoutPanel).Controls[0] as PictureBox).Tag = "Up";
                        }
                    }
                };

                if (this.InvokeRequired)
                    this.Invoke(methodInvokerDelegate1);
                else
                    methodInvokerDelegate1();
                return;
            }
            (((sender as PictureBox).Parent as TableLayoutPanel).Controls[0] as PictureBox).Tag = "Up";
            //(sender as PictureBox)
            //(((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text = "";
            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                try
                {
                    await API.DownloadFile(Token, (((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text, ApplicationSettings.DownLoadedSettings.DownLoadFileLocation);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    await API.DownloadFile(Token, (((sender as PictureBox).Parent as TableLayoutPanel).Controls[2] as Label).Text, ApplicationSettings.DownLoadedSettings.DownLoadFileLocation);
                }
                List<File> names;
                try
                {
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                names.Reverse();
                panel3.Controls.Clear();
                foreach (File item2 in names)
                { addtocontrole(item2); }
                this.Cursor = Cursors.Default;
                foreach (var item2 in panel3.Controls)
                { (item2 as TableLayoutPanel).Controls[0].Visible = isdonwnload; }
                List<string> outfiles;
                try
                {
                    outfiles = await API.GetOutFiles(Token);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    outfiles = await API.GetOutFiles(Token);
                }
                foreach (var item in panel3.Controls)
                {
                    if (outfiles.Contains(((item as TableLayoutPanel).Controls[2] as Label).Text))
                    {
                        ((item as TableLayoutPanel).Controls[0] as PictureBox).Image = global::Damato_App.Properties.Resources.icons8_Up_26px;
                        ((item as TableLayoutPanel).Controls[0] as PictureBox).Tag = "Up";
                    }
                }
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            issettings = false;
            ispre = false;
            isdonwnload = false;
            button1.Click += new System.EventHandler(this.button1_Click);
            panel3.Controls.Clear();

            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                List<string> ss = new List<string>();
                if (textBox1.Text.Trim() != "" && textBox1.Text.Trim() != "Search DAM")
                    ss.Add(textBox1.Text);

                foreach (var item in panel6.Controls)
                {
                    if ((item as CheckTreeView).Category == "Tags")
                    {
                        foreach (var item3 in (item as CheckTreeView).GetAllChecked())
                        {
                            ss.Add("*" + item3);
                        }
                    }
                    else
                    {
                        ss.AddRange((item as CheckTreeView).GetAllChecked());
                    }
                }

                List<File> names;
                try
                {
                    names = await API.SearchRecentFiles(Token, ss, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                catch
                {
                    Token = await API.GetNewToken(ApplicationSettings.LoginSettings.UserName, ApplicationSettings.LoginSettings.Password);
                    names = await API.SearchRecentFiles(Token, ss, ApplicationSettings.SearchSettings.ReturnAmount);
                }
                names.Reverse();
                panel3.Controls.Clear();
                foreach (File item in names)
                { addtocontrole(item); }
                this.Cursor = Cursors.Default;
                foreach (var item in panel3.Controls)
                { (item as TableLayoutPanel).Controls[0].Visible = false; }
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        public bool ispre = false;
        private void button3_Click(object sender, EventArgs e)
        {
            ispre = true;
            button1.Click -= new System.EventHandler(this.button1_Click);
            Hidden = false;
            timer1.Start();
            panel3.Controls.Clear();
            panel3.Controls.Add(new TemplatesControl() { Dock = DockStyle.Fill, Token = Token});
        }
    }

    public static class API
    {
        public static HttpClient _api = new HttpClient() { BaseAddress = new Uri("https://damatoapi.azurewebsites.net/api/") };

        public static async Task<List<File>> GetRecentFiles(string token, int amount = 10)
        {
            HttpResponseMessage response = await _api.GetAsync($"Files/{token}/GetRecentFiles?amount={amount}");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<File>>();
            else
                throw new Exception();
            //return new List<File>();
        }

        public static async Task<List<string>> GetAllFilesTypes(string token)
        {
            //Misc/3124084%2B00/GetAllFilesTags
            HttpResponseMessage response = await _api.GetAsync($"Misc/{token}/GetAllFilesTypes");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<string>>();
            else
                throw new Exception();
            //return new List<string>();
        }
        public static async Task<List<string>> GetAllFilesTags(string token)
        {
            HttpResponseMessage response = await _api.GetAsync($"Misc/{token}/GetAllFilesTags");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<string>>();
            else
                throw new Exception();
            //return new List<string>();
        }

        public static async Task<string> GetNewToken(string name, string password)
        {
            //\{\"Name\":\"{}\",\"Password\":\"{}\"\}
            HttpContent _content = new StringContent($"{"{"}\"Name\":\"{name}\",\"Password\":\"{password}\"{"}"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _api.PostAsync($"Users/GetNewToken", _content);
            if (response.IsSuccessStatusCode)
                return (await response.Content.ReadAsStringAsync()).Trim('"');
            return "Fail";
        }

        public static async Task<bool> UploadFile(string token, string filepath, string reupload = "false")
        {
            byte[] temp = System.IO.File.ReadAllBytes(filepath);//{filepath.Split('\\').Last()}
            //{ "Path": "string", "File": "AxD//w==" }
            HttpContent _content = new StringContent($"{"{"} \"Path\": \"{filepath.Split('\\').Last()}\", \"File\": \"{ Convert.ToBase64String(System.IO.File.ReadAllBytes(filepath))}\" {"}"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _api.PostAsync($"Files/{token}/UploadFile/{reupload}", _content);
            if (response.IsSuccessStatusCode)
                return true;
            else
                throw new Exception();
            //
        }
        public static async Task<bool> UploadFile(string token, string filepath, List<string> reupload)
        {
            byte[] temp = System.IO.File.ReadAllBytes(filepath);//{filepath.Split('\\').Last()}
            //{ "Path": "string", "File": "AxD//w==" }
            //{ "Path": "string", "File": "AxD//w==", Tags": [ "Test20",  "Test20"] }
            string tt = ", \"Tags\": [ ";
            foreach (var item in reupload)
            {
                tt += $"\"{item}\", ";
            }
            tt = tt.TrimEnd().TrimEnd(',') + ']';
            HttpContent _content = new StringContent($"{"{"} \"Path\": \"{filepath.Split('\\').Last()}\", \"File\": \"{ Convert.ToBase64String(System.IO.File.ReadAllBytes(filepath))}\" {tt} {"}"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _api.PostAsync($"Files/{token}/UploadFileTaged/false", _content);
            if (response.IsSuccessStatusCode)
                return true;
            else
                throw new Exception();
            //
        }

        public static async Task<bool> DownloadFile(string token, string filename, string filepath)
        {
            //api/Files/0132995%2B13/DownloadFile?filename=2.txt
            HttpResponseMessage response = await _api.PostAsync($"Files/{token}/DownloadFile?filename={filename}", null);
            if (response.IsSuccessStatusCode)
            {
                string ss = await response.Content.ReadAsStringAsync();
                System.IO.File.WriteAllBytes($"{filepath}\\{filename}", Convert.FromBase64String(ss.Trim('"')));
            }
            else
                throw new Exception();
            // 
            return true;
        }

        public static async Task<List<string>> GetOutFiles(string token)
        {
            HttpResponseMessage response = await _api.GetAsync($"Users/{token}/GetOutFiles");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<string>>();
            else
                throw new Exception();
            //return new List<string>();
        }

        public static async Task<string> Getlevel(string token)
        {
            //\{\"Name\":\"{}\",\"Password\":\"{}\"\}
            HttpResponseMessage response = await _api.GetAsync($"Users/{token}/Getlevel");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return "Fail";
        }

        public static async Task<List<File>> SearchRecentFiles(string token, List<string> search, int amount = 10)
        {
            string input = $"Files/{token}/SearchRecentFiles?amount={amount}";
            foreach (var item in search)
            {
                input += $"&search={item}";
            }
            HttpResponseMessage response = await _api.GetAsync(input);
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<File>>();
            else
                throw new Exception();
            //return new List<File>();
        }

        //public static async Task<List<Flag>> ByCurrency(string code)
        //{
        //    string[] vs = code.Split(',');
        //    List<string> vss = new List<string>();
        //    List<Flag> vsf = new List<Flag>();
        //    foreach (var item in vs) { vss.Add(item.Split('(').Last().TrimEnd(')')); }
        //    foreach (var item in vss)
        //    {
        //        HttpResponseMessage response = await _api.GetAsync($"currency/{item}?fields=name;flag");
        //        if (response.IsSuccessStatusCode)
        //            vsf.AddRange(JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<Flag>>());
        //    }
        //    return vsf;
        //}


        public static async Task<bool> DeletePresets(string token, string filename, string filepath)
        {
            //api/Files/0132995%2B13/DownloadFile?filename=2.txt
            HttpResponseMessage response = await _api.DeleteAsync($"Presets/{token}/DeletePresets/{filename}");
            // 
            return true;
        }

        public static async Task<int> PostPresets(string token, string filename, Presets filepath)
        {
            //{ "Name": "Passport", "Feleds": "Name*DOB*ID_No." }
            HttpContent s = new StringContent($"{"{"} \"Name\": \"{filepath.Name}\", \"Feleds\": \"{filepath.Feleds}\" {"}"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _api.PostAsync($"Presets/{token}/PostPresets", s);
            if (response.IsSuccessStatusCode)
            {
                string dd = await response.Content.ReadAsStringAsync();
                return Int32.Parse(dd.Split('"')[2].Trim(',').Trim(':'));
            }
            else
                throw new Exception();


        }

        public static async Task<List<Presets>> GetPresetss(string token, string filename)
        {
            //api/Files/0132995%2B13/DownloadFile?filename=2.txt
            HttpResponseMessage response = await _api.GetAsync($"Presets/{token}/GetPresetss");
            // 
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<Presets>>();
            else
                throw new Exception();
        }
    }
}
