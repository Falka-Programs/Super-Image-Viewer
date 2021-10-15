using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Super_Image_Viewer.Models;

namespace Super_Image_Viewer
{
    public partial class FindForm : Form
    {
        public string StartDirectory{get;set;}
        public string SearchFileName { get; set; }
        public string FindedFileName { get; set; }
        public FindForm(string startDirectory)
        {
            StartDirectory = startDirectory;
            InitializeComponent();
            mainDirectory_textBox.Text = StartDirectory;
        }

        private void FindForm_Load(object sender, EventArgs e)
        {

        }
        private void UpdateFiles(string[] files)
        {
            finded_files_listBox.Items.Clear();
            foreach(string item in files)
            {
                string name = item.Split('\\')[item.Split('\\').Length - 1];
                FileAndName fan = new FileAndName() {FullPath = item,Name = name };
                finded_files_listBox.Items.Add(fan);
            }
            finded_files_listBox.DisplayMember = "Name";
        }
        private void finded_files_listBox_DoubleClick(object sender, EventArgs e)
        {
            if (finded_files_listBox.SelectedItem != null)
            {
                FindedFileName = ((FileAndName)finded_files_listBox.SelectedItem).FullPath;
                this.DialogResult = DialogResult.OK;
            }
        }
        private void start_button_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(mainDirectory_textBox.Text) || !String.IsNullOrWhiteSpace(fileName_textBox.Text))
            {
                if (Directory.Exists(mainDirectory_textBox.Text))
                {
                    string search_directory = mainDirectory_textBox.Text;
                    if (search_directory.Length > 2)
                    {
                        if(search_directory[1]==':' && search_directory[2] == '\\')
                        {
                            try
                            {
                                string fName = fileName_textBox.Text;
                                string[] tmp = fileName_textBox.Text.Split('.');
                                if (fileName_textBox.Text.Split('.').Length > 1)
                                    fName = tmp[tmp.Length - 2];
                                string[] files = Directory.GetFiles(search_directory, $"{fName}.*", SearchOption.AllDirectories);
                                UpdateFiles(files);
                            }
                            catch(Exception err)
                            {
                                throw new Exception($"Find error\n{err.Message}");
                            }
                        }
                        else
                            MessageBox.Show("Wrong directory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Wrong directory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Wrong directory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
