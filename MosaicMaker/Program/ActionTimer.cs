using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace MosaicMakerNS
{
    public sealed class ActionTimer : IDisposable
    {
        #region Variables

        private readonly Stopwatch _stopwatch;
        private readonly int _minExecTime;

        #endregion

        #region Constructors

        public ActionTimer(float minSeconds)
        {
            _minExecTime = (int)(minSeconds * 1000);

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        #endregion

        public void Dispose()
        {
            _stopwatch.Stop();

            int diff = _minExecTime - _stopwatch.Elapsed.Milliseconds;

            if (diff > 0)
                Thread.Sleep(diff);
        }
    }

    public delegate void TimedAction(DoWorkEventArgs e);
}