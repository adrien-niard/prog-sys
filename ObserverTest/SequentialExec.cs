using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
	class SequentialExec : ASave
	{
        public override void ExecFull(List<Save> SList, Save save, Log log)
        {
            try
            {
                foreach (Save Obj in SList)
                {
                    var sw = Obj.GetStartTimer();
                    string name = Obj.name;
                    string src = Obj.src;
                    string dest = Obj.dest;
                    string type = Obj.type;

                    File.Copy(src, dest, true);

                    Console.WriteLine("File \x1B[4m" + src + "\x1B[0m well copied at \x1B[4m" + dest + "\x1B[0m");
                    Obj.RunTime = Obj.GetStopTimer(sw);
                    log.AddLog();
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExecDiff(List<Save> SList, Save save, Log log)
        {

            /*DirectoryInfo DiSrc = new DirectoryInfo(Obj.src);
            DirectoryInfo DiDest = new DirectoryInfo(Obj.dest);*/

            try
            {
                foreach (Save Obj in SList)
                {
                    DirectoryInfo DiSrc = new DirectoryInfo(Obj.src);
                    DirectoryInfo DiDest = new DirectoryInfo(Obj.dest);

                    CopyAll(DiSrc, DiDest, save, log);      
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public static void CopyAll(DirectoryInfo DiSrc, DirectoryInfo DiDest, Save Obj, Log log)
        {
            foreach (FileInfo fi in DiSrc.GetFiles())
            {
                Console.WriteLine(fi);
                /*if (FiSrc.LastWriteTimeUtc != FiDest.LastWriteTimeUtc)
                {
                    var sw = Obj.GetStartTimer();
                    string name = Obj.name;
                    string src = Obj.src;
                    string dest = Obj.dest;
                    string type = Obj.type;

                    File.Copy(src, dest, true);

                    Console.WriteLine("File \x1B[4m" + src + "\x1B[0m well copied at \x1B[4m" + dest + "\x1B[0m");
                    Obj.RunTime = Obj.GetStopTimer(sw);
                    log.AddLog();
                }*/

                Console.WriteLine(@"Copying {0}\{1}", DiDest.FullName, fi.Name);
                fi.CopyTo(Path.Combine(DiSrc.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in DiSrc.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    DiSrc.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, Obj, log);
            }
        }
    }
}
