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
        ImageList iList;
        public Form1()
        {
            InitializeComponent();
            fsv = new FileSystemViewer();
            fsv.MoveTo("..\\");
            path_textBox.Text = fsv.CurrentPath;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            LoadIcons();
            await UpdateFileViewAsync();
        }
        
        private void LoadIcons()
        {
            iList = new ImageList();
            iList.ImageSize = new Size(128, 128);
            iList.ColorDepth = ColorDepth.Depth32Bit;
            iList.Images.Add(Image.FromFile("../../Images/folder.png"));
            iList.Images.Add(Image.FromFile("../../Images/file.png"));
            iList.Images.Add(Image.FromFile("../../Images/image.png"));
            File_View.LargeImageList = iList;
        }
        private int AddIcons(Image img)
        {
            iList.Images.Add(img);
            return iList.Images.Count-1;
        }
        private void CleanIcons()
        {
            iList.Dispose();
            LoadIcons();
        }
        private Image resizeImage(string path)
        {
            try
            {
                Image toResize = Image.FromFile(path);
                Bitmap b = new Bitmap(128, 128);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(toResize, 0, 0, 128, 128);
                g.Dispose();
                toResize.Dispose();
                return (Image)b;
            }
            catch
            {
                Image error_image = Image.FromFile("../../Images/error.png");
                return error_image;
            }
        }
        private Task<Image> resizeImageAsync(string path)
        {
            return Task.Run<Image>(() =>
            {
                return resizeImage(path);
            });
        }

        private async Task UpdateFileViewAsync()
        {
            CleanIcons();
            //NOTE elements in array has index one less that elements in FileView
            //This caused by element ..\ that returns back in directory
            File_View.Items.Clear();
            File_View.Items.Add(@"..\", 0);
            for (int i = 0; i < fsv.GetDirectories().Length; i++)
            {
                File_View.Items.Add(fsv.GetDirectories()[i].Name, 0);
            }
            List<int> positions = new List<int>();
            List<string> names = new List<string>();
            for (int i = 0; i < fsv.GetFiles().Length; i++)
            {
                string file_name = fsv.GetFiles()[i].Name;
                if (fsv.IsImage(file_name)) {
                    if(ProgrammParametrs.ShowImagePreview==false)
                        File_View.Items.Add(file_name, 2);
                    else
                    {
                        Image resizedImage = await resizeImageAsync(fsv.GetFiles()[i].FullName);
                        int pos = AddIcons(resizedImage);
                        positions.Add(pos);
                        names.Add(file_name);
                        if (positions.Count > 4)
                        {
                            for (int count = 0; count < positions.Count; count++)
                            {
                                File_View.Items.Add(names[count], positions[count]);
                            }
                            positions.Clear();
                            names.Clear();
                        }
                    }
                }
                else
                {
                    File_View.Items.Add(file_name, 1);
                }
            }
            for(int i = 0; i < positions.Count; i++)
            {
                File_View.Items.Add(names[i], positions[i]);
            }
        }
        private void ShowImage(string path)
        {
            Image img = Image.FromFile(path);
            pictureBox.Image = img;
            SetActiveTab(1);
        }
        private Task ShowImageAsync(string path)
        {
            return Task.Run(() =>
            {
                ShowImage(path);
            });
        }

        private async void File_View_DoubleClick(object sender, EventArgs e)
        {
            if (File_View.SelectedIndices.Count > 0)
            {
                if (File_View.SelectedItems[0].ImageIndex == 0)
                {
                    fsv.MoveTo(File_View.SelectedItems[0].Text);
                    path_textBox.Text = fsv.CurrentPath;
                }
                else if (File_View.SelectedItems[0].ImageIndex == 1)
                    MessageBox.Show("Current format isn`t supported!");
                else if (File_View.SelectedItems[0].ImageIndex > 1)
                {
                    await ShowImageAsync(fsv.GetFilePath(File_View.SelectedItems[0].Text));

                }
            }
            await UpdateFileViewAsync();
        }
        delegate void SetActiveTabCallback(int tab);
        private void SetActiveTab(int tab)
        {
            if (this.path_textBox.InvokeRequired)
            {
                SetActiveTabCallback d = new SetActiveTabCallback(SetActiveTab);
                this.Invoke(d, new object[] { tab });
            }
            else
            {
                this.tabControl.SelectedIndex = tab;
            }
        }

        private async void path_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    fsv.CurrentPath = path_textBox.Text;
                    await UpdateFileViewAsync();

                }
                catch
                {
                    MessageBox.Show("Invalid path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private async void back_button_Click(object sender, EventArgs e)
        {
            if (fsv.IsBackAvaible() == false)
                back_button.Enabled = false;
            fsv.MoveTo("..\\");
            path_textBox.Text = fsv.CurrentPath;
            await UpdateFileViewAsync();
        }
    }

    
}
