using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
    class Log : IObserver
    {
        public string time;
        public string runtime;
        public string filesize;
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                Console.WriteLine("your save {0} as been done, the file {1} just got save here : {2} ", save.name, save.src, save.dest);
            }
        }
    }
}
