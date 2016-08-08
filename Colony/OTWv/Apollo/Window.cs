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
        #region User32.dll
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
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        #endregion

        //END-DLL
        private IntPtr _window;
        public IntPtr Win { get { return _window; } }
        private IntPtr _handle;
        private Rect _rect;

        public bool IsMouseOver = false;

        public Window(IntPtr handle)
        {
            this._window = handle;
        }

        public Window(IntPtr mainWindowHandle, IntPtr handle)
        {
            this._window = mainWindowHandle;
            this._handle = handle;
        }

        public byte Opacity { get; private set; }
        public bool Clickthrough { get; set; }
        public bool OnTop { get; set; }
        public bool Autohide { get; set; }

        public void SetOpacity(byte val, bool implicty = false)
        {
            if (!implicty)
                Opacity = val;

            SetWindowLong(_window, GWL_EXSTYLE, GetWindowLong(_handle, GWL_EXSTYLE) ^ WS_EX_LAYERED);
            SetLayeredWindowAttributes(_window, 0, Opacity, LWA_ALPHA);
        }
        public void SetClickthrough(bool enabled)
        {
            if (enabled)
            {
                SetWindowLong(_window, GWL_EXSTYLE, GetWindowLong(_handle, GWL_EXSTYLE) ^ WS_EX_LAYERED | 0x20);
                SetLayeredWindowAttributes(_window, 0, Opacity, LWA_ALPHA);
            }

        }
        public void SetOnTop(bool enabled)
        {
            SetWindowPos(_window, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        public bool IsMouseHover()
        {
            GetWindowRect(_window, ref _rect);
            var pt = Control.MousePosition;
            if (isRectangelContainPoint(_rect, pt))
            {
                IsMouseOver = true;
                return true;
            }
            else
            {
                IsMouseOver = false;
                return false;
            }
        }
        public void OnTick()
        {
            Task.Delay(100);

        }


        public bool isRectangelContainPoint(Rect rec, Point pt)
        {
            if (pt.X >= rec.Left && pt.X <= rec.Right && pt.Y <= rec.Bottom && pt.Y >= rec.Top)
                return true;
            else
                return false;
        }
    }
}
