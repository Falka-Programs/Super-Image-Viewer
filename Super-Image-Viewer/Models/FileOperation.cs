using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Image_Viewer.Models
{
    enum Operations {
    Copy,
    Paste,
    Cut,
    None
    }
    internal class FileOperation
    {
        public List<string> FilePathes { get; set; }
        public Operations operation;
        public FileOperation()
        {
            FilePathes = new List<string>();
            operation = Operations.None;
        }
    }
}
