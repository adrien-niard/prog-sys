using System;
using System.Collections.Generic;

namespace ObserverTest
{
    class Program
    {
        static void Main()
        {
            //Instance creation of the singleton based class langue
            Langage langage = Langage.Instance;

            //Creation of a string data that keep the langage choice from the user
            string Userchoice = langage.EnglishOrFrancais();

            langage.PrintText(Userchoice, "Welcome on EasySave", "Bienvenu sur EasySave");

            bool loop = true;

            List<Save> SaveList = new List<Save>();

            while (loop == true)
            {
                //Creation of save and log objects
                Save save = new Save();
                Log log = new Log();
                save.Attach(log);

                //Asking the user how his save should be called
                langage.PrintText(Userchoice, "Select your save's name", "choisi le nom de votre sauvegarde");
                string name = Console.ReadLine();

                //Asking the user the save's type that he wants
                langage.PrintText(Userchoice, "Select your save's type Full or Differential", "choisissez le type de sauvegarde");
                string type = Console.ReadLine();

                //Asking the src path
                langage.PrintText(Userchoice, "Wich file do you want to copy", "Quel fichier voulez vous copier");
                string src = Console.ReadLine();

                //Asking the dest path
                langage.PrintText(Userchoice, "Where do you want to copy the file", "Ou voulez vous copier le fichier");
                string dest = Console.ReadLine();

                //Set the save attributes depending of the user choices
                save.name = name;
                save.src = src;
                save.dest = dest;
                save.type = type;

                //Add the save into the list
                SaveList.Add(save);

                langage.PrintText(Userchoice, "Would you want to create an other save ? (Yes | No)", "Voulez-vous créer une nouvelle sauvegarde ? (Oui | Non)");
                string Choice = Console.ReadLine();
                string ChoiceLow = Choice.ToLower();

                if (ChoiceLow == "oui" || ChoiceLow == "yes")
                {
                    //If the user want to add an other save => restart the loop
                    loop = true;
                }
                else if (ChoiceLow == "non" || ChoiceLow == "no")
                {
                    //If the user don't want to add an other save => stop the loop and exec
                    loop = false;

                    int x = 1;

                    //Print the list
                    foreach (Save sv in SaveList) {
                        Console.WriteLine(x + ". Name : " + sv.name);
                        x++;
                    }

                    langage.PrintText(Userchoice, "Which save do you want to execute ? (Example: 1)", "Quelle sauvegarde voulez-vous exécuter ? (Exemple: 1)");
                    string ExecChoice = Console.ReadLine();

                    Console.WriteLine(SaveList[Int32.Parse(ExecChoice) - 1].name);


                }
            }

            /*if (save.type == "Full" || save.type =="Complete")
            {
                save.RunSave(src, dest);
            }
            
            else if (save.type == "Diff")
            {
                save.RunSave(src, dest);
            }*/




        }
    }


 

    




}
