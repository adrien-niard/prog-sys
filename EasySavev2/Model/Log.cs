using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;

namespace EasySavev2.Model
{
    class Log : IObserver
    {
        JObject JsonObj = new JObject();

        string JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".json";
        string XmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".xml";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                JsonObj.RemoveAll();

                JsonObj.Add("Name", save.name);
                JsonObj.Add("Type", save.type);
                JsonObj.Add("SourceFilePath", save.src);
                JsonObj.Add("DestinationFilePath", save.dest);
                JsonObj.Add("Time", save.GetTime());
                JsonObj.Add("RunTime", save.RunTime);
                JsonObj.Add("CryptTime", save.CryptTime);
            }
        }

        //Append a save to the json file
        public void AddLog()
        {
            try
            {
                FileInfo Fi = new FileInfo(JsonPath);
                XmlDocument xml = new XmlDocument();
                List<JObject> saveJsonList = new List<JObject>();

                if (!Fi.Exists)
                {
                    saveJsonList.Add(JsonObj);
                }
                else
                {
                    string JsonRead = File.ReadAllText(JsonPath);

                    saveJsonList = JsonConvert.DeserializeObject<List<JObject>>(JsonRead);

                    saveJsonList.Add(JsonObj);
                }

                string jsonStringLog = JsonConvert.SerializeObject(saveJsonList, Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(JsonPath, jsonStringLog);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
