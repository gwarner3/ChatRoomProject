using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class TextLogger : ILoggable
    {
        private string message;
        public void RecieveMessage(string message)
        {
            this.message = message;
            SaveMessage();
        }

        public void SaveMessage()
        {
            string path = @"D:\temp\MyTest.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);

                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
