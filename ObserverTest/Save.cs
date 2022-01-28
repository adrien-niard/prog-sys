using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
//Add the System.IO fore the File option
using System.IO;

namespace ObserverTest
{
    class Save : ISubject
    {
        private List<IObserver> _observers;

        public string name
        {
            get { return _name; }

            set
            {
                _name = value;
            }
        }
        private string _name;
        public string src
        {
            get { return _src; }

            set
            {
                _src = value;
            }
        }
        private string _src;

        public string dest
        {
            get
            {
                return _dest;
            }

            set
            {
                _dest = value;
            }
        }
        private string _dest;
        public string type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        private string _type;

        public long FileSize {
            get
            {
                return _fileSize;
            }
            set
            {
                _fileSize = value;
            }
        }

        private long _fileSize;
        public string Time {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

        private string _time;

        public string RunTime 
        {
            get
            {
                return _runtime;
            }

            set
            {
                _runtime = value;
                Notify();
            }
        }

        private string _runtime;

        public Save()
        {
            _observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.Update(this);
            }
            );
        }
        public long GetFileSize()
        {
            DirectoryInfo di = new DirectoryInfo(src);

            long size = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            return size;
        }

        public string GetTime()
        {
            string time;
            time = DateTime.Now.ToString();

            return time;
        }

        public Stopwatch GetStartTimer()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            return stopWatch;
        }
        public string GetStopTimer(Stopwatch stopWatch)
        {
            stopWatch.Stop();

            return stopWatch.Elapsed.ToString();
        }
    }
}
