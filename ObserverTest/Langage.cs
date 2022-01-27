﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
    class Langage
    {
        private static Langage instance = new Langage();
        private Langage() { }
        public static Langage Instance
        {
            get { return instance; }
        }

        //creation of a function that return the langage used by the user
        public string EnglishOrFrancais()
        {
            Console.WriteLine("L'application devrait être en français ou en anglais ?");
            Console.WriteLine("Should the application be in french or in English ?");
            bool lang = true;
            string LangueLow = "";

            while (lang == true)
            {
                Console.WriteLine("Type English or Francais / Ecrivez English ou Francais");
                string langue = Console.ReadLine();
                LangueLow = langue.ToLower(); 

                if (LangueLow == "francais" || LangueLow == "english")
                {
                    lang = false;

                }
            }
            return LangueLow;
        }

        //creation of a function we will use each time we want to print text
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
