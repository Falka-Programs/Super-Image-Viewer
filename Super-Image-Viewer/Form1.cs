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
    public partial class Form1 : Form
    {
        FileSystemViewer fsv;
        ImageList iList;
        public Form1()
        {
            InitializeComponent();
            fsv = new FileSystemViewer();
            fsv.MoveTo("..\\");
            fsv.previousPath = null;
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
            string cur_path = fsv.CurrentPath;
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
                        if (fsv.CurrentPath != cur_path)
                            return;
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

            if (fsv.IsBackAvaible())
            {
                back_button.Enabled = true;
            }
            else
                back_button.Enabled = false;
            if (fsv.IsFrontAvaible())
            {
                front_button.Enabled = true;
            }else
                front_button.Enabled = false;
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
                    try
                    {
                        fsv.MoveTo(File_View.SelectedItems[0].Text);
                        path_textBox.Text = fsv.CurrentPath;
                    }
                    catch
                    {
                        MessageBox.Show("You don`t have permissions to access this folder!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                if (tabControl.SelectedIndex != 0)
                {
                    tabControl.SelectedIndex = 0;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void item_menuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(fsv.fileOperation.operation != Operations.None)
            {
                item_menuStrip.Items[2].Enabled = true;
            }
            else
            {
                item_menuStrip.Items[2].Enabled = false;
            }

            if(File_View.SelectedIndices.Count > 0)
            {
                item_menuStrip.Items[0].Enabled = true;
                item_menuStrip.Items[1].Enabled = true;
                item_menuStrip.Items[3].Enabled = true;
            }
            else
            {
                item_menuStrip.Items[0].Enabled = false;
                item_menuStrip.Items[1].Enabled = false;
                item_menuStrip.Items[3].Enabled = false;
            }

        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File_View.SelectedIndices.Count > 0)
            {
                DialogResult df = MessageBox.Show($"Delete {File_View.SelectedIndices.Count} files?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (df == DialogResult.Yes)
                {
                    for (int i = 0; i < File_View.SelectedIndices.Count; i++)
                    {
                        if (File_View.SelectedItems[i].ImageIndex == 0)
                            fsv.DeleteInCurrentDirectory(File_View.SelectedItems[i].Text);
                        if (File_View.SelectedItems[i].ImageIndex >= 1)
                            fsv.DeleteInCurrentFile(File_View.SelectedItems[i].Text);
                    }
                    await UpdateFileViewAsync();
                }
            }
        }

        private async void front_button_Click(object sender, EventArgs e)
        {
            fsv.CurrentPath = fsv.previousPath;
            fsv.previousPath = null;
            
            await UpdateFileViewAsync();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fsv.fileOperation.FilePathes.Clear();
            if(File_View.SelectedIndices.Count > 0)
            {
                for(int i = 0; i < File_View.SelectedIndices.Count; i++)
                {
                    fsv.fileOperation.FilePathes.Add(fsv.GetFilePath(File_View.SelectedItems[i].Text));
                }
                fsv.fileOperation.operation = Operations.Copy;
            }
        }

        private async void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fsv.fileOperation.operation == Operations.Copy)
            {
                bool tg = false;
                for(int i = 0; i < fsv.fileOperation.FilePathes.Count; i++)
                {
                    try
                    {
                        fsv.Copy(fsv.fileOperation.FilePathes[i], fsv.CurrentPath);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        tg = true;
                    }
                }
                if (tg)
                    MessageBox.Show("Directories skipped", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await UpdateFileViewAsync();
            }
        }
    }

    
}
