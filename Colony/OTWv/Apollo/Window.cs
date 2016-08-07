using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTWv.Apollo
{
    public class Window
    {
        private IntPtr _handle;

        public Window(IntPtr handle)
        {
            this._handle = handle;
        }

        public byte Opacity { get; set; }
        public bool Clickthrough { get; set; }
        public bool OnTop { get; set; }
    }
}
