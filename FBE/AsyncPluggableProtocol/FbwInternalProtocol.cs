using FBE;
using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncPluggableProtocol
{
    public class FbwInternalProtocol : IProtocol
    {
        readonly MainForm form;
        public FbwInternalProtocol(MainForm form)
        {
            this.form = form;
        }

        public string Name
        {
            get
            {
                return "fbw-internal";
            }
        }

        public Task<Stream> GetStreamAsync(string url)
        {
            Uri uri = new Uri(url);
            string imageUrl = uri.Fragment.Substring(1);
            var document = form.HtmlDocument;
            byte[] data = (byte[])document.InvokeScript("apiGetBinary", new object[] { imageUrl });
            Stream stream = new MemoryStream(data);
            return Task.FromResult(stream);
        }
    }
}
