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
        static JsonSerializer serializer;
        public string time;
        public string runtime;
        public string filesize;

        public void Update(ISubject subject)
        {
            serializer = new JsonSerializer();
            if (subject is Save save)
            {
                jsonString = JsonConvert.SerializeObject(save, Formatting.Indented);
            };
            /*Console.WriteLine("your save {0} as been done, the file {1} just got save here : {2} ", save.name, save.src, save.dest);*/


        }
        public void AddState()
        {
            using (var streamWriter = new StreamWriter("c:/projet/state.json"))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonString));
                    Console.WriteLine("addstate OK");

                }
            }

        }
    }
}