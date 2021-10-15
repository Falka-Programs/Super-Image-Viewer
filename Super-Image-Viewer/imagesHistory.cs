using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Super_Image_Viewer.Models;

namespace Super_Image_Viewer
{
    public partial class imagesHistory : Form
    {
        HistoryControl historyControl;
        public string FilePath{get;set;}
        public imagesHistory(HistoryControl hc)
        {
            InitializeComponent();
            historyControl = hc;
            history_View.DoubleClick += Double_Click;
        }


        private async void imagesHistory_Load(object sender, EventArgs e)
        {
            await LoadHistory();
        }
        private async Task LoadHistory()
        {
            List<FileHistoryModel> files = await historyControl.GetFilesHistory();
            foreach(var item in files)
            {
                ListViewItem view_item = history_View.Items.Add(item.Id.ToString());
                view_item.SubItems.Add(item.FileName);
                view_item.SubItems.Add(item.FilePath);
                view_item.SubItems.Add(item.dateTime.ToString());
            }
        }

        private void Double_Click(object sender,EventArgs e)
        {
            if (history_View.SelectedItems.Count > 0)
            {
                FilePath = history_View.SelectedItems[0].SubItems[2].Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async void clean_button_Click(object sender, EventArgs e)
        {
            await historyControl.Clean();
            await LoadHistory();
        }
    }
}
