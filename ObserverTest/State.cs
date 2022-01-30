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

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                string SaveName = save.name;
                string Time = save.Time;



                /*FileInfo FI = new FileInfo("C:/projet/State.json");

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
                }*/
            }
        }

        //Append a save to the json file
        public void AddState(int NbObj)
        {
            if (NbObj != 0)
            {
                string State = "In progress";

            }
            else
            {
                string State = "Complete";
            }
            /*File.AppendAllText(@"C:/projet/State.json", jsonString);*/
        }
    }
}