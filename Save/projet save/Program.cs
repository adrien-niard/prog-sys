using System;

namespace projet_save
{
    class Program
    {
        static void Main()
        {
            //Instance creation of the singleton based class langue
            Langue langage = Langue.Instance;

            //Creation of a string data that keep the langage choice from the user
            string  ChoixUtilisateur = langage.EnglishOrFrancais();

            //PrintText function print text either the user choice is english or french
            langage.PrintText(ChoixUtilisateur, "your application is now in english", "Votre application est à présent en français");

            //asking the user how his save should be called
            langage.PrintText(ChoixUtilisateur, "Select your save's name", "choisi le nom de votre sauvegarde");
            string nom = Console.ReadLine();

            //asking the user the save's type that he wants
            langage.PrintText(ChoixUtilisateur, "Select your save's type Full or Differential", "choisissez le type de sauvegarde");
            string type = Console.ReadLine();


            string src = Console.ReadLine();
            string dest = "D:/projet/wow.txt";

            //instantiation of sauvegarde
            Save Sauvegarde = new Save(nom,src,dest,type);

            //based on user choice run eaither full save or differential save
            if (Sauvegarde.type =="Full")
            {
                Sauvegarde.RunSave(src, dest);
            }

            else if (Sauvegarde.type == "Differential")
            {
                Sauvegarde.RunSequentialSave(src, dest);
            }


            

            langage.PrintText(ChoixUtilisateur, "Your Save as been done", "Votre sauvegarde a été effectué");
            Console.ReadLine();

        }
    }
}
