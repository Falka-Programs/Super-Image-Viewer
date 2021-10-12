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
    Cut
    }
    internal class FileOperation
    {
        public string FilePath { get; set; }
    }
}
