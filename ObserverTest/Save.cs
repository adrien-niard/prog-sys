using System;
using System.Collections.Generic;
using System.Text;
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
                Notify();
            }
        }

        private string _type;
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

        public void RunSave(string src, string dest)
        {
            try
            {
                /*ASave Exec = new ASave();*/
				/*File.Copy(src, dest, true);*/
			}

            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
    }
}
