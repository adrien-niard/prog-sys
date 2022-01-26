using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace projet_save
{
   
    class Save
    {
        //Initate attributes
        public string name { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string type { get; set; }

        //initiate constructor
        public Save (string Name, string Source, string Destination, string Type)
        {
            name = Name;
            source = Source;
            destination = Destination;
            type = Type;
        }

        //initiate a full save method
        public void RunSave(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }

            catch(IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        //initiate a sequenial save method
        public void RunSequentialSave(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }

            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }

        }
        public void DeleteSave ()
        {

        }
        
        public void EditSave()
        {

        }

        public void ShowSave()
        {

        }
    }
}
