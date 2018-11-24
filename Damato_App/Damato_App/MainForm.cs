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
        public MainForm(string token)
        {
            string json = System.IO.File.ReadAllText("ApplicationSettings.json");
            ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
            InitializeComponent();
            panelWidth = 150;
            Hidden = false;
            Token = token.Trim('"');
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
            if (issettings)
            {
                string json = JsonConvert.SerializeObject(ApplicationSettings);
                System.IO.File.WriteAllText("ApplicationSettings.json", json);
            }
            issettings = false;
            button1.Click += new System.EventHandler(this.button1_Click);
            panel3.Controls.Clear();

            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                List<File> names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                names.Reverse();
                foreach (File item in names)
                { addtocontrole(item); }
                this.Cursor = Cursors.Default;
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
            Label label1x = new Label();
            Label label2x = new Label();
            // 
            // pictureBox2
            // 
            pictureBoxx.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxx.Image = GetImage(item.PathParts.Last());
            pictureBoxx.Location = new System.Drawing.Point(3, 3);
            pictureBoxx.Name = "pictureBoxx";
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
            label1x.Name = "label1x";
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
            label2x.Location = new System.Drawing.Point(441, 3);
            label2x.Name = "label2x";
            label2x.Padding = new System.Windows.Forms.Padding(0, 2, 2, 2);
            label2x.Size = new System.Drawing.Size(91, 24);
            label2x.TabIndex = 4;
            label2x.Text = item.DateAdded.ToShortDateString();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanelx.ColumnCount = 3;
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelx.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanelx.Controls.Add(label2x, 2, 0);
            tableLayoutPanelx.Controls.Add(label1x, 1, 0);
            tableLayoutPanelx.Controls.Add(pictureBoxx, 0, 0);
            tableLayoutPanelx.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanelx.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelx.Name = "tableLayoutPanelx";
            tableLayoutPanelx.RowCount = 1;
            tableLayoutPanelx.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelx.Size = new System.Drawing.Size(535, 30);
            tableLayoutPanelx.TabIndex = 0;
            tableLayoutPanelx.MouseEnter += new System.EventHandler(this.tableLayoutPanel1_MouseEnter);
            tableLayoutPanelx.MouseLeave += new System.EventHandler(this.tableLayoutPanel1_MouseLeave);

            panel3.Controls.Add(tableLayoutPanelx);
        }
        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            (sender as TableLayoutPanel).BackColor = Color.FromArgb(35, 35, 35);
        }
        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            (sender as TableLayoutPanel).BackColor = Color.FromArgb(31, 31, 31);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel3.Controls.Clear();

            this.Cursor = Cursors.WaitCursor;
            MethodInvoker methodInvokerDelegate = async delegate ()
            {
                List<File> names = await API.GetRecentFiles(Token, ApplicationSettings.SearchSettings.ReturnAmount);
                names.Reverse();
                foreach (File item in names)
                { addtocontrole(item); }

                List<string> filetypes = await API.GetAllFilesTypes(Token);
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
            };

            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }
    }


    public static class API
    {
        public static HttpClient _api = new HttpClient() { BaseAddress = new Uri("http://localhost:52799/api/") };

        public static async Task<List<File>> GetRecentFiles(string token, int amount = 10)
        {
            HttpResponseMessage response = await _api.GetAsync($"Files/{token}/GetRecentFiles?amount={amount}");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<File>>();
            return new List<File>();
        }
        public static async Task<List<string>> GetAllFilesTypes(string token)
        {
            HttpResponseMessage response = await _api.GetAsync($"Misc/{token}/GetAllFilesTypes");
            if (response.IsSuccessStatusCode)
                return JArray.Parse((await response.Content.ReadAsStringAsync())).ToObject<List<string>>();
            return new List<string>();
        }

        public static async Task<string> GetNewToken(string name, string password)
        {
            //\{\"Name\":\"{}\",\"Password\":\"{}\"\}
            HttpContent _content = new StringContent($"{"{"}\"Name\":\"{name}\",\"Password\":\"{password}\"{"}"}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _api.PostAsync($"Users/GetNewToken", _content);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return "Fail";
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
    }
}
