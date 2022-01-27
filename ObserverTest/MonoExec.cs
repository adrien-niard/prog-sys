using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
    class MonoExec : ASave
    {
        public override void ExeComplet(List<Save> SList)
        {
            try
            {
                /*File.Copy(src, dest, true);*/
            }

            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExeDiff(List<Save> SList)
        {

        }
    }
}
