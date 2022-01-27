using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
    interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();

    }
}
