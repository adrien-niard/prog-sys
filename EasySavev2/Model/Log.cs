using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace EasySavev2.Model
{
    class Log : IObserver
    {
        private JObject JsonObj = new JObject();

        private string JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".json";
        private string XmlPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EasySave/" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + ".xml";

        //Function launch when Notify
        public void Update(ISubject subject)
        {
            if (subject is Save save)
            {
                JsonObj.RemoveAll();

                JsonObj.Add("Name", save.Name);
                JsonObj.Add("Type", save.Type);
                JsonObj.Add("SourceFilePath", save.Src);
                JsonObj.Add("DestinationFilePath", save.Dest);
                JsonObj.Add("Time", save.GetTime());
                JsonObj.Add("RunTime", save.RunTime);
                JsonObj.Add("CryptTime", save.CryptTime);
            }
        }

        public void AddLog()
        {
            //Json file creation
            try
            {
                FileInfo JsonFi = new FileInfo(JsonPath);
                List<JObject> saveJsonList = new List<JObject>();

                if (!JsonFi.Exists)
                {
                    //If the json file doesn't exist => just add json informations
                    saveJsonList.Add(JsonObj);
                }
                else
                {
                    //If the json file exist => deserialization of the existing file, then add to the informations the new save's informations
                    string JsonRead = File.ReadAllText(JsonPath);

                    saveJsonList = JsonConvert.DeserializeObject<List<JObject>>(JsonRead);

                    saveJsonList.Add(JsonObj);
                }

                //Serialization of all the datas
                string jsonStringLog = JsonConvert.SerializeObject(saveJsonList, Newtonsoft.Json.Formatting.Indented);

                //Write it into the json file
                File.WriteAllText(JsonPath, jsonStringLog);

                //Xml file creation
                try
                {
                    XmlDocument XmlObj = JsonConvert.DeserializeXmlNode(jsonStringLog);

                    FileInfo XmlFi = new FileInfo(XmlPath);
                    XmlSerializer xml = new XmlSerializer(JsonObj.GetType());
                    List<XmlDocument> saveXmlList = new List<XmlDocument>();

                    if (!XmlFi.Exists)
                    {
                        saveXmlList.Add(XmlObj);
                    }
                    else
                    {
                        string XmlRead = File.ReadAllText(XmlPath);

                        saveXmlList = JsonConvert.DeserializeObject<List<XmlDocument>>(XmlRead);

                        saveXmlList.Add(XmlObj);
                    }

                    string xmlStringLog = JsonConvert.SerializeObject(saveXmlList, Newtonsoft.Json.Formatting.Indented);

                    File.WriteAllText(XmlPath, xmlStringLog);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
