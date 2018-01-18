using AsyncPluggableProtocol;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FBE
{
    public partial class MainForm : Form
    {
        readonly Document document;

        public readonly Dictionary<string, bool> descInfo = new Dictionary<string, bool>()
        {

            { "ci_all", true },
            { "sti_all", false },
            { "di_id", true },
            { "id", true },
            { "ti_kw", true },
            { "ti_nic_mail_web", true },
            { "ti_genre_match", true }
        };

        public HtmlDocument HtmlDocument { get { return webBrowser.Document; } }

         public MainForm()
        {
            InitializeComponent();
            document = new FBE.Document(this, webBrowser);
        }

         private void MainForm_Load(object sender, EventArgs e)
        {
            ProtocolFactory.Register("fbw-internal", () => new FbwInternalProtocol(this));
            document.New();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            document.New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                document.Open(openFileDialog.FileName);
        }
    }
}
