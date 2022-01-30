using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
	class SequentialExec : ASave
	{
        public override void ExecFull(List<Save> SList, Save save, Log log, State state)
        {
            try
            {
                int NbObj = SList.Count;

                //Cycle through saves to copy the specified directory in differential mode
                foreach (Save Obj in SList)
                {
                    //Start the timer
                    var sw = Obj.GetStartTimer();
                    string name = Obj.name;
                    string src = Obj.src;
                    string dest = Obj.dest;
                    string type = Obj.type;

                    DirectoryInfo DirSrc = new DirectoryInfo(src);
                    DirectoryInfo DirDest = new DirectoryInfo(dest);

                    //Cycle through the file in the folder to copy them into the destination folder
                    foreach (FileInfo fi in DirSrc.GetFiles())
                    {
                        //Define source/destination files
                        FileInfo FiSrc = new FileInfo(Path.Combine(DirSrc.ToString(), fi.Name));
                        FileInfo FiDest = new FileInfo(Path.Combine(DirDest.ToString(), fi.Name));

                        save.src = src + "/" + fi.Name;
                        save.dest = dest + "/" + fi.Name;

                        try
                        {
                            //Copy source directory in destination directory
                            File.Copy(FiSrc.ToString(), FiDest.ToString(), true);
                        }
                        catch (IOException iox)
                        {
                            Console.WriteLine(iox.Message);
                        }

                        //Stop timer
                        Obj.RunTime = Obj.GetStopTimer(sw);
                        log.AddLog();
                        state.AddState(NbObj);
                    }

                    NbObj -= 1;
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExecDiff(List<Save> SList, Save save, Log log, State state)
        {
            try
            {
                int NbObj = SList.Count;

                //Cycle through saves to copy the specified directory in differential mode
                foreach (Save Obj in SList)
                {
                    //Start the timer
                    var sw = Obj.GetStartTimer();
                    string name = Obj.name;
                    string src = Obj.src;
                    string dest = Obj.dest;
                    string type = Obj.type;

                    DirectoryInfo DirSrc = new DirectoryInfo(src);
                    DirectoryInfo DirDest = new DirectoryInfo(dest);

                    //Cycle through the file in the folder to copy them into the destination folder
                    foreach (FileInfo fi in DirSrc.GetFiles())
                    {
                        //Define source/destination files
                        FileInfo FiSrc = new FileInfo(Path.Combine(DirSrc.ToString(), fi.Name));
                        FileInfo FiDest = new FileInfo(Path.Combine(DirDest.ToString(), fi.Name));

                        save.src = src + "/" + fi.Name;
                        save.dest = dest + "/" + fi.Name;

                        //Compare the last write time of both files
                        if (FiSrc.LastWriteTimeUtc != FiDest.LastWriteTimeUtc)
                        {
                            try
                            {
                                //Copy source directory in destination directory
                                File.Copy(FiSrc.ToString(), FiDest.ToString(), true);
                            }
                            catch (IOException iox)
                            {
                                Console.WriteLine(iox.Message);
                            }

                            //Stop timer
                            Obj.RunTime = Obj.GetStopTimer(sw);
                            log.AddLog();
                            state.AddState(NbObj);
                        }
                    }

                    NbObj -= 1;
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
    }
}
