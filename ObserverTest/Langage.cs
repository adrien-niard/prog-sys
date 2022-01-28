using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
    //Setup a singleton pattern to define the langage of the application
    class Langage
    {
        private static Langage instance = new Langage();
        private Langage() { }
        public static Langage Instance
        {
            get { return instance; }
        }

        //Creation of a function that return the langage used by the user
        public string EnglishOrFrancais()
        {
            Console.WriteLine("L'application devrait être en français ou en anglais ?");
            Console.WriteLine("Should the application be in french or in English ?");

            bool loop = true;
            string LangueLow = "";

            //While loop to choose language and protect from typing mistake
            while (loop == true)
            {
                Console.WriteLine("Type English or Francais / Ecrivez English ou Francais");
                string langue = Console.ReadLine();
                LangueLow = langue.ToLower(); 

                if (LangueLow == "francais" || LangueLow == "english")
                {
                    loop = false;
                }
            }

            Console.WriteLine("--------------------");

            return LangueLow;
        }

        //Creation of a function we will use each time we want to print text either in english or french
        public void PrintText(String langue, string englishText, string frenchText)
        {
            if (langue == "english")
            {
                Console.WriteLine(englishText);
            }

            else if (langue == "francais")
            {
                Console.WriteLine(frenchText);
            }
        }
    }
}
