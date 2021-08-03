using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class NewProjectForm : Form
    {
        public string project_name = "New Project";
        public string project_dir;
        public IntPtr window_handle;
        public Size window_resolution = new Size(1, 1);
        public uint input_nodes = 1;
        public uint middle_nodes = 0;
        public uint output_nodes = 0;
        public Network net;
        public CheckBox[] key_binds = new CheckBox[KeyCodes.Names.Length];
        public List<int> key_bind_codes = new List<int>();

        readonly System.Windows.Forms.Timer window_refresh_timer = new System.Windows.Forms.Timer();

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        public NewProjectForm()
        {
            InitializeComponent();
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            int box_y = 467;
            for (int i = 0; i < KeyCodes.Names.Length; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Text = KeyCodes.Names[i];
                cb.AutoSize = false;
                cb.Size = new Size(132, 23);
                cb.Location = new Point(136, box_y += 29);
                cb.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                key_binds[i] = cb;
                Controls.Add(cb);
            }

            window_refresh_timer.Tick += new EventHandler(WindowRefreshTimerEvent);
            window_refresh_timer.Interval = 5000;
            window_refresh_timer.Start();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            project_dir = folderBrowserDialog.SelectedPath;
            projectDirLabel.Text = project_dir;
        }

        private void createButton_Click(object sender, EventArgs e)
        {

            //validate inputs

            //project name
            if (string.IsNullOrEmpty(project_name))
            {
                MessageBox.Show("Enter a project name");
                return;
            }

            //project directory
            if (string.IsNullOrEmpty(project_dir))
            {
                MessageBox.Show("Select a location");
                return;
            }

            //window
            if (window_handle == IntPtr.Zero)
            {
                MessageBox.Show("Select a window");
                return;
            }

            //output nodes
            if (output_nodes == 0)
            {
                MessageBox.Show("Select at least one output");
                return;
            }

            project_dir = Path.Combine(project_dir, project_name);

            //existing directory
            if (Directory.Exists(project_dir))
            {
                MessageBox.Show("File already exists in this location");
                return;
            }

            //set variables
            net = new Network((int)input_nodes, (int)middle_nodes, (int)output_nodes);
            foreach (CheckBox cb in key_binds)
            {
                if (cb.Checked)
                {
                    key_bind_codes.Add(KeyCodes.Codes[cb.Text]);
                }
            }

            //setup project files

            //project folder
            System.IO.Directory.CreateDirectory(project_dir);

            //network xml
            using (XmlWriter writer = XmlWriter.Create(project_dir + "\\Network.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Network");

                //write input nodes
                writer.WriteStartElement("input_nodes");
                writer.WriteAttributeString("size", input_nodes.ToString());
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
                writer.WriteAttributeString("size", middle_nodes.ToString());
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
                writer.WriteAttributeString("size", output_nodes.ToString());
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

            //properties xml
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
                writer.WriteAttributeString("size", key_bind_codes.Count.ToString());
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

            //close window
            DialogResult = DialogResult.OK;
            Close();
        }

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

        private void projectNameTextBox_TextChanged(object sender, EventArgs e)
        {
            project_name = projectNameTextBox.Text;
        }

        private void windowBrowseButton_Click(object sender, EventArgs e)
        {
            SelectWindowForm select_window_form = new SelectWindowForm();
            if (select_window_form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            window_handle = select_window_form.selected_window_handle;
            Rectangle rc = new Rectangle();
            GetWindowRect(window_handle, ref rc);
            window_resolution = new Size(rc.Width - rc.X, rc.Height - rc.Y);
            windowWidthTextBox.Text = window_resolution.Width.ToString();
            windowHeightTextBox.Text = window_resolution.Height.ToString();
            input_nodes = (uint)(window_resolution.Width * window_resolution.Height);
            inputNodesCountLabel.Text = input_nodes.ToString();
        }

        private void WindowRefreshTimerEvent(object sender, EventArgs e)
        {
            window_refresh_timer.Stop();
            UpdateWindow();
            window_refresh_timer.Start();
        }

        private void UpdateWindow()
        {
            if (window_handle != IntPtr.Zero)
            {
                windowPictureBox.Image = new Bitmap(GetWindowCaptureAsBitmap(window_handle), window_resolution.Width, window_resolution.Height);
            }
        }

        private static Bitmap GetWindowCaptureAsBitmap(IntPtr handle)
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
                BitBlt(hdcBitmap, 0, 0, rc.Width - rc.X, rc.Height - rc.Y, hdcWindow, 0, 0, 0x00CC0020);    //SRCCOPY = 0x00CC0020

                gfxBitmap.ReleaseHdc(hdcBitmap);
                ReleaseDC(handle, hdcWindow);
            }
            return bitmap;
        }

        private void windowWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                window_resolution.Width = Math.Max((int)Convert.ToUInt16(windowWidthTextBox.Text), 1);
                windowWidthTextBox.Text = window_resolution.Width.ToString();
                input_nodes = (uint)(window_resolution.Width * window_resolution.Height);
                inputNodesCountLabel.Text = input_nodes.ToString();
                UpdateWindow();
            }
            catch (Exception)
            {
                windowWidthTextBox.Text = window_resolution.Width.ToString();
            }
        }

        private void windowHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                window_resolution.Height = Math.Max((int)Convert.ToUInt16(windowHeightTextBox.Text), 1);
                windowHeightTextBox.Text = window_resolution.Height.ToString();
                input_nodes = (uint)(window_resolution.Width * window_resolution.Height);
                inputNodesCountLabel.Text = input_nodes.ToString();
                UpdateWindow();
            }
            catch
            {
                windowHeightTextBox.Text = window_resolution.Height.ToString();
            }
        }

        private void NewProjectFormClosing(object sender, EventArgs e)
        {
            window_refresh_timer.Stop();
        }

        private void middleNodesCountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                middle_nodes = Convert.ToUInt16(middleNodesCountTextBox.Text);
                middleNodesCountTextBox.Text = middle_nodes.ToString();
            }
            catch (Exception)
            {
                if (middleNodesCountTextBox.Text == "")
                {
                    middle_nodes = 0;
                    middleNodesCountTextBox.Text = "0";
                }
                else
                {
                    middleNodesCountTextBox.Text = middle_nodes.ToString();
                }
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                output_nodes++;
            }
            else
            {
                output_nodes--;
            }
            outputNodesCountLabel.Text = output_nodes.ToString();
        }

        private void toggleMouseButtonsButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["Left Mouse Button"] && code <= KeyCodes.Codes["X2 Mouse Button"] && code != KeyCodes.Codes["Control-Break Processing"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleArrowKeysButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["LEFT ARROW Key"] && code <= KeyCodes.Codes["DOWN ARROW Key"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleNumberKeysButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["0 Key"] && code <= KeyCodes.Codes["9 Key"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleFunctionKeysButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["F1 Key"] && code <= KeyCodes.Codes["F24 Key"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleLetterKeysButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["A Key"] && code <= KeyCodes.Codes["Z Key"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleKeypadKeysButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                int code = KeyCodes.Codes[cb.Text];
                if (code >= KeyCodes.Codes["Numeric Keypad 0 Key"] && code <= KeyCodes.Codes["Numeric Keypad Divide Key"])
                {
                    cb.Checked = !cb.Checked;
                }
            }
        }

        private void toggleAllButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                cb.Checked = !cb.Checked;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in key_binds)
            {
                cb.Checked = false;
            }
        }
    }
}
