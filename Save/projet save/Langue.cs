using System;
using System.Collections.Generic;
using System.Text;

namespace projet_save
    //Setup a singleton patern to manage the english or french langage
{
    public class Langue
    {
        private static Langue instance = new Langue();
        private Langue() { }
        public static Langue Instance
        {
            get { return instance; }
        }

        //creation of a function that return the langage used by the user
        public string EnglishOrFrancais()
        {
            Console.WriteLine("l'application devrait être en français ou en anglais ?");
            Console.WriteLine("Should the application be in french or in English ?");
            Console.WriteLine("Type English or Francais / Ecrivez English ou Francais") ;
            string langue = Console.ReadLine();
            return langue;
        }

        //creation of a function we will use each time we want to print text
        public void PrintText(String langue, string englishText, string frenchText)
        {
            if (langue == "English")
            {
                Console.WriteLine(englishText);
            }

            else if (langue == "Francais")
            {
                Console.WriteLine(frenchText);
            }
        }

    }

   
}
