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
            string path = @"C:\Windows\Temp\PermanentChatLog.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);

                }
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
