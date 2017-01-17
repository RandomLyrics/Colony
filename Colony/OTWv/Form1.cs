using OTWv.Apollo;
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
        private Window _window;
        public static Dictionary<string, Process> _processesDic { get; set; } = new Dictionary<string, Process>();
        public static Dictionary<Process, Window> _windowsDic { get; set; } = new Dictionary<Process, Window>();
        public Form1()
        {
            InitializeComponent();
            hScrollBar1.Value = 255;
            _cursor = new Cursor(Cursor.Current.Handle);
            timer1.Enabled = true;
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
                if (!_windowsDic.ContainsKey(item))
                {
                    _windowsDic[item] = new Window(item.MainWindowHandle, Handle);
                    _window = _windowsDic[item];
                }
                var window = _windowsDic[item];
                window.SetOnTop(true);
                window.SetOpacity((byte)hScrollBar1.Value);
                window.SetClickthrough(checkBox1.Checked);
            }

        }

        //refresh
        private void button3_Click(object sender, EventArgs e)
        {

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
            // Gaga((TextBox)sender);
        }

        private void Gaga(TextBox sender)
        {
            // var asd = sender;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                byte tmp;
                Byte.TryParse(textBox1.Text, out tmp);
                hScrollBar1.Value = tmp;
            }
        }


        public Cursor _cursor { get; set; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var winkp in _windowsDic)
            {
                var win = winkp.Value;
                if (win.IsMouseHover())
                {
                    win.IsMouseHovering = true;
                    if (!win.IsHidden)
                    {
                        win.SetWindowConfig(30, true);
                        win.IsHidden = true;
                    }
                }
                else
                {
                    win.IsMouseHovering = false;
                    if (win.IsHidden && !win.IsMouseHovering)
                    {
                        win.SetWindowConfig(win.Opacity, win.Clickthrough);
                        win.IsHidden = false;
                    }
                }
            }
        }
        private void Form1_Resize_1(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(500, "OTW", "topp", ToolTipIcon.Info);
                this.ShowInTaskbar = false;
            }

            //else if (FormWindowState.Normal == this.WindowState)
            //{
            //    notifyIcon1.Visible = false;
            //}
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                _window.SetThickBorder(true);
            else
                _window.SetThickBorder(false);
        }
    }
}
