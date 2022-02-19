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
        JObject StateObj = new JObject();

        string StatePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/state.json";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                StateObj.RemoveAll();

                StateObj.Add("Name", save.name);
                StateObj.Add("Time", save.Time);
                StateObj.Add("State", "N/A");
                StateObj.Add("TotalFilesToCopy", "N/A"); //save.TotalFiles()
                StateObj.Add("TotalFilesSize", "N/A"); //save.GetFileSize()
                StateObj.Add("NbFilesLeftToDo", "N/A");
                StateObj.Add("TotalFilesSizeLeftToDo", "N/A");
                StateObj.Add("SourceFilePath", save.src);
                StateObj.Add("DestinationFilePath", save.dest);

                /*jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);*/
            }
        }

        //Append a save to the json file
        public void AddState(int NbObj)
        {
            try
            {
                FileInfo Fi = new FileInfo(StatePath);
                List<JObject> stateJsonList = new List<JObject>();
                /*var desJSON = JsonConvert.DeserializeObject<JObject>(JsonObj);*/

                if (NbObj > 0)
                {
                    string State = "ACTIVE";
                    string TotalFilesToCopy = "N/A";
                    string TotalFilesSize = "N/A";
                    string NbFilesLeftToDo = "N/A";
                    string TotalFilesSizeLeftToDo = "N/A";

                    StateObj["State"] = State;
                    StateObj["TotalFilesToCopy"] = TotalFilesToCopy;
                    StateObj["TotalFilesSize"] = TotalFilesSize;
                    StateObj["NbFilesLeftToDo"] = NbFilesLeftToDo;
                    StateObj["TotalFilesSizeLeftToDo"] = TotalFilesSizeLeftToDo;
                }
                else
                {
                    string State = "END";

                    StateObj["State"] = State;
                }

                if (!Fi.Exists)
                {
                    stateJsonList.Add(StateObj);
                }
                else
                {
                    string JsonRead = File.ReadAllText(StatePath);

                    stateJsonList = JsonConvert.DeserializeObject<List<JObject>>(JsonRead);

                    stateJsonList.Add(StateObj);
                }

                /*if (Fi.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonStringState = ",\n";
                    jsonStringState += JsonConvert.SerializeObject(desJSON, Formatting.Indented);
                }
                else
                {
                    jsonStringState += JsonConvert.SerializeObject(desJSON, Formatting.Indented);
                }*/

                /*File.AppendAllText(Path, jsonStringState);*/

                string stateStringLog = JsonConvert.SerializeObject(stateJsonList, Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(StatePath, stateStringLog);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
