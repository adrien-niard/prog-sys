using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace EasySavev2.Model
{
    class Save : ISubject
    {
        private List<IObserver> _observers;

        //Definition of the name attribute
        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                Notify();
            }
        }
        private string _name;

        //Definition of the source attribute
        public string Src
        {
            get { return _src; }

            set
            {
                _src = value;
            }
        }
        private string _src;

        //Definition of the destination attribute
        public string Dest
        {
            get
            {
                return _dest;
            }

            set
            {
                _dest = value;
                Notify();
            }
        }
        private string _dest;

        //Definition of the type attribute
        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
                Notify();
            }
        }
        private string _type;

        //Definition of the FileSize attribute
        public long FileSize
        {
            get
            {
                return _fileSize;
            }
            set
            {
                _fileSize = value;
                Notify();
            }
        }
        private long _fileSize;

        //Definition of the Time attribute
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
                Notify();
            }
        }
        private string _time;

        public string CryptTime
        {
            get
            {
                return _cryptTime;
            }

            set
            {
                _cryptTime = value;
                Notify();
            }
        }
        private string _cryptTime;

        //Definition of the RunTime attribute
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

        //Constructor
        public Save()
        {
            _observers = new List<IObserver>();
        }

        //Link observer - listener
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        //Event
        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.Update(this);
            }
            );
        }

        //Get the size of a directory
        public long GetFileSize()
        {
            long size = 0;
            try
            {
                string SrcModif = Src.Substring(0, Src.LastIndexOf("/") + 1);
                DirectoryInfo di = new DirectoryInfo(SrcModif);

                size = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            return size;
        }
        public long TotalFiles()
        {
            string SrcModif = Src.Substring(0, Src.LastIndexOf("/") + 1);
            DirectoryInfo di = new DirectoryInfo(SrcModif);

            int TF = di.EnumerateFiles("*", SearchOption.AllDirectories).Count();

            return TF;
        }

        public long GetDestFilesSize()
        {
            long size = 0;
            try
            {
                string DestModif = Dest.Substring(0, Dest.LastIndexOf("/") + 1);
                DirectoryInfo di = new DirectoryInfo(DestModif);

                size = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            return size;
        }

        //Get the time of the creation of the save
        public string GetTime()
        {
            string time;
            time = DateTime.Now.ToString();

            return time;
        }

        //Start the timer to have the execution time
        public Stopwatch GetStartTimer()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            return stopWatch;
        }

        //Stop the timer to have the execution time
        public string GetStopTimer(Stopwatch stopWatch)
        {
            stopWatch.Stop();

            return stopWatch.Elapsed.ToString();
        }
    }
}
