using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace EasySavev2.Model
{
    class ParallèleExec : ASave
    {
        private delegate void DELG();
        public override void ExecFull(List<Save> SList, Log log, State state, int i)
        {
            try
            {
                Save Obj = SList[i];
                int NbObj = i;

                //Start the timer & define variables for save attributes
                var runTime = Obj.GetStartTimer();
                string name = Obj.Name;
                string src = Obj.Src;
                string dest = Obj.Dest;
                string type = Obj.Type;

                //Set object for directories info
                DirectoryInfo DirSrc = new DirectoryInfo(src);
                DirectoryInfo DirDest = new DirectoryInfo(dest);

                //Cycle through files in the source folder to copy them into the destination folder
                foreach (FileInfo fi in DirSrc.GetFiles())
                {
                    //Define source/destination files
                    string SrcFull = Path.Combine(DirSrc.ToString(), fi.Name);
                    string DestFull = Path.Combine(DirDest.ToString(), fi.Name);

                    FileInfo FiSrc = new FileInfo(SrcFull);
                    FileInfo FiDest = new FileInfo(DestFull);

                    Obj.Src = SrcFull;
                    Obj.Dest = DestFull;

                    state.AddState(NbObj);

                    //Launch semaphore to copy files
                    if (fi.Length < 1000) //1000 à changer avec un paramètre
                    {
                        Semaphore semaphore = new Semaphore(SList.Count, SList.Count);
                        DELG delg_semaphore = () =>
                        {
                            semaphore.WaitOne();
                            File.Copy(FiSrc.ToString(), FiDest.ToString(), true);
                        };

                        Thread SemaThread = new Thread(delg_semaphore.Invoke);
                        SemaThread.Start();

                        //Call the CryptoSoft method to decrypt the source
                        CryptoSoft(DestFull, "encryptkey1234", Obj);
                    }

                    //Stop timer
                    Obj.RunTime = Obj.GetStopTimer(runTime);
                    log.AddLog();
                }
                NbObj -= 1;
                state.AddState(NbObj);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExecDiff(List<Save> SList, Log log, State state, int i)
        {
            try
            {
                Save Obj = SList[0];
                int NbObj = i;

                //Start the timer & define variables for save attributes
                var runTime = Obj.GetStartTimer();
                string name = Obj.Name;
                string src = Obj.Src;
                string dest = Obj.Dest;
                string type = Obj.Type;

                //Set object for directories info
                DirectoryInfo DirSrc = new DirectoryInfo(src);
                DirectoryInfo DirDest = new DirectoryInfo(dest);

                //Cycle through the file in the folder to copy them into the destination folder
                foreach (FileInfo fi in DirSrc.GetFiles())
                {
                    //Define source/destination files
                    string SrcDiff = Path.Combine(DirSrc.ToString(), fi.Name);
                    string DestDiff = Path.Combine(DirDest.ToString(), fi.Name);

                    //Define source/destination files
                    FileInfo FiSrc = new FileInfo(SrcDiff);
                    FileInfo FiDest = new FileInfo(DestDiff);

                    Obj.Src = SrcDiff;
                    Obj.Dest = DestDiff;

                    state.AddState(NbObj);

                    //Compare the last write time of both files
                    if (FiSrc.LastWriteTimeUtc != FiDest.LastWriteTimeUtc)
                    {

                        //Launch semaphore to copy files
                        if (fi.Length < 1000) //1000 à changer avec un paramètre
                        {

                            Semaphore semaphore = new Semaphore(SList.Count, SList.Count);
                            DELG delg = () =>
                            {
                                semaphore.WaitOne();
                                File.Copy(FiSrc.ToString(), FiDest.ToString(), true);
                            };

                            Thread t1 = new Thread(delg.Invoke);
                            t1.Start();

                            //Call the CryptoSoft method
                            CryptoSoft(DestDiff, "encryptkey1234", Obj);
                        }

                        //Stop timer
                        Obj.RunTime = Obj.GetStopTimer(runTime);
                        log.AddLog();
                    }
                }
                NbObj -= 1;

                state.AddState(NbObj);

            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public void CryptoSoft(string SrcPath, string key, Save Obj)
        {
            //Method to launch cryptosoft.exe
            Process CryptProcess = new Process();

            var cryptTime = Obj.GetStartTimer();

            //Set the filename and all arguments needed to crypt files
            CryptProcess.StartInfo.FileName = @"..\..\..\..\publish\Crypto.exe";
            CryptProcess.StartInfo.ArgumentList.Add(SrcPath);
            CryptProcess.StartInfo.ArgumentList.Add(key);
            CryptProcess.Start();

            Obj.CryptTime = Obj.GetStopTimer(cryptTime);
        }
    }
}