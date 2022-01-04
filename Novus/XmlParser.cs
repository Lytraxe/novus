using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Novus.Classes;

namespace Novus
{
    class XmlParser
    {
 
        public static bool Serialize(Item item)
        {
            if (item != null)
            {
                try
                {
                    if (!Directory.Exists(GlobalVariables._XmlPath))
                        Directory.CreateDirectory(GlobalVariables._XmlPath);
                    XmlSerializer serializer = new XmlSerializer(typeof(Item));
                    using (StreamWriter sw = new StreamWriter(GlobalVariables._XmlPath + item.Name + ".xml"))
                    {
                        serializer.Serialize(sw, item);
                        return true;
                    }
                }

                catch (Exception e)
                {
                    Utilities.ShowExceptionBox(e, "xmlSerialize");
                    return false;
                }
            }
            else
                return false;
        }

        public static bool UpdateSerialize(string oldName, Item item)
        {
            if (item != null && oldName != null)
            {
                if (item.Name == oldName)
                {
                    return Serialize(item);
                }
                else
                {
                    try
                    {
                        File.Delete(GlobalVariables._XmlPath + oldName + ".xml");
                        return Serialize(item);
                    }
                    catch (Exception e)
                    {
                        Utilities.ShowExceptionBox(e, "xmlSerialize");
                        return false;
                    }
                }
            }
            else
                return false;
        }

        public static Item Deserialize(string path)
        {
            if (File.Exists(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(Item));
                using (FileStream reader = new FileStream(path, FileMode.Open))
                {
                    Item item = s.Deserialize(reader) as Item;
                    return item;
                }
            }
            return null;
        }

        public static string[] getFiles()
        {
            return Directory.GetFiles(GlobalVariables._XmlPath);
        }

        //For Preference File
        public static bool Serialize(Preference pref)
        {
            if (pref != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Preference));
                    using (StreamWriter sw = new StreamWriter(GlobalVariables._PrefFilePath))
                    {
                        serializer.Serialize(sw, pref);
                        return true;
                    }
                }

                catch (Exception e)
                {
                    Utilities.ShowExceptionBox(e, "xmlSerialize");
                    return false;
                }
            }
            else
                return false;
        }
        public static Preference Deserialize()
        {
            if (File.Exists(GlobalVariables._PrefFilePath))
            {
                XmlSerializer s = new XmlSerializer(typeof(Preference));
                using (FileStream reader = new FileStream(GlobalVariables._PrefFilePath, FileMode.Open))
                {
                    Preference pref = s.Deserialize(reader) as Preference;
                    return pref;
                }
            }
            return null;
        }
    }
}
