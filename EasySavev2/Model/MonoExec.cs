using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace EasySavev2.Model
{
    class MonoExec : ASave
    {
        //Cycle through saves to copy the specified directory in full mode
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
                        Process CryptProcess = new Process();

                        //Define source/destination files
                        FileInfo FiSrc = new FileInfo(Path.Combine(DirSrc.ToString(), fi.Name));
                        FileInfo FiDest = new FileInfo(Path.Combine(DirDest.ToString(), fi.Name));

                        save.src = src + fi.Name;
                        save.dest = dest + fi.Name;

                        var ct = Obj.GetStartTimer();

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

                        CryptProcess.StartInfo.FileName = @"..\..\..\..\publish\Crypto.exe";
                        CryptProcess.StartInfo.ArgumentList.Add(src + "/" + fi.Name);
                        CryptProcess.StartInfo.ArgumentList.Add(fi.Name);
                        CryptProcess.StartInfo.ArgumentList.Add("0111000101101001");
                        CryptProcess.Start();

                        Obj.CryptTime = Obj.GetStopTimer(ct);

                        //Stop timer
                        Obj.RunTime = Obj.GetStopTimer(sw);
                        log.AddLog();
                    }
                    NbObj -= 1;
                }
                state.AddState(NbObj);
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
                        Process CryptProcess = new Process();

                        //Define source/destination files
                        FileInfo FiSrc = new FileInfo(Path.Combine(DirSrc.ToString(), fi.Name));
                        FileInfo FiDest = new FileInfo(Path.Combine(DirDest.ToString(), fi.Name));

                        save.src = src + fi.Name;
                        save.dest = dest + fi.Name;

                        state.AddState(NbObj);

                        //Compare the last write time of both files
                        if (FiSrc.LastWriteTimeUtc != FiDest.LastWriteTimeUtc)
                        {
                            var ct = Obj.GetStartTimer();

                            /*CryptProcess.StartInfo.FileName = @"C:\Users\adrie\source\repos\prog-sys\publish\Crypto.exe";
                            CryptProcess.StartInfo.ArgumentList.Add(src + "/" + fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add(fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add("0111000101101001");
                            CryptProcess.Start();*/

                            try
                            {
                                //Copy source directory in destination directory
                                File.Copy(FiSrc.ToString(), FiDest.ToString(), true);
                            }
                            catch (IOException iox)
                            {
                                Console.WriteLine(iox.Message);
                            }

                            CryptProcess.StartInfo.FileName = @"C:\Users\adrie\source\repos\prog-sys\publish\Crypto.exe";
                            CryptProcess.StartInfo.ArgumentList.Add(src + "/" + fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add(fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add("0111000101101001");
                            CryptProcess.Start();

                            /*CryptProcess.StartInfo.FileName = @"C:\Users\adrie\source\repos\prog-sys\publish\Crypto.exe";
                            CryptProcess.StartInfo.ArgumentList.Add(dest + "/" + fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add(fi.Name);
                            CryptProcess.StartInfo.ArgumentList.Add("0111000101101001");
                            CryptProcess.Start();*/

                            Obj.CryptTime = Obj.GetStopTimer(ct);

                            //Stop timer
                            Obj.RunTime = Obj.GetStopTimer(sw);
                            log.AddLog();
                        }
                    }
                    NbObj -= 1;

                }
                state.AddState(NbObj);

            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
    }
}
