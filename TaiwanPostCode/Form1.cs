
using Postcode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TaiwanPostCode
{
    public partial class Form1 : Form
    {

        private OpenFileDialog openFileDialog1 = null;
        private IPostcodeImporter importer = null;
        public Form1()
        {
            InitializeComponent();
            initial();
        }

        void initial()
        {
            this.openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.Filter = "xml檔 (*.xml)|*.xml";
            importer = new PostcodeImporter();
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                await (importer.ImportAsync(openFileDialog1.FileName));
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = Path.Combine(Environment.CurrentDirectory, "postCode.xml");
                WebClient wc = new WebClient();
                var uriBuilder = new UriBuilder("http://download.post.gov.tw/post/download/Xml_10510.xml");

                this.label1.Text = "檔案下載中";
                await wc.DownloadFileTaskAsync(uriBuilder.Uri, fileName);
                this.label1.Text = "檔案下載完成";
                if (File.Exists(fileName))
                {
                   
                    await importer.ImportAsync(fileName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
