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

            langage.PrintText(Userchoice, "Welcome on EasySave", "Bienvenue sur EasySave");

            //Loop to create save object(s)
            bool loop = true;
            List<Save> SaveList = new List<Save>();

            while (loop == true)
            {
                //Creation of save and log objects and setup log as a listener
                Save save = new Save();
                Log log = new Log();
                /*STATE*/
                save.Attach(log);

                //Asking the user how his save should be called
                langage.PrintText(Userchoice, "Select your save's name :", "Choisissez le nom de votre sauvegarde :");
                string name = Console.ReadLine();

                //Asking the user the save's type that he wants
                langage.PrintText(Userchoice, "Select your save's type : (Full | Diff)", "Choisissez le type de sauvegarde : (Full | Diff)");
                string type = Console.ReadLine();

                //Asking the source path
                langage.PrintText(Userchoice, "Which file do you want to copy ?", "Quel fichier voulez-vous copier ?");
                string src = Console.ReadLine();

                //Asking the destination path
                langage.PrintText(Userchoice, "Where do you want to copy the file ?", "Où voulez-vous copier le fichier");
                string dest = Console.ReadLine();

                //Set the save attributes depending of the user's choices
                save.name = name;
                save.src = src;
                save.dest = dest;
                save.type = type;

                //Add the save into the list
                SaveList.Add(save);
                log.AddLog();

                Console.WriteLine("--------------------");

                //Asking the user if he want to add an other save or proceed to the execution
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
                    //If the user don't want to add an other save => stop the loop and start execution
                    loop = false;
                    
                    //Select the execution of one save (Mono) or all the saves (Sequential)
                    langage.PrintText(Userchoice, "Do you want to execute one or all saves ? (One | All)", "Voulez-vous exécuter une ou toutes les sauvegardes ? (Une | Toutes)");
                    string ExecModeChoice = Console.ReadLine();
                    string ExecModeChoiceLow = ExecModeChoice.ToLower();

                    //Execution of one save 
                    if (ExecModeChoiceLow == "one" || ExecModeChoiceLow == "une")
                    {
                        int x = 1;
                        //Print the list
                        foreach (Save sv in SaveList)
                        {
                            Console.WriteLine(x + ". Name : " + sv.name);
                            x++;
                        }

                        //The user choose which save he want to execute
                        langage.PrintText(Userchoice, "Which save do you want to execute ? (Example: 1)", "Quelle sauvegarde voulez-vous exécuter ? (Exemple: 1)");
                        string ExecChoice = Console.ReadLine();
                        Save SaveObject = SaveList[Int32.Parse(ExecChoice) - 1];

                        //Add the list to call the execution
                        List<Save> ListMono = new List<Save>();
                        ListMono.Add(SaveObject);
                        ASave MonoExec = new MonoExec();

                        //Call the full execution if the object type match
                        if (SaveObject.type == "Full")
                        {
                            MonoExec.ExecFull(ListMono);
                        }
                        //Call the differential execution if the object type match
                        else if (SaveObject.type == "Diff")
                        {
                            MonoExec.ExecDiff(ListMono);
                        }
                    }
                    //Execution of all saves
                    else if (ExecModeChoiceLow == "all" || ExecModeChoiceLow == "toutes" || ExecModeChoiceLow == "toute")
                    {
                        ASave SeqExec = new SequentialExec();
                    
                        //Browse the list to execute individualy objects
                        foreach (Save sv in SaveList)
                        {
                            //Call the full execution if the object type match
                            if (sv.type == "Full")
                            {
                                SeqExec.ExecFull(SaveList);
                            }
                            //Call the differential execution if the object type match
                            else if (sv.type == "Diff")
                            {
                                SeqExec.ExecDiff(SaveList);
                            }
                        }
                    }
                }
            }
        }
    }
}
