using OTWv.Apollo.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTWv.Apollo
{
    public class Window
    {
        public byte Opacity
        {
            get { return Opacity; }
            set { SetOpacity(value, true); }
        }



        //END-DLL
        private const int AUTO_BORDER_HEIGHT = 40;
        private const int AUTO_OPACITY = 30;
        private IntPtr _window;
        public IntPtr Win { get { return _window; } }
        private IntPtr _handle;
        private Rect _rect;

        public bool IsHidden = false;
        public bool IsMouseHovering = false;

        public Window(IntPtr handle)
        {
            this._window = handle;
        }

        public Window(IntPtr mainWindowHandle, IntPtr handle)
        {
            this._window = mainWindowHandle;
            this._handle = handle;
        }


        public bool Clickthrough { get; set; }
        public bool OnTop { get; set; }
        public bool Autohide { get; set; }

        public void SetThickBorder(bool enabled)
        {
            if (enabled)
            {
                var style = User32.GetWindowLong(_window, User32.GWL_EXSTYLE);
                style = (uint)~(WindowStyles.WS_CAPTION | WindowStyles.WS_THICKFRAME | WindowStyles.WS_MINIMIZE | WindowStyles.WS_MAXIMIZE | WindowStyles.WS_SYSMENU);
                User32.SetWindowLong(_window, (int)WindowLongFlags.GWL_STYLE, style);
            }
        }


        public void SetWindowConfig(byte oval, bool clickthrough)
        {
            if (clickthrough)
                User32.SetWindowLong(_window, User32.GWL_EXSTYLE, User32.GetWindowLong(_handle, User32.GWL_EXSTYLE) ^ User32.WS_EX_LAYERED | 0x20);
            else
                User32.SetWindowLong(_window, User32.GWL_EXSTYLE, User32.GetWindowLong(_handle, User32.GWL_EXSTYLE) ^ User32.WS_EX_LAYERED);
            User32.SetLayeredWindowAttributes(_window, 0, oval, User32.LWA_ALPHA);
        }
        public void SetOpacity(byte val, bool implicty = false)
        {
            if (!implicty)
                Opacity = val;

            User32.SetWindowLong(_window, User32.GWL_EXSTYLE, User32.GetWindowLong(_handle, User32.GWL_EXSTYLE) ^ User32.WS_EX_LAYERED);
        }
        public void SetClickthrough(bool enabled, bool implicty = false)
        {
            if (enabled)
            {
                if (!implicty)
                    Clickthrough = enabled;
                User32.SetWindowLong(_window, User32.GWL_EXSTYLE, User32.GetWindowLong(_handle, User32.GWL_EXSTYLE) ^ User32.WS_EX_LAYERED | 0x20);
                User32.SetLayeredWindowAttributes(_window, 0, Opacity, User32.LWA_ALPHA);
            }

        }
        public void SetOnTop(bool enabled)
        {
            User32.SetWindowPos(_window, User32.HWND_TOPMOST, 0, 0, 0, 0, User32.TOPMOST_FLAGS);
        }
        public bool IsMouseHover()
        {
            User32.GetWindowRect(_window, ref _rect);
            var pt = Control.MousePosition;
            if (isRectangelContainPoint(_rect, pt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public Task OnTick()
        //{

        //}


        //private Task _tickTask;
        //public void RunTickClock()
        //{
        //    Task.Run(OnTick);
        //}
        public bool isRectangelContainPoint(Rect rec, Point pt)
        {
            if (pt.X >= rec.Left && pt.X <= rec.Right && pt.Y <= rec.Bottom && pt.Y >= rec.Top + AUTO_BORDER_HEIGHT)
                return true;
            else
                return false;
        }
    }
}
