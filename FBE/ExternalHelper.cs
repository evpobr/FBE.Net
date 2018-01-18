using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using mshtml;
using System.Drawing;

namespace FBE
{
    [ComVisible(true)]
    public class ExternalHelper
    {
        readonly MainForm form;

        public ExternalHelper(MainForm form)
        {
            this.form = form;
        }

        public string GetImageDimsByData(object data)
        {
            int width = 0;
            int height = 0;

            try
            {
                using (var ms = new MemoryStream(data as byte[]))
                {
                    using (var image = Image.FromStream(ms))
                    {
                        width = image.Width;
                        height = image.Height;
                    }
                }
            }
            finally
            {

            }

            return String.Format("{0}x{1}", width, height);
        }

        public string GetNBSP()
        {
            return @"\u0160";
        }

        public string GetStylePath()
        {
            return Directory.GetCurrentDirectory();
        }

        public int GetBinarySize(object data)
        {
            return (data as byte[]).Length;
        }

        public void InflateParagraphs(object data)
        {
            var elem = data as IHTMLElement2;
            IHTMLElementCollection pp = elem.getElementsByTagName("P");
            int length = pp.length;
            for (int i = 0; i < length; i++)
            {
                pp.item(i).inflateBlock = true;
            }
        }

        public string GetUUID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        public bool GetExtendedStyle(string elem)
        {
            bool ret = false;
            bool value;
            if (form.descInfo.TryGetValue(elem, out value))
                ret = value;

            return ret;
        }

        public void DescShowElement(string elem, bool show)
        {
            form.descInfo[elem] = show;
        }

        public string GetProgramVersion()
        {
            return "FBE.Net";
        }
    }
}
