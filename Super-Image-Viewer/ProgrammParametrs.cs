using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Super_Image_Viewer
{
    class ProgrammParametrs
    {
        public bool ShowImagePreview;
        public bool IntelegentImagePreview;
        string filePath = @"../../Data/parametrs.xml";
        XDocument Document;
        public ProgrammParametrs()
        {
            try
            {
                Document = XDocument.Load(filePath);
                List<XElement> elements = Document.Root?.Element("parametrs")?.Elements("parametr")?.ToList();
                if (elements.Count > 1)
                {
                    if (elements[0].Value == "true")
                        ShowImagePreview = true;
                    else
                        ShowImagePreview = false;
                    if (elements[1].Value == "true")
                        IntelegentImagePreview = true;
                    else
                        IntelegentImagePreview = false;
                }
            }
            catch
            {
                LoadDefaults();
            }
        }

        public void UpdateSetting()
        {
            List<XElement> elements = Document.Root.Element("parametrs").Elements("parametr").ToList();
            elements[0] = new XElement(ShowImagePreview.ToString());
            elements[1] = new XElement(IntelegentImagePreview.ToString());
            Document.Save(filePath);
        }

        
        private void LoadDefaults()
        {
            ShowImagePreview = true;
            IntelegentImagePreview = false;
        }
    }
}
