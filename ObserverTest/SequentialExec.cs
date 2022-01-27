using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
	class SequentialExec : ASave
	{
        public override void ExecFull(List<Save> SList)
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

        public override void ExecDiff(List<Save> SList)
        {

        }
    }
}
