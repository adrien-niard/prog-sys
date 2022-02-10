using System;
using System.Collections.Generic;
using System.Text;

namespace EasySavev2.Model
{
    interface IObserver
    {
        void Update(ISubject subject);
    }
}
