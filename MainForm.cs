using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        Network net = null;
        string project_dir = string.Empty;
        IntPtr window_handle = IntPtr.Zero;
        Size window_resolution = new Size(1, 1);
        int[] key_bind_codes = new int[0];
        float[] net_outputs = new float[0];
        bool net_busy = false;

        const int SRCCOPY = 0x00CC0020;
        const uint KEYEVENTF_KEYUP = 0x0002;

        #region Dll Imports

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripDropDownButton("File", null, new ToolStripItem[] {
                    new ToolStripDropDownButton("New", null, new ToolStripItem[] {
                        new ToolStripButton("Project", null, new EventHandler(new_project))
                    }),
                    new ToolStripButton("Open", null, new EventHandler(open_project)),
                    new ToolStripSeparator(),
                    new ToolStripButton("Save", null, new EventHandler(save_project))
                })
            });
        }

        #region Event Handlers

        private void new_project(object sender, EventArgs e)
        {
            using (NewProjectForm new_project_form = new NewProjectForm())
            {
                if (new_project_form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                net = new_project_form.net;
                project_dir = new_project_form.project_dir;
                window_handle = new_project_form.window_handle;
                window_resolution = new_project_form.window_resolution;
                key_bind_codes = new_project_form.key_bind_codes.ToArray();
            }
            net_outputs = net.getOutputs();
            windowPictureBox.Image = new Bitmap(GetWindowCaptureAsBitmap(window_handle), window_resolution.Width, window_resolution.Height);
        }

        private void open_project(object sender, EventArgs e)
        {
            //browse folders
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            project_dir = folderBrowserDialog.SelectedPath;

            //validate project folder
            //network file
            if (!File.Exists(project_dir + "\\Network.xml"))
            {
                open_project_error("Network file missing");
                return;
            }

            //properties file
            if (!File.Exists(project_dir + "\\Properties.xml"))
            {
                open_project_error("Properties file missing");
                return;
            }

            //load network
            Network loading_net = new Network();
            //read and load xml document line by line
            using (FileStream stream = new FileStream(project_dir + "\\Network.xml", FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {

                    //input nodes
                    if (!reader_search_for_element(reader, "input_nodes"))
                    {
                        open_project_error("Network incorrectly formatted");
                        return;
                    }
                    int input_node_count = Convert.ToInt32(reader.GetAttribute("size"));
                    loading_net.input_nodes = new InputNode[input_node_count];
                    for (int in_node_index = 0; in_node_index < input_node_count; in_node_index++)
                    {
                        loading_net.input_nodes[in_node_index] = new InputNode();
                        if (!reader_search_for_element(reader, "weights"))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        int weight_count = Convert.ToInt32(reader.GetAttribute("size"));
                        loading_net.input_nodes[in_node_index].weights = new float[weight_count];
                        for (int weight_index = 0; weight_index < weight_count; weight_index++)
                        {
                            if (!reader_search_for_type(reader, XmlNodeType.Text))
                            {
                                open_project_error("Network incorrectly formatted");
                                return;
                            }
                            loading_net.input_nodes[in_node_index].weights[weight_index] = float.Parse(reader.Value);
                        }
                    }

                    //middle nodes
                    if (!reader_search_for_element(reader, "middle_nodes"))
                    {
                        open_project_error("Network incorrectly formatted");
                        return;
                    }
                    int middle_node_count = Convert.ToInt32(reader.GetAttribute("size"));
                    loading_net.middle_nodes = new CalcNode[middle_node_count];
                    for (int mid_node_index = 0; mid_node_index < middle_node_count; mid_node_index++)
                    {
                        loading_net.middle_nodes[mid_node_index] = new CalcNode();
                        if (!reader_search_for_type(reader, XmlNodeType.Text))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        loading_net.middle_nodes[mid_node_index].current_value = float.Parse(reader.Value);
                        if (!reader_search_for_element(reader, "weights"))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        int weight_count = Convert.ToInt32(reader.GetAttribute("size"));
                        loading_net.middle_nodes[mid_node_index].weights = new float[weight_count];
                        for (int weight_index = 0; weight_index < weight_count; weight_index++)
                        {
                            if (!reader_search_for_type(reader, XmlNodeType.Text))
                            {
                                open_project_error("Network incorrectly formatted");
                                return;
                            }
                            loading_net.middle_nodes[mid_node_index].weights[weight_index] = float.Parse(reader.Value);
                        }
                        if (!reader_search_for_element(reader, "inbound_values"))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        int inbound_count = Convert.ToInt32(reader.GetAttribute("size"));
                        loading_net.middle_nodes[mid_node_index].inbound_values = new float[inbound_count];
                        for (int inbound_index = 0; inbound_index < inbound_count; inbound_index++)
                        {
                            if (!reader_search_for_type(reader, XmlNodeType.Text))
                            {
                                open_project_error("Network incorrectly formatted");
                                return;
                            }
                            loading_net.middle_nodes[mid_node_index].inbound_values[inbound_index] = float.Parse(reader.Value);
                        }
                        if (!reader_search_for_type(reader, XmlNodeType.Text))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        loading_net.middle_nodes[mid_node_index].bias = float.Parse(reader.Value);
                    }

                    //output nodes
                    if (!reader_search_for_element(reader, "output_nodes"))
                    {
                        open_project_error("Network incorrectly formatted");
                        return;
                    }
                    int output_node_count = Convert.ToInt32(reader.GetAttribute("size"));
                    loading_net.output_nodes = new CalcNode[output_node_count];
                    for (int out_node_index = 0; out_node_index < output_node_count; out_node_index++)
                    {
                        loading_net.output_nodes[out_node_index] = new CalcNode();
                        if (!reader_search_for_type(reader, XmlNodeType.Text))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        loading_net.output_nodes[out_node_index].current_value = float.Parse(reader.Value);
                        if (!reader_search_for_element(reader, "weights"))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        int weight_count = Convert.ToInt32(reader.GetAttribute("size"));
                        loading_net.output_nodes[out_node_index].weights = new float[weight_count];
                        for (int weight_index = 0; weight_index < weight_count; weight_index++)
                        {
                            if (!reader_search_for_type(reader, XmlNodeType.Text))
                            {
                                open_project_error("Network incorrectly formatted");
                                return;
                            }
                            loading_net.output_nodes[out_node_index].weights[weight_index] = float.Parse(reader.Value);
                        }
                        if (!reader_search_for_element(reader, "inbound_values"))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        int inbound_count = Convert.ToInt32(reader.GetAttribute("size"));
                        loading_net.output_nodes[out_node_index].inbound_values = new float[inbound_count];
                        for (int inbound_index = 0; inbound_index < inbound_count; inbound_index++)
                        {
                            if (!reader_search_for_type(reader, XmlNodeType.Text))
                            {
                                open_project_error("Network incorrectly formatted");
                                return;
                            }
                            loading_net.output_nodes[out_node_index].inbound_values[inbound_index] = float.Parse(reader.Value);
                        }
                        if (!reader_search_for_type(reader, XmlNodeType.Text))
                        {
                            open_project_error("Network incorrectly formatted");
                            return;
                        }
                        loading_net.output_nodes[out_node_index].bias = float.Parse(reader.Value);
                    }
                }
            }
            net = loading_net;
            net_outputs = net.getOutputs();

            //load properties
            using (FileStream stream = new FileStream(project_dir + "\\Properties.xml", FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    //window handle
                    if (!reader_search_for_type(reader, XmlNodeType.Text))
                    {
                        open_project_error("Properties incorrectly formatted");
                        return;
                    }
                    window_handle = FindWindowByCaption(IntPtr.Zero, reader.Value);
                    if (window_handle == IntPtr.Zero)
                    {
                        if (MessageBox.Show("Could not find selected window. Please select a new one") == DialogResult.Cancel)
                        {
                            open_project_error("No window found");
                            return;
                        }
                        using (SelectWindowForm select_window_form = new SelectWindowForm())
                        {
                            if (select_window_form.ShowDialog() == DialogResult.Cancel)
                            {
                                open_project_error("No window found");
                                return;
                            }
                            window_handle = select_window_form.selected_window_handle;
                        }
                    }

                    //window width
                    if (!reader_search_for_type(reader, XmlNodeType.Text))
                    {
                        open_project_error("Properties incorrectly formatted");
                        return;
                    }
                    window_resolution.Width = Convert.ToInt32(reader.Value);

                    //window height
                    if (!reader_search_for_type(reader, XmlNodeType.Text))
                    {
                        open_project_error("Properties incorrectly formatted");
                        return;
                    }
                    window_resolution.Height = Convert.ToInt32(reader.Value);

                    //key binds
                    if (!reader_search_for_element(reader, "key_binds"))
                    {
                        open_project_error("Properties incorrectly formatted");
                        return;
                    }
                    int bind_count = Convert.ToInt32(reader.GetAttribute("size"));
                    key_bind_codes = new int[bind_count];
                    for (int i = 0; i < bind_count; i++)
                    {
                        if (!reader_search_for_type(reader, XmlNodeType.Text))
                        {
                            open_project_error("Properties incorrectly formatted");
                            return;
                        }
                        key_bind_codes[i] = Convert.ToInt32(reader.Value);
                    }
                }
            }
            windowPictureBox.Image = new Bitmap(GetWindowCaptureAsBitmap(window_handle), window_resolution.Width, window_resolution.Height);
        }

        private void save_project(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(project_dir))
            {
                MessageBox.Show("Create a new project or open an existing network to save");
                return;
            }

            //save network
            using (XmlWriter writer = XmlWriter.Create(project_dir + "\\Network.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Network");

                //write input nodes
                writer.WriteStartElement("input_nodes");
                writer.WriteAttributeString("size", net.input_nodes.Length.ToString());
                foreach (InputNode in_node in net.input_nodes)
                {
                    writer.WriteStartElement("weights");
                    writer.WriteAttributeString("size", in_node.weights.Length.ToString());
                    foreach (float weight in in_node.weights)
                    {
                        writer.WriteStartElement("w");
                        writer.WriteEndElement();
                        writer.WriteString(weight.ToString());
                    }
                    writer.WriteEndElement();   //end weights
                }
                writer.WriteEndElement();   //end input_nodes

                //write middle nodes
                writer.WriteStartElement("middle_nodes");
                writer.WriteAttributeString("size", net.middle_nodes.Length.ToString());
                foreach (CalcNode mid_node in net.middle_nodes)
                {
                    writer.WriteStartElement("c");
                    writer.WriteEndElement();
                    writer.WriteString(mid_node.current_value.ToString());
                    writer.WriteStartElement("weights");
                    writer.WriteAttributeString("size", mid_node.weights.Length.ToString());
                    foreach (float weight in mid_node.weights)
                    {
                        writer.WriteStartElement("w");
                        writer.WriteEndElement();
                        writer.WriteString(weight.ToString());
                    }
                    writer.WriteEndElement(); //end weights
                    writer.WriteStartElement("inbound_values");
                    writer.WriteAttributeString("size", mid_node.inbound_values.Length.ToString());
                    foreach (float value in mid_node.inbound_values)
                    {
                        writer.WriteStartElement("v");
                        writer.WriteEndElement();
                        writer.WriteString(value.ToString());
                    }
                    writer.WriteEndElement();   //end inbound_values
                    writer.WriteStartElement("b");
                    writer.WriteEndElement();
                    writer.WriteString(mid_node.bias.ToString());
                }
                writer.WriteEndElement();   //end middle_nodes

                //write output nodes
                writer.WriteStartElement("output_nodes");
                writer.WriteAttributeString("size", net.output_nodes.Length.ToString());
                foreach (CalcNode out_node in net.output_nodes)
                {
                    writer.WriteStartElement("c");
                    writer.WriteEndElement();
                    writer.WriteString(out_node.current_value.ToString());
                    writer.WriteStartElement("weights");
                    writer.WriteAttributeString("size", out_node.weights.Length.ToString());
                    foreach (float weight in out_node.weights)
                    {
                        writer.WriteStartElement("w");
                        writer.WriteEndElement();
                        writer.WriteString(weight.ToString());
                    }
                    writer.WriteEndElement(); //end weights
                    writer.WriteStartElement("inbound_values");
                    writer.WriteAttributeString("size", out_node.inbound_values.Length.ToString());
                    foreach (float value in out_node.inbound_values)
                    {
                        writer.WriteStartElement("v");
                        writer.WriteEndElement();
                        writer.WriteString(value.ToString());
                    }
                    writer.WriteEndElement();   //end inbound_values
                    writer.WriteStartElement("b");
                    writer.WriteEndElement();
                    writer.WriteString(out_node.bias.ToString());
                }
                writer.WriteEndElement();   //end output_nodes

                writer.WriteEndElement();   //end Network
                writer.WriteEndDocument();
            }

            //save properties
            using (XmlWriter writer = XmlWriter.Create(project_dir + "\\Properties.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Properties");

                //window handle
                writer.WriteStartElement("window");
                writer.WriteEndElement();
                writer.WriteString(GetWindowText(window_handle));

                //resolution
                writer.WriteStartElement("width");
                writer.WriteEndElement();
                writer.WriteString(window_resolution.Width.ToString());
                writer.WriteStartElement("height");
                writer.WriteEndElement();
                writer.WriteString(window_resolution.Height.ToString());

                //key binds
                writer.WriteStartElement("key_binds");
                writer.WriteAttributeString("size", key_bind_codes.Length.ToString());
                foreach (int code in key_bind_codes)
                {
                    writer.WriteStartElement("c");
                    writer.WriteEndElement();
                    writer.WriteString(code.ToString());
                }
                writer.WriteEndElement();   //end key_binds

                writer.WriteEndElement();   //end Properties
                writer.WriteEndDocument();
            }
        }

        private void randomizeNetworkButton_Click(object sender, EventArgs e)
        {
            if (net != null)
            {
                net.randomize();
            }
        }

        private void sendOutputsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!net_busy)
            {
                if (sendOutputsCheckBox.Checked && net != null)
                {
                    SendOutputs();
                }
            }
            else
            {
                sendOutputsCheckBox.Checked = false;
            }
        }

        #endregion

        #region Helper Functions

        private void open_project_error(string message)
        {
            MessageBox.Show(message);
            net = null;
            project_dir = string.Empty;
            window_handle = IntPtr.Zero;
            window_resolution = new Size(1, 1);
            key_bind_codes = new int[0];
            net_outputs = new float[0];
        }

        private bool reader_search_for_element(XmlReader reader, string name)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == name)
                {
                    return true;
                }
            }
            return false;
        }

        private bool reader_search_for_type(XmlReader reader, XmlNodeType type)
        {
            while (reader.Read())
            {
                if (reader.NodeType == type)
                {
                    return true;
                }
            }
            return false;
        }

        private float[] BitmapToFloatArray(Bitmap bmp)
        {
            float[] outArray = new float[bmp.Width * bmp.Height];
            int arrIndex = 0;
            for (int col = 0; col < bmp.Width; col++)
            {
                for (int row = 0; row < bmp.Height; row++)
                {
                    outArray[arrIndex++] = ColorToInt(bmp.GetPixel(col, row));
                }
            }
            return outArray;
        }

        public static int ColorToInt(Color c)
        {
            return (c.R << 0) | (c.G << 8) | (c.B << 16);
        }

        #endregion

        #region Get Inputs and Send Outputs

        private static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        private Bitmap GetWindowCaptureAsBitmap(IntPtr handle)
        {
            Rectangle rc = new Rectangle();
            if (!GetWindowRect(handle, ref rc))
            {
                return null;
            }
            Bitmap bitmap = new Bitmap(rc.Width - rc.X, rc.Height - rc.Y);
            using (Graphics gfxBitmap = Graphics.FromImage(bitmap))
            {
                IntPtr hdcBitmap = gfxBitmap.GetHdc();
                IntPtr hdcWindow = GetWindowDC(handle);
                BitBlt(hdcBitmap, 0, 0, rc.Width - rc.X, rc.Height - rc.Y, hdcWindow, 0, 0, SRCCOPY);

                gfxBitmap.ReleaseHdc(hdcBitmap);
                ReleaseDC(handle, hdcWindow);
            }
            return bitmap;
        }

        private async void SendOutputs()
        {
            while (sendOutputsCheckBox.Checked)
            {
                net_busy = true;
                if (await Task.Run(() => GetForegroundWindow()) == window_handle)
                {
                    await Task.Run(() => net_outputs = net.step(BitmapToFloatArray(new Bitmap(GetWindowCaptureAsBitmap(window_handle), window_resolution.Width, window_resolution.Height))));
                    for (int i = 0; i < net_outputs.Length; i++)
                    {
                        if (net_outputs[i] >= 0)
                        {
                            keybd_event((byte)key_bind_codes[i], 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                        }
                        else
                        {
                            keybd_event((byte)key_bind_codes[i], 0, 0, UIntPtr.Zero);
                        }
                    }
                }
            }
            for (int i = 0; i < net_outputs.Length; i++)
            {
                keybd_event((byte)key_bind_codes[i], 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
            net_busy = false;
        }

        #endregion
    }
}
