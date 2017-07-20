using System;
using System.Diagnostics;
using System.Threading;

namespace MosaicMaker
{
    public class ActionTimer : IDisposable
    {
        #region Variables

        private Stopwatch _stopwatch;
        private int _waitTime;

        #endregion

        #region Constructors

        public ActionTimer(float waitSeconds)
        {
            _waitTime = (int)(waitSeconds * 1000);

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        #endregion

        public void Dispose()
        {
            _stopwatch.Stop();

            int diff = _waitTime - _stopwatch.Elapsed.Milliseconds;
            if (diff > 0)
                Thread.Sleep(diff);
        }
    }
}