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
                foreach (Save Obj in SList)
                {
                    string name = Obj.name;
                    string src = Obj.src;
                    string dest = Obj.dest;
                    string type = Obj.type;

                    File.Copy(src, dest, true);

                    Console.WriteLine("File \x1B[4m" + src + "\x1B[0m well copied at \x1B[4m" + dest + "\x1B[0m");
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExecDiff(List<Save> SList)
        {
            Console.WriteLine("Seq Diff");
        }
    }
}
