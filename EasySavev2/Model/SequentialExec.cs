using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySavev2.Model
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

                        state.AddState(NbObj);

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

                        NbObj -= 1;

                        state.AddState(NbObj);
                    }
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

                        state.AddState(NbObj);

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

                        }

                        NbObj -= 1;

                        state.AddState(NbObj);
                    }
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
    }
}
