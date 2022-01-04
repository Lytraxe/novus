using System;
using System.Windows;
using System.Windows.Media.Imaging;

using System.IO;

namespace Novus
{
    class Utilities
    {
        static Classes.Logger logger = new Classes.Logger();

       public static void ShowExceptionBox(Exception e, string sender)
        {
            MessageBox.Show( e.ToString(), e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            logger.LogError(e.Message + "\n" + e.ToString(), sender);
        }
        public static void ShowExceptionBox(Exception e)
        {
            MessageBox.Show(e.ToString(), e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string[] ReturnAllFiles(string path)
        {
            return Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);
        }

        public static void TestBox(string str)
        {
            MessageBox.Show("test(" + str + ")", "test test");
        }

        //-- Show a Choose File / Open File Dialog
        public static string OpenFileDialog(string extFilter)
        {
            using (System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                fileDialog.Title = "Choose a file..";
                fileDialog.Multiselect = false;
                fileDialog.Filter = extFilter;

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return fileDialog.FileName;
                }
                else
                    return null;
            }
        }

        //-- Generate Bitmap and decode hight and width of an Image Uri
        public static BitmapImage BitmapImage(Uri imageLocation, int decodePixelWidth, int decodePixelHeight)
        {
            try
            {
                BitmapImage bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.UriSource = imageLocation;
                bmpImg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bmpImg.CacheOption = BitmapCacheOption.None;
                bmpImg.DecodePixelHeight = decodePixelHeight;
                bmpImg.DecodePixelWidth = decodePixelWidth;
                bmpImg.EndInit();
                return bmpImg;
            }
            catch (DirectoryNotFoundException ex)
            {
                logger.LogWarning("Image at '" + imageLocation + "' was not found, returning null", "utilBitmap");
                return null;
            }
            catch(NotSupportedException ex)
            {
                logger.LogWarning("Image at '" + imageLocation + "' is either corrupted or not supported, returning null", "utilBitmap");
                return null;
            }
            
        }

        //-- Show a confirmation box and return true or false based on the user's response
        public static bool ShowConfirmationBox(string msg)
        {
            MessageBoxResult result = MessageBox.Show(msg, "Waiting for Confirmation...", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }
        }

        //-- Delete file and log it
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    logger.LogInfo("Deleting shortcut at '" + path + "'", "utilDeleteFile");
                    File.Delete(path);

                }
                catch (Exception e)
                {

                    ShowExceptionBox(e, "utilDeleteFile");
                }
            }
        }

        //-- Check if file exists at requested file
        public static bool FileExists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }

        //-- Perform a check whether requested shortcut exists or not. and ask to delete
        public static bool ItemCheck(Classes.Item item)
        {
            if(File.Exists(item.Path))
            {
                return true;
            }
            else
            {
                logger.LogWarning("File at: '" + item.Path + "' was not found. Waiting for confirmation to delete the shortcut", "utilItemCheck");
                if (ShowConfirmationBox("File at : '" + item.Path + "' was not found. \nDo you want to delete this shortcut ? "))
                {
                    logger.LogInfo("Deleting shortcut : '" + item.Name + "'", "utilItemCheck");
                    DeleteFile(Classes.GlobalVariables._XmlPath + item.Name + ".xml");
                    return false;
                }
                else
                    logger.LogInfo("skipping file", "utilItemCheck"); return false;
            }
        }

        //Animating Property of a control
        public static void StartAnimation(System.Windows.Media.Animation.AnimationTimeline _animation, string objectName, PropertyPath pathToProperty, FrameworkElement passTheElement)
        {
            var storyBoard = new System.Windows.Media.Animation.Storyboard();
            storyBoard.Children.Add(_animation);
            System.Windows.Media.Animation.Storyboard.SetTargetName(_animation, objectName);
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(_animation, pathToProperty);
            storyBoard.Begin(passTheElement);
        }
    }
}
