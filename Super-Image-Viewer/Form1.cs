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
using System.Threading;
using Super_Image_Viewer.Models;
using System.Drawing.Imaging;

namespace Super_Image_Viewer
{
    public partial class Form1 : Form
    {
        FileSystemViewer fsv;
        ImageList iList;
        ProgrammParametrs programmParametrs;
        public Form1()
        {
            InitializeComponent();
            programmParametrs = new ProgrammParametrs();
            fsv = new FileSystemViewer();
            fsv.MoveTo("..\\");
            fsv.previousPath = null;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            LoadIcons();
            LoadDrives();
            fsv.GoToDefaultPath();
            Console.WriteLine(fsv.CurrentPath);
            await UpdateFileViewAsync();
            
        }
        private void LoadDrives()
        {
            string[] drives = fsv.GetDrivesLetters();
            foreach(var item in drives)
            {
                drives_listBox.Items.Add(item);
            }
            if (drives_listBox.Items.Count > 0)
                drives_listBox.SelectedIndex = 0;
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
            fsv.Update();
            CleanIcons();
            //NOTE elements in array has index one less that elements in FileView
            //This caused by element ..\ that returns back in directory
            File_View.Items.Clear();
            File_View.Items.Add(@"..\", 0);
            path_textBox.Text = fsv.CurrentPath;
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
                    if(programmParametrs.ShowImagePreview==false)
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
                       // path_textBox.Text = fsv.CurrentPath;
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

        private void SetActiveTab(int tab)
        {
            Action action = () =>
            {
                this.tabControl.SelectedIndex = tab;
            };
            if (this.tabControl.InvokeRequired)
            {
                Invoke(action);
            }
            else{
                action();
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
            //path_textBox.Text = fsv.CurrentPath;
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
                        
                        await fsv.CopyAsync(fsv.fileOperation.FilePathes[i], fsv.CurrentPath);
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
            else if (fsv.fileOperation.operation == Operations.Cut)
            {
                bool tg = false;
                for (int i = 0; i < fsv.fileOperation.FilePathes.Count; i++)
                {
                    try
                    {

                        await fsv.CutAsync(fsv.fileOperation.FilePathes[i], fsv.CurrentPath);
                    }
                    catch (Exception ex)
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

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fsv.fileOperation.FilePathes.Clear();
            if (File_View.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < File_View.SelectedIndices.Count; i++)
                {
                    fsv.fileOperation.FilePathes.Add(fsv.GetFilePath(File_View.SelectedItems[i].Text));
                }
                fsv.fileOperation.operation = Operations.Cut;
            }
        }

        private async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            try
            {
                pictureBox.Image = Image.FromFile(files[files.Length - 1]);
            }
            catch
            {
                MessageBox.Show("File format not supported","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                string[] data_path = files[files.Length - 1].Split('\\');
            if (data_path.Length > 2)
            {
                string path = "";
                for(int i = 0; i < data_path.Length-1; i++)
                    path += data_path[i] + '\\';
                try
                {
                    fsv.CurrentPath = path;
                    fsv.CurrentImagePath = path;
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Internal error\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SetActiveTab(1);
            await UpdateFileViewAsync();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void saveToToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pictureBox.Image != null)
            {
                saveFileDialog1.FileName = "Export image";
                saveFileDialog1.Filter = "Png file(*.png)|*.png|Jpeg file(*.jpg)|*.jpg";
                DialogResult dr = saveFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string path = saveFileDialog1.FileName;
                    string format = path.Split('.')[path.Split('.').Length - 1];
                    if (format == "png")
                    {
                        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        ImageCodecInfo EncodeInfo=null;
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                        myEncoderParameters.Param[0] = myEncoderParameter;

                        foreach (var item in encoders)
                        {
                            if(item.CodecName == "Built-in PNG Codec")
                            {
                                EncodeInfo = item;
                                break;
                            }
                        }
                        if (EncodeInfo != null)
                            pictureBox.Image.Save(path, EncodeInfo, myEncoderParameters);

                    }else if(format == "jpg")
                    {
                        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        ImageCodecInfo EncodeInfo = null;
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                        myEncoderParameters.Param[0] = myEncoderParameter;

                        foreach (var item in encoders)
                        {
                            if (item.CodecName == "Built-in JPEG Codec")
                            {
                                EncodeInfo = item;
                                break;
                            }
                        }
                        if (EncodeInfo != null)
                            pictureBox.Image.Save(path, EncodeInfo, myEncoderParameters);
                    }
                    else
                    {
                        MessageBox.Show("Invalid format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private async void drives_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drives_listBox.SelectedIndex != -1)
            {
                try
                {
                    fsv.CurrentPath = drives_listBox.SelectedItem.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Internal error.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                await UpdateFileViewAsync();
            }
        }
    }

    
}
