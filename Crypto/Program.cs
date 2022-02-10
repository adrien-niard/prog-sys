using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;


namespace crypto
{
    class CEncryption
    {

        /*public string Encrypt(string strIn, string strKey)
        {
            string sbOut = String.Empty;
            for (int i = 0; i < strIn.Length; i++)
            {
                sbOut += String.Format("{0:00}", strIn[i] ^ strKey[i % strKey.Length]);
            }
            return sbOut;
        }
        public string Decrypt(string strIn, string strKey)
        {
            string sbOut = String.Empty;
            for (int i = 0; i < strIn.Length; i += 2)
            {
                byte code = Convert.ToByte(strIn.Substring(i, 2));
                sbOut += (char)(code ^ strKey[(i / 2) % strKey.Length]);
            }
            return sbOut;
        }*/
        public string EncryptOrDecrypt(string text, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < text.Length; c++)
            {
                //take next character from string
                char character = text[c];
                //cast to a uint
                int charCode = (int)character;
                //figure out which character to take from the key
                int keyPosition = c % key.Length; //use modulo to "wrap round"
                                                  //take the key character
                char keyChar = key[keyPosition];
                //cast it to a uint also
                int keyCode = (int)keyChar;
                //perform XOR on the two character codes
                int combinedCode = charCode ^ keyCode;
                //cast back to a char
                char combinedChar = (char)combinedCode;
                //add to the result
                result.Append(combinedChar);
            }
            return result.ToString();
        }
        public static async Task WriteFile(string FileToRead, string FinalRe)
        {
            string text = FinalRe;

            await File.WriteAllTextAsync(FileToRead, text);

        }


        public static void Main(string[] args)
        {
            CEncryption en = new CEncryption();
            // Xor Key
            string key = args[2];
            //Path
            string FileToRead = args[0]; //@"C:\Users\JeanGONCALVES\test.txt";
            //Name File
            string NameFile = args[1];
            
            // File Info
            FileInfo fi = new FileInfo(args[0]);

            IEnumerable<string> line = File.ReadLines(FileToRead);
            
            // Recover the Message of the TXT file
            string Message = String.Join(Environment.NewLine, line);
            //Encrypt of the message
            string Solution = en.EncryptOrDecrypt(Message, key);
            Debug.WriteLine("Contenu : " + Solution + " " + NameFile);
            // Rewrite the txt file with the Encrypt solution
            using (TextWriter tw = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                tw.Write(Solution);
            }
            //Recover again the Solution, Decrypt it, and rewrite to get back the first message
            string FinalRe = en.EncryptOrDecrypt(Solution, key);
            
            using (TextWriter tw = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                tw.Write(FinalRe);
            }
            Console.ReadKey();
            
        }

    }
}