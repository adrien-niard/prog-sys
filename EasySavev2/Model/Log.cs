using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace EasySavev2.Model
{
    class Log : IObserver
    {
        private JObject JsonObj = new JObject();
        private XElement XmlObj;
        private string XmlString = "";

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

                XmlObj = new XElement(save.Name, 
                            new XElement("Name", save.Name),
                            new XElement("Type", save.Type),
                            new XElement("SourceFilePath", save.Src),
                            new XElement("DestinationFilePath", save.Dest),
                            new XElement("Time", save.GetTime()),
                            new XElement("RunTime", save.RunTime),
                            new XElement("CryptTime", save.CryptTime)
                );
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

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            //Xml file creation
            try
            {
                FileInfo XmlFi = new FileInfo(XmlPath);
                XDocument XmlDoc;

                if (!XmlFi.Exists)
                {
                    XmlDoc = new XDocument();
                    XmlDoc.Add(new XElement("Log", XmlObj));
                }
                else
                {
                    XmlDoc = XDocument.Load(XmlPath);

                    XmlDoc.Root.LastNode.AddAfterSelf(XmlObj);
				}

                XmlString = XmlDoc.ToString();

                //Write it into the xml file
                File.WriteAllText(XmlPath, XmlString);
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
