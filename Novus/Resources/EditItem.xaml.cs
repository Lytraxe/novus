using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Novus.Classes;

namespace Novus.Resources
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Page
    {
        MainWindow mainWindow;
        Item item;

        string iconPath;

        public EditItem()
        {
            InitializeComponent();
            if (GlobalVariables.curSelectedItem != null)
                item = GlobalVariables.curSelectedItem;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.FileExists(textPath.Text))
            {
                var oldName = item.Name;
                if (textPath.Text != "" && textAddName.Text != "")
                {
                    item.Name = textAddName.Text;
                    item.Path = textPath.Text;
                    if (iconPath != null)
                        item.IconLocation = iconPath;
                    if (XmlParser.UpdateSerialize(oldName, item))
                    { GlobalVariables.curSelectedItem = null; mainWindow.BackPage(); }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.curSelectedItem = null;
            mainWindow.BackPage();
        }

        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            var tmpPath = Utilities.OpenFileDialog("All Files (*.*)|*.*");
            if (tmpPath != null)
            {
                textPath.Text = tmpPath;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = Window.GetWindow(this) as MainWindow;
            if (item != null)
            {

                textAddName.Text = item.Name;
                textPath.Text = item.Path;
                textLaunchPara.Text = item.LaunchPara;
                lblEName.Content = item.Name;

                if (item.IconLocation != null && item.IconLocation != "")
                {
                    imgIconPreview.Source = Utilities.BitmapImage(new Uri(item.IconLocation, UriKind.RelativeOrAbsolute), 160, 90);
                }
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnBrowsePath.IsEnabled = false;
                btnChangeImg.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        private void btnChangeImg_Click(object sender, RoutedEventArgs e)
        {
            iconPath = Utilities.OpenFileDialog("Image Files (*.jpg, *jpeg, *.png, *.bmp, *.tiff)|*.JPG;*.JPEG;*.PNG;*.BMP;*.TIFF|All Files (*.*)|*.*");

            if(iconPath != null && iconPath != "")
            {
                imgIconPreview.Source = Utilities.BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute), 160, 90);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Windows.ConfirmationDialog cnfrmDiag = new Windows.ConfirmationDialog();

            if (Utilities.ShowConfirmationBox("Are you sure you want to delete this Item ? "))
            {
                Utilities.DeleteFile(GlobalVariables._XmlPath + item.Name + ".xml");
                GlobalVariables.curSelectedItem = null;
                mainWindow.BackPage();
            }
        }
    }
}
