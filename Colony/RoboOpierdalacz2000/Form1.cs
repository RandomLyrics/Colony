using MouseKeyboardLibrary;
using Newtonsoft.Json;
using RoboOpierdalacz2000.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace RoboOpierdalacz2000
{
    public partial class Form1 : Form
    {
        MacroPack _movies = new MacroPack();
        List<MacroEvent> _cevents = new List<MacroEvent>();
        long lastTimeRecorded = 0;

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();


        public Form1()
        {
            InitializeComponent();

            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);

        }

        //macro events
        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            _cevents.Add(
                new MacroEvent(
                    MacroEventType.MouseMove,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;
        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {

            _cevents.Add(
                new MacroEvent(
                    MacroEventType.MouseDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            _cevents.Add(
                new MacroEvent(
                    MacroEventType.MouseUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {

            _cevents.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            _cevents.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }
        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Controls.OfType<Button>().ToList().ForEach(x => x.Enabled = false);
                checkBox1.Enabled = false;
                comboBox1.Enabled = false;

                _cevents.Clear();
                lastTimeRecorded = Environment.TickCount;

                keyboardHook.Start();
                mouseHook.Start();
            }
            else
            {
                keyboardHook.Stop();
                mouseHook.Stop();

                var tmp = new MacroMovie(comboBox1.Text);
                tmp.Records = _cevents;
                _movies.Movies.Add(tmp);
                
                comboBox1.DataSource = _movies;

                Controls.OfType<Button>().ToList().ForEach(x => x.Enabled = true);
                checkBox1.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.DataSource = _movies.Movies;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = _movies;
        }

        //FILE
        private void button2_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = FD.FileName;
                var filebody = File.ReadAllText(path);
                var open = JsonConvert.DeserializeObject<MacroPack>(filebody);
                comboBox1.DataSource = open;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.SaveFileDialog();
            FD.DefaultExt = ".mpack";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = FD.FileName;
                var filebody = JsonConvert.SerializeObject(_movies);
                File.WriteAllText(path, filebody);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _cevents = _movies.Movies[comboBox1.SelectedIndex].Records;
                foreach (MacroEvent macroEvent in _cevents)
                {

                    Thread.Sleep((int)macroEvent.TimeSinceLastEvent);

                    switch (macroEvent.MacroEventType)
                    {
                        case MacroEventType.MouseMove:
                            {
                                MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;
                                MouseSimulator.X = mouseArgs.X;
                                MouseSimulator.Y = mouseArgs.Y;
                            }
                            break;
                        case MacroEventType.MouseDown:
                            {
                                MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;
                                MouseSimulator.MouseDown(mouseArgs.Button);
                            }
                            break;
                        case MacroEventType.MouseUp:
                            {
                                MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;
                                MouseSimulator.MouseUp(mouseArgs.Button);
                            }
                            break;
                        case MacroEventType.KeyDown:
                            {
                                KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                                KeyboardSimulator.KeyDown(keyArgs.KeyCode);
                            }
                            break;
                        case MacroEventType.KeyUp:
                            {
                                KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                                KeyboardSimulator.KeyUp(keyArgs.KeyCode);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }
}
