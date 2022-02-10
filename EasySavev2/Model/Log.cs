using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace EasySavev2.Model
{
    class Log : IObserver
    {
        static string jsonString;
        static string jsonStringLog;
        string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".json";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                FileInfo FI = new FileInfo(Path);
                JObject json = new JObject();

                json.Add("Name", save.name);
                json.Add("Type", save.type);
                json.Add("SourceFilePath", save.src);
                json.Add("DestinationFilePath", save.dest);
                json.Add("Time", save.GetTime());
                json.Add("RunTime", save.RunTime);

                jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
            }
        }

        //Append a save to the json file
        public void AddLog()
        {
            try
            {
                FileInfo Fi = new FileInfo(Path);

                if (Fi.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonStringLog = ",\n\n";
                    jsonStringLog += JsonConvert.SerializeObject(jsonString, Formatting.Indented);
                }
                else
                {
                    jsonStringLog += JsonConvert.SerializeObject(jsonString, Formatting.Indented);
                }

                File.AppendAllText(Path, jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
