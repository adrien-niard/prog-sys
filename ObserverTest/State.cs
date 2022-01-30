using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                FileInfo Fi = new FileInfo("C:/projet/State.json");

                JObject json = new JObject();

                json.Add("Name", save.name);
                json.Add("Time", save.Time);
                /*json.Add("State", "N/A");
                json.Add("TotalFilesToCopy", "N/A");
                json.Add("TotalFilesSize", "N/A");
                json.Add("NbFilesLeftToDo", "N/A");
                json.Add("TotalFilesSizeLeftToDo", "N/A");*/
                json.Add("SourceFilePath", save.src);
                json.Add("DestinationFilePath", save.dest);

                if (Fi.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonString = ",\n\n";
                    jsonString = jsonString + JsonConvert.SerializeObject(json, Formatting.Indented);
                }
                else
                {
                    jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
                }
            }
        }

        //Append a save to the json file
        public void AddState(int NbObj)
        {
            if (NbObj != 0)
            {
                Console.WriteLine(jsonString);
                var desJSON = JsonConvert.DeserializeObject(jsonString);
                Console.WriteLine(desJSON);
                /*string State = "ACTIVE";
                string TotalFilesToCopy = "";
                string TotalFilesSize = "";
                string NbFilesLeftToDo = "";
                string TotalFilesSizeLeftToDo = "";

                desJSON["State"] = State;
                desJSON["TotalFilesToCopy"] = TotalFilesToCopy;
                desJSON["TotalFilesSize"] = TotalFilesSize;
                desJSON["NbFilesLeftToDo"] = NbFilesLeftToDo;
                desJSON["TotalFilesSizeLeftToDo"] = TotalFilesSizeLeftToDo;*/

                Console.WriteLine(desJSON);
            }
            else
            {
                string State = "END";
            }

            File.AppendAllText(@"C:/projet/State.json", jsonString);
        }
    }
}