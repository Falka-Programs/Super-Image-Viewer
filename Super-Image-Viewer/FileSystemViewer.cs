using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Super_Image_Viewer.Models;


namespace Super_Image_Viewer
{
    class FileSystemViewer
    {
        private string[] ImageFormats = { "png","jpg","jpeg","gif" };
        public string CurrentImagePath;
        public string CurrentPath { get
            {
                return currentPath;
            }
            set
            {
                try
                {
                    UpdatePath(value);
                    currentPath = value;
                }
                catch(Exception err)
                {
                    throw new ArgumentOutOfRangeException(err.Message);
                }
                
            }
        }
        private string currentPath;
        public string previousPath;

        public FileOperation fileOperation;

        private DirectoryInfo[] Directories;
        private FileInfo[] Files;
        DirectoryInfo df;
        private void UpdatePath(string newPath)
        {
            
            try
            {
                df = new DirectoryInfo(newPath);
                Files = df.GetFiles();
                Directories = df.GetDirectories();

            }
            catch(Exception err)
            {
                throw new ArgumentException($"{err.Message}");
            }
        }
        public FileSystemViewer()
        {
            CurrentPath = Directory.GetCurrentDirectory();
            fileOperation = new FileOperation();
        }
        public FileSystemViewer(string path)
        {
            if (Directory.Exists(path))
                CurrentPath = path;
        }
        public FileInfo[] GetFiles()
        {
            return Files;
        }
        public void Update()
        {
            CurrentPath = CurrentPath;
        }
        public void Copy(string path1,string path2)
        {
            if (Directory.Exists(path2))
            {
                if (File.Exists(path1))
                {
                    string[] frm = path1.Split('\\');
                    string OriginalfileName = frm[frm.Length - 1];
                    string fileName = frm[frm.Length - 1];
                    string newPath2 = path2 +fileName;
                    int counter = 1;
                    while(File.Exists(newPath2))
                    {
                        if (fileName.Split('.').Length > 1)
                        {
                            fileName = OriginalfileName.Split('.')[OriginalfileName.Split('.').Length-2] + counter.ToString() +'.'+ OriginalfileName.Split('.')[OriginalfileName.Split('.').Length-1];
                        }
                        else
                            return;

                        newPath2 = path2 + fileName;
                        counter++;
                    }
                    File.Copy(path1,newPath2);
                    
                }else if (Directory.Exists(path1))
                {
                    throw new ArgumentException("Directory copy isn`t supported");
                }

            }
        }
        public Task CopyAsync(string path1, string path2)
        {
            return Task.Run(() =>
            {
                Copy(path1,path2);
            });
        }
        public void GoToDefaultPath()
        {
            CurrentPath = Directory.GetCurrentDirectory();
        }

        public void Cut(string path1,string path2)
        {
            if (Directory.Exists(path2))
            {
                if (File.Exists(path1))
                {
                    string[] frm = path1.Split('\\');
                    string OriginalfileName = frm[frm.Length - 1];
                    string fileName = frm[frm.Length - 1];
                    string newPath2 = path2 + fileName;
                    int counter = 1;
                    while (File.Exists(newPath2))
                    {
                        if (fileName.Split('.').Length > 1)
                        {
                            fileName = OriginalfileName.Split('.')[OriginalfileName.Split('.').Length - 2] + counter.ToString() + '.' + OriginalfileName.Split('.')[OriginalfileName.Split('.').Length - 1];
                        }
                        else
                            return;

                        newPath2 = path2 + fileName;
                        counter++;
                    }
                    File.Copy(path1, newPath2);
                    File.Delete(path1);

                }
                else if (Directory.Exists(path1))
                {
                    throw new ArgumentException("Directory cut isn`t supported");
                }

            }
        }
        public Task CutAsync(string path1, string path2)
        {
            return Task.Run(() =>
            {
                Cut(path1, path2);
            });
        }
        public DirectoryInfo[] GetDirectories()
        {
            return Directories;
        }
        public void MoveTo(string folderName)
        {
            previousPath = CurrentPath;
            if(folderName == "..\\")
            {
                string[] tmp = CurrentPath.Split('\\');
                if (tmp.Length > 2)
                {
                    string[] res = new string[tmp.Length - 1];
                    for(int i = 0; i < tmp.Length-2; i++)
                    {
                        res[i] = tmp[i];
                    }
                    string path = "";
                    for (int i = 0; i < res.Length; i++)
                    {
                        if (i != res.Length - 1)
                            path += $"{res[i]}\\";
                        else
                            path += res[i];
                    }
                    CurrentPath = path;
                }
            }
            else if (Directory.Exists($"{CurrentPath}/{folderName}"))
            {
                CurrentPath = $"{CurrentPath}{folderName}\\";
            }
        }

        public string GetFilePath(string path)
        {
            return $"{CurrentPath}{path}";
        }
        public bool IsImage(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                string[] splitted_name = name.Split('.');
                if (splitted_name.Length > 1)
                {
                    if(ImageFormats.Contains(splitted_name[splitted_name.Length - 1].ToLower()))
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public void DeleteInCurrentDirectory(string directory)
        {
            if (Directory.Exists(CurrentPath + directory))
            {
                Directory.Delete(CurrentPath + directory, true);
            }
        }
        public void DeleteInCurrentFile(string file)
        {
            if (File.Exists(CurrentPath +file))
            {
                File.Delete(CurrentPath + file);
            }
        }
        public bool IsBackAvaible()
        {
            if (currentPath.Split('\\').Length > 2)
                return true;
            else
                return false;
        }
        public bool IsFrontAvaible()
        {
            if (previousPath != null && !String.IsNullOrWhiteSpace(previousPath))
                return true;
            else
                return false;
        }

        public string[] GetDrivesLetters()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            int size = 0;
            for(int i = 0; i < drives.Length; i++)
                if(drives[i].DriveType == DriveType.Fixed)
                    size++;

            string[] tmp = new string[size];
            int currentId = 0;
            for (int i = 0; i < drives.Length; i++)
                if (drives[i].DriveType == DriveType.Fixed)
                {
                    tmp[currentId] = drives[i].RootDirectory.FullName;
                    currentId++;
                }
            
            return tmp;
        }
    }
}
