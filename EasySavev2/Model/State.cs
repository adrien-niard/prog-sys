using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasySavev2.Model
{
    class State : IObserver
    {
        static string jsonString;
        static string jsonStringState;
        string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/state.json";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                JObject json = new JObject();

                json.Add("Name", save.name);
                json.Add("Time", save.Time);
                json.Add("State", "N/A");
                json.Add("TotalFilesToCopy", "N/A"); //save.TotalFiles()
                json.Add("TotalFilesSize", "N/A"); //save.GetFileSize()
                json.Add("NbFilesLeftToDo", "N/A");
                json.Add("TotalFilesSizeLeftToDo", "N/A");
                json.Add("SourceFilePath", save.src);
                json.Add("DestinationFilePath", save.dest);

                jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
            }
        }

        //Append a save to the json file
        public void AddState(int NbObj)
        {
            try
            {
                FileInfo Fi = new FileInfo(Path);

                var desJSON = JsonConvert.DeserializeObject<JObject>(jsonString);

                if (NbObj > 0)
                {
                    string State = "ACTIVE";
                    string TotalFilesToCopy = "N/A";
                    string TotalFilesSize = "N/A";
                    string NbFilesLeftToDo = "N/A";
                    string TotalFilesSizeLeftToDo = "N/A";

                    desJSON["State"] = State;
                    desJSON["TotalFilesToCopy"] = TotalFilesToCopy;
                    desJSON["TotalFilesSize"] = TotalFilesSize;
                    desJSON["NbFilesLeftToDo"] = NbFilesLeftToDo;
                    desJSON["TotalFilesSizeLeftToDo"] = TotalFilesSizeLeftToDo;
                }
                else
                {
                    string State = "END";

                    desJSON["State"] = State;
                }

                if (Fi.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonStringState = ",\n\n";
                    jsonStringState += JsonConvert.SerializeObject(desJSON, Formatting.Indented);
                }
                else
                {
                    jsonStringState += JsonConvert.SerializeObject(desJSON, Formatting.Indented);
                }

                File.AppendAllText(Path, jsonStringState);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
