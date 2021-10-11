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

namespace Super_Image_Viewer
{
    public partial class Form1 : Form
    {
        FileSystemViewer fsv;
        public Form1()
        {
            InitializeComponent();
            fsv = new FileSystemViewer();
            fsv.MoveTo("..\\");
            Console.WriteLine(fsv.GetDirectories()[0].Name);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //imageList1.ImageSize = new Size(64, 64);
            LoadIcons();
            ListViewItem lvt = new ListViewItem("Test folder",0);
            UpdateFileView();
        }

        private void LoadIcons()
        {
            ImageList iList = new ImageList();
            iList.ImageSize = new Size(128, 128);
            iList.ColorDepth = ColorDepth.Depth32Bit;
            iList.Images.Add(Image.FromFile("../../Images/folder.png"));
            iList.Images.Add(Image.FromFile("../../Images/file.png"));
            iList.Images.Add(Image.FromFile("../../Images/image.png"));
            File_View.LargeImageList = iList;
        }
        private void UpdateFileView()
        {
            //NOTE elements in array has index one less that elements in FileView
            //This caused by element ... that returns back in directory
            File_View.Items.Clear();
            File_View.Items.Add(@"..\", 0);
            for (int i = 0; i < fsv.GetDirectories().Length; i++)
            {
                File_View.Items.Add(fsv.GetDirectories()[i].Name, 0);
            }
            for (int i = 0; i < fsv.GetFiles().Length; i++)
            {
                string file_name = fsv.GetFiles()[i].Name;
                if (fsv.IsImage(file_name)) {
                    File_View.Items.Add(file_name, 2);
                }
                else
                {
                    File_View.Items.Add(file_name, 1);
                }
            }
        }

        private void File_View_DoubleClick(object sender, EventArgs e)
        {
            if (File_View.SelectedIndices.Count > 0)
            {
                if (File_View.SelectedItems[0].ImageIndex == 0)
                    fsv.MoveTo(File_View.SelectedItems[0].Text);
                else if (File_View.SelectedItems[0].ImageIndex == 1)
                    MessageBox.Show("Current format isn`t supported!");
                else if (File_View.SelectedItems[0].ImageIndex == 2)
                {
                    Console.WriteLine(1);
                }
            }
            UpdateFileView();
        }
    }
}
