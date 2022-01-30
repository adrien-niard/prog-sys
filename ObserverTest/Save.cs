﻿using System;
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

        //Definition of the name attribute
        public string name
        {
            get { return _name; }

            set
            {
                _name = value;
            }
        }
        private string _name;

        //Definition of the source attribute
        public string src
        {
            get { return _src; }

            set
            {
                _src = value;
            }
        }
        private string _src;

        //Definition of the destination attribute
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

        //Definition of the type attribute
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

        //Definition of the FileSize attribute
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

        //Definition of the Time attribute
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
                DirectoryInfo di = new DirectoryInfo(src);

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
