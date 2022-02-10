using System;
using System.Collections.Generic;
using System.Text;

namespace EasySavev2.Model
{
    interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }
}
