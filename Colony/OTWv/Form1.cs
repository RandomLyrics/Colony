using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTWv
{
    public partial class Form1 : Form
    {
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        public static Dictionary<string, Process> _processesDic { get; set; } = new Dictionary<string, Process>();

        public Form1()
        {
            InitializeComponent();
            hScrollBar1.Value = 255;
            Form asdasd = new Form();
           
        }

        private void GetWindowsNames()
        {
            _processesDic.Clear();
            comboBox1.Items.Clear();
            
            foreach (var proc in Process.GetProcesses())
            {
                var name = proc.MainWindowTitle;
                if (!String.IsNullOrEmpty(name))
                {
                    _processesDic[name] = proc;
                    comboBox1.Items.Add(name);
                }
            }
        }

        //load procs
        private void button1_Click(object sender, EventArgs e)
        {
            GetWindowsNames();
        }

        //stay top
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var item = _processesDic[comboBox1.SelectedItem.ToString()];
                var opacity = (byte)hScrollBar1.Value;
                var window = item.MainWindowHandle;
                //ON TOP
                SetWindowPos(window, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                if (checkBox1.Checked)
                {
                    //OPACITY
                    SetWindowLong(window, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) ^ WS_EX_LAYERED | 0x20);
                    SetLayeredWindowAttributes(window, 0, opacity, LWA_ALPHA);
                }
                else
                {
                    //OPACITY
                    SetWindowLong(window, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) ^ WS_EX_LAYERED);
                    SetLayeredWindowAttributes(window, 0, opacity, LWA_ALPHA);
                }
            }
            //Form asd = new Form();
            //asd.cli
           
        }

        //refresh
        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(@"http://www.cda.pl/video/89814229?wersja=720p");
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Refresh();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            GetWindowsNames();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            textBox1.Text = hScrollBar1.Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
