using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using System.Windows;

namespace EasySavev2.Model
{
    class ParallèleExec : ASave
    {
        private delegate void DELG();
        
        public override void ExecFull(List<Save> SList, Log log, State state, int NbObj, int NbSave)
        {
            try
            {
                Save Obj = SList[NbSave];
                
                //Start the timer & define variables for save attributes
                var runTime = Obj.GetStartTimer();
                string name = Obj.Name;
                string src = Obj.Src;
                string dest = Obj.Dest;
                string type = Obj.Type;

                //Set object for directories info
                DirectoryInfo DirSrc = new DirectoryInfo(src);
                DirectoryInfo DirDest = new DirectoryInfo(dest);

                //Variable for size max of a file to copy
                string sizeMax = ConfigurationManager.AppSettings.Get("size");
                int koMax = 0;

                if (sizeMax != "")
                {
                    koMax = Int32.Parse(sizeMax);
                }

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
                    List<FileInfo> Priority = new List<FileInfo>();
                    List<FileInfo> NoPriority = new List<FileInfo>();
                    ConfigurationManager.RefreshSection("appSettings");
                    string[] keys = ConfigurationManager.AppSettings.AllKeys;

                    foreach (string cle in keys)
                    {
                        if(cle.IndexOf(".") != -1 && cle.Substring(0, cle.IndexOf(".")) == "pri") 
                        { 
                            if (fi.Extension == cle.Substring(3, cle.Length - 3))
                            {
                                Priority.Add(fi);
                            }
                            else
                            {
                                NoPriority.Add(fi);
                            }
                        }
                    }
                    foreach (FileInfo file in Priority)
                    {
                        //Launch semaphore to copy files
                        if (koMax == 0 || fi.Length < koMax)
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

                            CryptoSoft(DestFull, "encryptkey1234", Obj, fi);
                            log.AddLog();
                        }
                    }

                    foreach (FileInfo file in NoPriority)
                    {
                        if (koMax == 0 || fi.Length < koMax)
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
                            CryptoSoft(DestFull, "encryptkey1234", Obj, fi) ;
                            log.AddLog();
                        }
                    }
                    //Stop timer
                    Obj.RunTime = Obj.GetStopTimer(runTime);

                }
                NbObj -= 1;
                state.AddState(NbObj);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public override void ExecDiff(List<Save> SList, Log log, State state, int NbObj, int NbSave)
        {
            try
            {
                Save Obj = SList[NbSave];

                //Start the timer & define variables for save attributes
                var runTime = Obj.GetStartTimer();
                string name = Obj.Name;
                string src = Obj.Src;
                string dest = Obj.Dest;
                string type = Obj.Type;

                //Set object for directories info
                DirectoryInfo DirSrc = new DirectoryInfo(src);
                DirectoryInfo DirDest = new DirectoryInfo(dest);

                //Variable for size max of a file to copy
                string sizeMax = ConfigurationManager.AppSettings.Get("size");
                int koMax = Int32.Parse(sizeMax);

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
                        if (koMax.ToString() == null || fi.Length < koMax)
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
                            
                            CryptoSoft(DestDiff, "encryptkey1234", Obj, fi);
                            //Stop timer
                            Obj.RunTime = Obj.GetStopTimer(runTime);
                            log.AddLog();
                        }
                    }
                    List<FileInfo> Priority = new List<FileInfo>();
                    List<FileInfo> NoPriority = new List<FileInfo>();
                    ConfigurationManager.RefreshSection("appSettings");
                    string[] keys = ConfigurationManager.AppSettings.AllKeys;

                    foreach (string cle in keys)
                    {
                        if (cle.IndexOf(".") != -1 && cle.Substring(0, cle.IndexOf(".")) == "pri")
                        {
                            if (fi.Extension == cle.Substring(3, cle.Length - 3))
                            {
                                Priority.Add(fi);
                            }
                            else
                            {
                                NoPriority.Add(fi);
                            }
                        }
                    }
                    foreach (FileInfo file in Priority)
                    {
                        //Launch semaphore to copy files
                        if (koMax == 0 || fi.Length < koMax)
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

                            CryptoSoft(DestDiff, "encryptkey1234", Obj, fi);
                            log.AddLog();
                            /*while (State == 1)
                            {
                                Thread.Sleep(2000);
                            }*/
                        }
                    }
                    foreach (FileInfo file in NoPriority)
                    {
                        if (koMax == 0 || fi.Length < koMax)
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
                            CryptoSoft(DestDiff, "encryptkey1234", Obj, fi);
                            log.AddLog();
                        }
                        //Stop timer
                        Obj.RunTime = Obj.GetStopTimer(runTime);
                    }
                    NbObj -= 1;

                    state.AddState(NbObj);


                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public void CryptoSoft(string SrcPath, string key, Save Obj, FileInfo fi)
        {
            //Method to launch cryptosoft.exe
            Process CryptProcess = new Process();

            var cryptTime = Obj.GetStartTimer();

            //Set the filename and all arguments needed to crypt files
            CryptProcess.StartInfo.FileName = @"..\..\..\..\publish\Crypto.exe";
            CryptProcess.StartInfo.ArgumentList.Add(SrcPath);
            CryptProcess.StartInfo.ArgumentList.Add(key);
            
            ConfigurationManager.RefreshSection("appSettings");
            string[] keys = ConfigurationManager.AppSettings.AllKeys;

            foreach (string cle in keys)
            {
                if (keys != null)
                {
                    if (cle.IndexOf(".") != -1 && cle.Substring(0, cle.IndexOf(".")) == "ext")
                    {
                        if (cle.Substring(3, cle.Length - 3) == fi.Extension)
                        {

                            CryptProcess.Start();
                        }
                    }

                }
            }


            Obj.CryptTime = Obj.GetStopTimer(cryptTime);
        }
    }
}