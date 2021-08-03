using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SelectWindowForm : Form
    {
        public IntPtr selected_window_handle;
        Timer timer;
        List<WindowButton> windowButtons;
        const int button_width = 150;
        const int button_height = 150;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int CloseWindow(IntPtr hWnd);

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

        private static IEnumerable<IntPtr> FindWindows(EnumWindowsProc filter)
        {
            List<IntPtr> windows = new List<IntPtr>();

            EnumDesktopWindows(IntPtr.Zero, delegate (IntPtr wnd, IntPtr param)
            {
                if (filter(wnd, param))
                {
                    windows.Add(wnd);
                }
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        public SelectWindowForm()
        {
            InitializeComponent();
        }

        private void SelectWindowForm_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Interval = 5000;
            timer.Start();

            windowButtons = new List<WindowButton>();
            IEnumerable<IntPtr> windows = FindWindows(delegate (IntPtr wnd, IntPtr param)
            {
                return IsWindowVisible(wnd) && !string.IsNullOrEmpty(GetWindowText(wnd));
            });

            foreach (IntPtr wnd in windows)
            {
                WindowButton wndbtn = new WindowButton();
                wndbtn.window_handle = wnd;
                Button btn = new Button();
                Bitmap img = GetWindowCaptureAsBitmap(wnd);
                btn.Image = new Bitmap(img, button_width, (int)(img.Height * ((float)button_width / img.Width)));
                string title = GetWindowText(wnd);
                int lastDash = title.LastIndexOf(" - ");
                btn.Text = title.Substring(lastDash > 0 ? lastDash + 2 : 0);
                btn.TextImageRelation = TextImageRelation.TextAboveImage;
                btn.Size = new Size(button_width, button_height);
                btn.Click += new System.EventHandler(SelectWindow);
                Controls.Add(btn);
                wndbtn.button = btn;
                windowButtons.Add(wndbtn);
            }
            RelocateButtons();
        }

        private void UpdateWindowButtons()
        {
            foreach (WindowButton wnd in windowButtons)
            {
                using (Bitmap img = GetWindowCaptureAsBitmap(wnd.window_handle))
                {
                    if (img == null)
                    {
                        Controls.Remove(wnd.button);
                        windowButtons.Remove(wnd);
                        RelocateButtons();
                        return;
                    }
                    wnd.button.Image = new Bitmap(img, button_width, (int)(img.Height * ((float)button_width / img.Width)));
                }
            }
        }

        private void RelocateButtons()
        {
            int padding = 10;
            int x = -button_width;
            int y = padding;
            foreach (WindowButton wnd in windowButtons)
            {
                if ((x += button_width + padding) + button_width > Width)
                {
                    x = padding;
                    y += padding + button_height;
                }
                wnd.button.Location = new Point(x, y);
            }
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            UpdateWindowButtons();
        }

        private static Bitmap GetWindowCaptureAsBitmap(IntPtr handle)
        {
            Rectangle rc = new Rectangle();
            if (!GetWindowRect(handle, ref rc))
            {
                return null;
            }

            Bitmap bitmap = new Bitmap(rc.Width - rc.X, rc.Height - rc.Y);
            Graphics gfxBitmap = Graphics.FromImage(bitmap);
            IntPtr hdcBitmap = gfxBitmap.GetHdc();
            IntPtr hdcWindow = GetWindowDC(handle);
            BitBlt(hdcBitmap, 0, 0, rc.Width - rc.X, rc.Height - rc.Y, hdcWindow, 0, 0, 0x00CC0020);    //SRCCOPY = 0x00CC0020

            gfxBitmap.ReleaseHdc(hdcBitmap);
            ReleaseDC(handle, hdcWindow);
            gfxBitmap.Dispose();

            return bitmap;
        }

        private void SelectWindow(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            foreach (WindowButton wnd in windowButtons)
            {
                if (wnd.button == clicked && GetWindowCaptureAsBitmap(wnd.window_handle) != null)
                {
                    selected_window_handle = wnd.window_handle;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void SelectWindowFormClosing(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
