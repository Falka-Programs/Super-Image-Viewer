using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
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
                    if (elements[0].Value == "True")
                        ShowImagePreview = true;
                    else
                        ShowImagePreview = false;
                    if (elements[1].Value == "True")
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
            var items = from item in Document.Root.Elements("parametrs").Elements("parametr")
                        where item.Attribute("name").Value == "ShowImagePreview"
                        select item;
            foreach (var item in items)
            {
                item.SetValue(ShowImagePreview.ToString());
            }

            var items2 = from item in Document.Root.Elements("parametrs").Elements("parametr")
                         where item.Attribute("name").Value == "IntelegentImagePreview"
                         select item;

            foreach (var item in items2)
            {
                item.SetValue(IntelegentImagePreview.ToString());
            }
            Document.Save(filePath);
            Console.WriteLine("Saving");
        }

        
        private void LoadDefaults()
        {
            ShowImagePreview = true;
            IntelegentImagePreview = false;
        }
    }
}
