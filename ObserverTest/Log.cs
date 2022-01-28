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

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                FileInfo FI = new FileInfo("C:/projet/log.json");

                //Test if the json file exist
                if (FI.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonString = ",\n\n";
                    jsonString = jsonString + JsonConvert.SerializeObject(save, Formatting.Indented);
                }
                else
                {
                    jsonString = JsonConvert.SerializeObject(save, Formatting.Indented);
                }
            };
        }

        //Append a save to the json file
        public void AddLog()
        {
            File.AppendAllText(@"C:/projet/log.json", jsonString);
        }
    }
}