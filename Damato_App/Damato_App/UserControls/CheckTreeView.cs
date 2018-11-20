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
            checkBox.Name = "checkBox2";
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
        }

        public string Category { get { return checkBox1.Text; } set { checkBox1.Text = value; } }
        [Editor(typeof(MyStringCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Subcategory
        {
            get
            {
                List<string> vs = new List<string>();
                foreach (var item in subcategory) { vs.Add(item.ToString()); }
                return vs;
            }
            set
            {
                subcategory.Clear();
                foreach (var item in value) { subcategory.Add(newCheckBox(item)); }
            }
        }
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
                    subcategory[i].Checked = true;
            }
        }
    }

    public class MyStringCollectionEditor : System.ComponentModel.Design.CollectionEditor
    {
        public MyStringCollectionEditor() : base(type: typeof(List<String>)) { }
        protected override object CreateInstance(Type itemType) {
            return string.Empty;
        }
    }
}
