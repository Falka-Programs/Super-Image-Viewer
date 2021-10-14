using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Super_Image_Viewer
{
    public partial class SettingsForm : Form
    {
        public bool ImagePreviewStatus;
        public bool IntelegentPreviewStatus;
        public SettingsForm()
        {
            InitializeComponent();
            ImagePreviewStatus = false;
            IntelegentPreviewStatus = false;
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            imagePreview_checkBox.Checked = ImagePreviewStatus;
            IntelegentPreview_comboBox.Checked = IntelegentPreviewStatus;
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            ImagePreviewStatus = imagePreview_checkBox.Checked;
            IntelegentPreviewStatus = IntelegentPreview_comboBox.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}
