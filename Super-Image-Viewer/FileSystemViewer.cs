using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Super_Image_Viewer
{
    class FileSystemViewer
    {
        private string[] ImageFormats = { "png","jpg","jpeg","gif" };
        public string CurrentPath { get
            {
                return currentPath;
            }
            set
            {
                currentPath = value;
                UpdatePath(currentPath);
            }
        }
        private string currentPath;

        private DirectoryInfo[] Directories;
        private FileInfo[] Files;
        DirectoryInfo df;
        private void UpdatePath(string newPath)
        {
            df = new DirectoryInfo(CurrentPath);
            try
            {
                Files = df.GetFiles();
                Directories = df.GetDirectories();
            }
            catch
            {
                Files = new FileInfo[0];
                Directories = new DirectoryInfo[0];
            }
        }
        public FileSystemViewer()
        {
            CurrentPath = Directory.GetCurrentDirectory();
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

        public DirectoryInfo[] GetDirectories()
        {
            return Directories;
        }
        public void MoveTo(string folderName)
        {
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


        public bool IsImage(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                string[] splitted_name = name.Split('.');
                if (splitted_name.Length > 1)
                {
                    if(ImageFormats.Contains(splitted_name[splitted_name.Length - 1]))
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
    }
}
