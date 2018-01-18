using mshtml;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FBE
{
    public class Document
    {
        readonly MainForm form;
        readonly WebBrowser view;

        int documentVersion = -1;
        IMarkupContainer2 markupContainer;

        const string DefaultFileName = "blank.fb2";

        public string FileName { get; private set; }

        public bool IsModified
        {
            get
            {
                bool isModified = false;
                if (markupContainer != null)
                    isModified = markupContainer.GetVersionNumber() != documentVersion;
                return isModified;
            }
        }

        public Document(MainForm form, WebBrowser view)
        {
            this.view = view ?? throw new ArgumentNullException("view");
            this.form = form ?? throw new ArgumentNullException("form");
            this.view.DocumentCompleted += View_DocumentCompleted;
        }

        private void View_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            view.ObjectForScripting = new ExternalHelper(form);
            view.Document.InvokeScript("apiLoadFB2", new object[] { FileName, "RUS" });
            markupContainer = view.Document.DomDocument as IMarkupContainer2;
            MarkSavePoint();
        }

        public void New()
        {
            FileName = Path.Combine(Environment.CurrentDirectory, DefaultFileName);
            Open(FileName);
        }

        public void Open(string filename)
        {
            Close();
            FileName = filename;
            string path = Path.Combine(Environment.CurrentDirectory, "main.html");
            view.Navigate(path);
        }

        public void Close()
        {
            documentVersion = -1;
            if (markupContainer != null)
                Marshal.ReleaseComObject(markupContainer);
        }


        private void MarkSavePoint()
        {
            if (markupContainer != null)
                documentVersion = markupContainer.GetVersionNumber();
        }
    }
}