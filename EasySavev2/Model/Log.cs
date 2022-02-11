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
        static string jsonString;
        static string jsonStringLog;
        string JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".json";
        string XmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".xml";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                FileInfo FI = new FileInfo(JsonPath);
                JObject json = new JObject();

                json.Add("Name", save.name);
                json.Add("Type", save.type);
                json.Add("SourceFilePath", save.src);
                json.Add("DestinationFilePath", save.dest);
                json.Add("Time", save.GetTime());
                json.Add("RunTime", save.RunTime);
                json.Add("CryptTime", save.CryptTime);

                jsonString = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            }
        }

        //Append a save to the json file
        public void AddLog()
        {
            try
            {
                FileInfo Fi = new FileInfo(JsonPath);
                XmlDocument xml = new XmlDocument();

                if (Fi.Exists)
                {
                    //Store save's attribute in jsonString variable
                    jsonStringLog = ",\n\n";
                    jsonStringLog += JsonConvert.SerializeObject(jsonString, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    jsonStringLog += JsonConvert.SerializeObject(jsonString, Newtonsoft.Json.Formatting.Indented);
                }

                File.AppendAllText(JsonPath, jsonString);

                xml = JsonConvert.DeserializeXmlNode(jsonStringLog);
                /*File.AppendAllText(XmlPath, xml.ToString());*/
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
