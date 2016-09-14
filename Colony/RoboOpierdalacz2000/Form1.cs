using RoboOpierdalacz2000.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboOpierdalacz2000
{
    public partial class Form1 : Form
    {
        private byte _state = 0; //0-none 1-record 2-play
        private List<MacroEvent> _marks = new List<MacroEvent>();
        private int _marksCounter = 0;

        // private Hook _hook = new Hook();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_state == 1)
            {
                var m = new Mark();
                m.MousePosition = MousePosition;
                _marks.Add(m);
            }
            else if (_state == 2)
            {
                if (_marksCounter >= _marks.Count)
                    _marksCounter = 0;
                if (_marks.Count > 0)
                {
                    Cursor.Position = _marks[_marksCounter].MousePosition;
                    if (_marks[_marksCounter].KeyPressed != 0)
                    {
                        SendKeys.Send("ZXCVBNM");
                    }
                    _marksCounter++;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                _state = 2; //play
            }
            else
            {
                _state = 0; //none
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _state = 1; //record
            }
            else
            {
                _state = 0; //none
                label1.Text = _marks.Count.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //clear cache
            _marks.Clear();
            _marksCounter = 0;
            label1.Text = _marks.Count.ToString();
            timer1.Interval = Convert.ToInt32(textBox1.Text);
            timer1.Enabled = true;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Escape)
            {
                timer1.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener.UnHookKeyboard();
        }
        
    }
}
