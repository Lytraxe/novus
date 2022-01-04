using System;

using System.IO;

namespace Novus.Classes
{
    class Logger
    {
        bool logging = GlobalVariables.isLogging;
        public void InitializeFile()
        {
            if (logging)
            {
                if (!Directory.Exists(GlobalVariables._LogDirectory))
                {
                    Directory.CreateDirectory(GlobalVariables._LogDirectory);
                }
                //do something
                if (File.Exists(GlobalVariables._LogFilePath))
                {
                    DateTime dt = File.GetLastWriteTime(GlobalVariables._LogFilePath);
                    string renamedFileName = GlobalVariables._LogDirectory + @"log-" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "-" + dt.Hour.ToString() + "-" + dt.Minute.ToString() + ".log";
                    try
                    {
                        File.Move(GlobalVariables._LogFilePath, renamedFileName);
                    }
                    catch { }
                    using (StreamWriter sw = File.CreateText(GlobalVariables._LogFilePath)) ;
                    LogInfo("Application launched, renamed old log file.", "main");
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(GlobalVariables._LogFilePath)) ;
                    LogInfo("Application launched, created new log file.", "main");
                }
            }
        }

        public void LogInfo(string info, string sender)
        {
            if (logging)
            {
                string _log;
                DateTime curTime = DateTime.Now;

                _log = "[" + curTime.Hour + ":" + curTime.Minute + ":" + curTime.Second + "] [" + sender + "/INFO] : " + info;
                LogToFile(_log);
            }
        }

        public void LogWarning(string info, string sender)
        {
            if (logging)
            {
                string _log;
                DateTime curTime = DateTime.Now;

                _log = "[" + curTime.Hour + ":" + curTime.Minute + ":" + curTime.Second + "] [" + sender + "/WARN] : " + info;
                LogToFile(_log);
            }
        }

        public void LogError(string info, string sender)
        {
            if (logging)
            {
                string _log;
                DateTime curTime = DateTime.Now;

                _log = "[" + curTime.Hour + ":" + curTime.Minute + ":" + curTime.Second + "] [" + sender + "/ERROR] : " + info;
                LogToFile(_log);
            }
        }

        public void LogToFile(string log)
        {
            if (logging)
            {
                if (File.Exists(GlobalVariables._LogFilePath))
                {
                    try
                    {
                        using (StreamWriter sw = File.AppendText(GlobalVariables._LogFilePath))
                        {
                            sw.WriteLine(log);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
