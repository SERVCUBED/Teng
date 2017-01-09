using System;
using System.Diagnostics;
using System.Threading;

namespace TemplateEngine
{
    class ThreadManager
    {
        private readonly int _maxThreadCount;
        private int _currentThreadCount;
        private readonly int _waitInterval;

        /// <summary>
        /// Initialises a new instance of the SafeThreadManager class.
        /// </summary>
        /// <param name="waitInterval">The number of milliseconds to wait between each check 
        /// when the maximum number of threads has been exceeded. A low value is recommended.</param>
        /// <param name="maxThreadCount">The maximum number of threads.</param>
        public ThreadManager(int waitInterval = 10, int maxThreadCount = 50)
        {
            _waitInterval = waitInterval;
            _maxThreadCount = maxThreadCount;
        }

        /// <summary>
        /// Performs an action in a theadsafe environment.
        /// </summary>
        /// <param name="func">The function to perform.</param>
        /// <param name="threadPriority">The priority of the thread. Default is normal priority.</param>
        public void RunThread(Action func, ThreadPriority threadPriority = ThreadPriority.Normal)
        {
            var f = func;

            if (_maxThreadCount == 0)
            {
                TryRunAction(f);
                return;
            }

            // Wait for clear space
            while (_currentThreadCount >= _maxThreadCount)
                Thread.Sleep(_waitInterval);

            new Thread(() =>
            {
                _currentThreadCount++;

                TryRunAction(f);

                _currentThreadCount--;
            })
            { Priority = threadPriority }.Start();
        }

        private static void TryRunAction(Action func)
        {
            try
            {
                func();
            }
            catch (Exception)
            {
#if DEBUG
                if (!Debugger.IsAttached)
                    Debugger.Launch();
                throw;
#endif
            }
        }

        /// <summary>
        /// Pause the current thread while all other threads finish
        /// </summary>
        public void WaitForFinish()
        {
            while (_currentThreadCount > 0)
            {
                Thread.Sleep(_waitInterval);
            }
        }

        /// <summary>
        /// The number of currently running threads.
        /// </summary>
        public int RunningThreads
        {
            get { return _currentThreadCount; }
        }
    }
}
