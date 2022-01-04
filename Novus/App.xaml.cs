using System;

using System.Windows;

namespace Novus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Classes.Logger logger;
        public static void CallShutDown(int exiCode)
        {
            switch(exiCode)
            {
                case 0:
                    logger.LogInfo("Application shutdown called by user", "main");
                    break;
                case 1:
                    logger.LogWarning("Unexpected shutdown initialized, most probably a crash", "main");
                    break;
                default:
                    logger.LogWarning("An unknown error caused application shutdown", "main");
                    break;
            }
            logger.LogInfo("Shutting down application (Exit Code : " + exiCode.ToString() + ")", "main");
            App.Current.Shutdown(exiCode);
        }

        private void AppStartup(object sender, StartupEventArgs arg)
        {
            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            //Check if it is the first time startup...
            if(!Utilities.FileExists(Classes.GlobalVariables._PrefFilePath))
            {
                SetupFirstTimeStartup();
                Classes.GlobalVariables.isLogging = true;
            }
            else
            {
                //-- Theres no need of this block of code right now.
                Classes.Preference pref = XmlParser.Deserialize();
                if (pref.Version != Classes.GlobalVariables.prefVersion || pref.Version == null)
                {
                    pref.Version = Classes.GlobalVariables.prefVersion;
                    XmlParser.Serialize(pref);
                    pref = XmlParser.Deserialize();
                }

                if (pref.EnableLog == "true")
                    Classes.GlobalVariables.isLogging = true;
                else
                    Classes.GlobalVariables.isLogging = false;
            }
            logger = new Classes.Logger();
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMsg = string.Format("An unhandled exception occurred : {0} \n{1}", e.Exception.Message, e.Exception.ToString());
            logger.LogError(errorMsg, "dispatcherUnhandledException");
            e.Handled = true;
            CallShutDown(1);
        }

        void OnUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            string errorMsg = string.Format("An unhandled exception occurred : {0}", e.ExceptionObject.ToString());
            logger.LogError(errorMsg, "unhandledException");
            CallShutDown(1);
        }

        private void SetupFirstTimeStartup()
        {
            try
            {
                if(!System.IO.Directory.Exists((Classes.GlobalVariables._AppDataDirectory)))
                    System.IO.Directory.CreateDirectory(Classes.GlobalVariables._AppDataDirectory);
                if(!System.IO.Directory.Exists((Classes.GlobalVariables._LogDirectory)))
                    System.IO.Directory.CreateDirectory(Classes.GlobalVariables._LogDirectory);
                if(!System.IO.Directory.Exists((Classes.GlobalVariables._XmlPath)))
                    System.IO.Directory.CreateDirectory(Classes.GlobalVariables._XmlPath);

                //wont work like this.... will probably have to serialize xml instead.
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(Classes.GlobalVariables._PrefFilePath)) ;
                Classes.Preference pref = new Classes.Preference { Version = Classes.GlobalVariables.prefVersion, EnableLog = "false" };
                XmlParser.Serialize(pref);
            }
            catch(Exception e)
            {
                Utilities.ShowExceptionBox(e);
            }
        }
    }
}
