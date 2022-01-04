using System;
using System.Diagnostics;
using System.IO;

namespace Novus.Classes
{
    class ProcessManager
    {
        public static void Start(string path, string launchPara)
        {
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo pInfo = new ProcessStartInfo(path);
                    pInfo.Arguments = launchPara;

                    pInfo.WorkingDirectory = Path.GetDirectoryName(path);
                    p.StartInfo = pInfo;

                    p.Start();
                    //LOGGER here
                }
            }
            catch (Exception e)
            {
                //LOGGER here
                Utilities.ShowExceptionBox(e, "process");
            }
        }

        public static void Start(string path)
        {
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo pInfo = new ProcessStartInfo(path);

                    pInfo.WorkingDirectory = Path.GetDirectoryName(path);
                    p.StartInfo = pInfo;

                    p.Start();
                    //LOGGER here
                }
            }
            catch (Exception e)
            {
                //LOGGER here
                Utilities.ShowExceptionBox(e, "process");
            }
           
        }
    }
}
