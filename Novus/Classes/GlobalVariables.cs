

namespace Novus.Classes
{
    class GlobalVariables
    {
        public static string _AppDataDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\Novus\";
        //public const string _AppDataDirectory = @" ";
        public static string _PrefFilePath =  _AppDataDirectory + @"preferences.xml";
        public static string _LogDirectory =  _AppDataDirectory + @"logs\";
        public static string _LogFilePath = _LogDirectory + @"latest.log";
        public static string _XmlPath = _AppDataDirectory + @"Items\";

        public static Item curSelectedItem;
        public static bool isLogging;
        public const string prefVersion = "1.0";
    }
}
