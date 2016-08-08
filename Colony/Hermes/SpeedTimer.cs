using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    public class SpeedTimer: IDisposable
    {
        private Stopwatch _timer;
        public SpeedTimer()
        {
            _timer = new Stopwatch();
            _timer.Start();
        }

        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            _timer.Stop();
            Console.WriteLine("Elpased: {0} ticks", _timer.ElapsedTicks);
            Dispose(true);
        }
        #endregion

    }
}
