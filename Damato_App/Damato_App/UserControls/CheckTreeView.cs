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
    public partial class CheckTreeView : UserControl
    {
        private static CheckBox newCheckBox(string text = "Subcategory")
        {
            CheckBox checkBox = new CheckBox();
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.Dock = System.Windows.Forms.DockStyle.Top;
            checkBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            checkBox.Location = new System.Drawing.Point(5, 0);
            checkBox.Size = new System.Drawing.Size(164, 17);
            checkBox.TabIndex = 2;
            checkBox.Text = text;
            checkBox.UseVisualStyleBackColor = true;

            return checkBox;
        }

        private readonly int itemHeight = 17;

        public CheckTreeView()
        {
            InitializeComponent();
            subcategoryChatch = new List<string>();
        }

        public string Category { get { return label1.Text; } set { label1.Text = value; } }

        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Subcategory { get { return subcategoryChatch; } set { subcategoryChatch = value; } }

        private void UpdateSub()
        {
            List<CheckBox> boxes = new List<CheckBox>();
            foreach (var item in subcategoryChatch) { boxes.Add(newCheckBox(item)); }
            subcategory = boxes;
        }

        private List<string> subcategoryChatch { get; set; }
        //{
        //    get
        //    {
        //        List<string> vs = new List<string>();
        //        foreach (var item in subcategory) { vs.Add(item.Text); }
        //        return vs;
        //    }
        //    set
        //    {
        //        List<CheckBox> boxes = new List<CheckBox>();
        //        foreach (var item in value) { boxes.Add(newCheckBox(item)); }
        //        subcategory = boxes;
        //    }
        //}

        private List<CheckBox> subcategory
        {
            get
            {
                List<CheckBox> boxes = new List<CheckBox>();
                foreach (var item in panel1.Controls) { boxes.Add(item as CheckBox); }
                return boxes;
            }
            set
            {
                panel1.Controls.Clear();
                foreach (var item in value) { panel1.Controls.Add(item); }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= (subcategory.Count - 1); i++)
            {
                if (checkBox1.Checked)
                    subcategory[i].Checked = true;
                else
                    subcategory[i].Checked = false;
            }
        }

        private void CheckTreeView_Load(object sender, EventArgs e)
        {
            UpdateSub();
            VisibleSub = !VisibleSub;
            for (int i = 0; i <= (subcategory.Count - 1); i++)
            {
                if (checkBox1.Checked)
                    subcategory[i].Visible = VisibleSub;
                else
                    subcategory[i].Visible = VisibleSub;
            }
        }
        private bool VisibleSub = true;
        private void checkBox1_Click(object sender, EventArgs e)
        {
            VisibleSub = !VisibleSub;
            for (int i = 0; i <= (subcategory.Count - 1); i++)
            {
                if (checkBox1.Checked)
                    subcategory[i].Visible = VisibleSub;
                else
                    subcategory[i].Visible = VisibleSub;
            }
        }
    }
}
