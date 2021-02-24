using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Network current_net = null;
        string current_file = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStrip1.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripDropDownButton("File", null, new ToolStripItem[] {
                    new ToolStripButton("New", null, new EventHandler(new_network)),
                    new ToolStripButton("Open", null, new EventHandler(open_network)),
                    new ToolStripSeparator(),
                    new ToolStripButton("Save", null, new EventHandler(save_network)),
                    new ToolStripButton("Save As", null, new EventHandler(save_network_as))
                })
            });
        }

        private void new_network(object sender, EventArgs e)
        {
            current_net = new Network();
            current_file = string.Empty;
        }

        private void open_network(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = string.Empty;

            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == string.Empty)
            {
                return;
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(openFileDialog1.FileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Network);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        current_net = (Network)serializer.Deserialize(reader);
                    }
                }
                current_file = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }

        private void save_network(object sender, EventArgs e)
        {
            if (current_file == string.Empty)
            {
                save_network_as(sender, e);
            }

            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Network));
                writer = new StreamWriter(current_file, false);
                serializer.Serialize(writer, current_net);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private void save_network_as(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.ShowDialog();

            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Network));
                writer = new StreamWriter(saveFileDialog1.FileName, false);
                serializer.Serialize(writer, current_net);
                current_file = saveFileDialog1.FileName;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
