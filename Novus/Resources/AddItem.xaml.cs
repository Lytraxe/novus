using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Novus.Classes;

namespace Novus.Resources
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Page
    {

        MainWindow mainWindow;
        string iconPath;
        Logger logger = new Logger();

        //Initialization..
        public AddItem()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = Window.GetWindow(this) as MainWindow;
        }


        //Button click events..
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.BackPage();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (textPath.Text != "" && textAddName.Text != "")
            {
                //TODO : Validate path of file..
                if (Utilities.FileExists(textPath.Text))
                {
                    //Start serializing.
                    Item item = new Item { Name = textAddName.Text, Path = textPath.Text, IconLocation = iconPath, LaunchPara = textLaunchPara.Text };
                    if (XmlParser.Serialize(item))
                    {
                        logger.LogInfo("Successfully created shortcut for '" + item.Name + "'", "addItem");
                        mainWindow.BackPage();
                    }
                    else
                        logger.LogWarning("Something went wrong when trying to create shrotcut", "addItem");
                }
                else
                {
                    //Flash path field
                    var cAnimation = new System.Windows.Media.Animation.ColorAnimation();
                    cAnimation.To = Color.FromRgb(255, 122, 115);
                    cAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                    cAnimation.AutoReverse = true;
                    Utilities.StartAnimation(cAnimation, textPath.Name, new PropertyPath("(TextBox.Background).(SolidColorBrush.Color)"), this);
                }
            }
        }

        private void rectIcon_MouseUp(object sender, RoutedEventArgs e)
        {
            iconPath = Utilities.OpenFileDialog("Image Files (*.jpg, *.png, *.bmp, *.tiff)|*.JPG;*.PNG;*.BMP;*.TIFF|All Files (*.*)|*.*");

            //TODO : Update UI to show preview of the icon....

            if (iconPath != null)
            {
                imgIconPreview.Source = Utilities.BitmapImage(new Uri(iconPath), 160, 90);
                imgIconPreview.IsEnabled = true;
                imgIconPreview.Visibility = Visibility.Visible;
                rectIcon.Visibility = Visibility.Hidden;
                rectIcon.IsEnabled = false;
            }
        }

        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            var tmpPath = Utilities.OpenFileDialog("All Files (*.*)|*.*");
            if(tmpPath != null)
            {
                textPath.Text = tmpPath;
            }
        }
    }
}
