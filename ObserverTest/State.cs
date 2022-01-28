using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace ObserverTest
{
    class State : IObserver
    {
        static string jsonString;

        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                FileInfo FI = new FileInfo("C:/projet/State.json");

                if (FI.Exists)
                {
                    jsonString = ",\n\n";
                    jsonString = jsonString + JsonConvert.SerializeObject(save, Formatting.Indented);
                }
                else
                {
                    jsonString = JsonConvert.SerializeObject(save, Formatting.Indented);
                }
            };
        }
        public void AddState()
        {
            File.AppendAllText(@"C:/projet/State.json", jsonString);
        }
    }
}