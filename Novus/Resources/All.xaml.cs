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

namespace Novus.Resources
{
    /// <summary>
    /// Interaction logic for All.xaml
    /// </summary>
    public partial class All : Page
    {
        Classes.Logger logger = new Classes.Logger();

        public All()
        {
            InitializeComponent();
        }

        void AddRow()
        {
            itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(135, GridUnitType.Pixel) });
        }

        // its kinda TRICKY. i dont even wanna explain it, just figure out
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.IO.Directory.Exists(Classes.GlobalVariables._XmlPath))
                System.IO.Directory.CreateDirectory(Classes.GlobalVariables._XmlPath);
            string[] files = Utilities.ReturnAllFiles(Novus.Classes.GlobalVariables._XmlPath);
            logger.LogInfo("Found " + files.Length.ToString() + " shortcuts at " + Classes.GlobalVariables._XmlPath.ToString(), "allItemPage");
            if (files.Length != 0)
            {
                int _length = files.Length - 1;
                int _remainder;
                int _result = Math.DivRem(_length, 5, out _remainder);
                if (_result <= 1)
                { AddRow(); AddRow(); }
                else
                {
                    for (int i = 0; i < _result + 1; i++)
                    {
                        AddRow();
                    }
                }


                for (int i = 0; i < files.Length; i++)
                {
                    int remainder;
                    int result = Math.DivRem(i, 5, out remainder);
                    Novus.Classes.Item item = XmlParser.Deserialize(files[i]);
                    if (Utilities.ItemCheck(item))
                    {
                        GridItem block = new GridItem(item);
                        Grid.SetRow(block, result);
                        Grid.SetColumn(block, remainder);
                        itemGrid.Children.Add(block);
                    }
                }
            } else
            {
                lblNoItem.IsEnabled = true;
                lblNoItem.Visibility = Visibility.Visible;
            }
        }

        private void rectAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mwindow = Window.GetWindow(this) as MainWindow;
            mwindow.ChangePage(2);
        }
    }
}
