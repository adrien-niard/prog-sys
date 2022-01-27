using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace ObserverTest
{
    class Log : IObserver
    {
        static string jsonString;
        static JsonSerializer serializer;

        public void Update(ISubject subject)
        {
            serializer = new JsonSerializer();
            if (subject is Save save)
            {
                FileInfo FI = new FileInfo("C:/projet/log.json");

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
        public void AddLog()
        {
            File.AppendAllText(@"C:/projet/log.json", jsonString);
        }
    }
}