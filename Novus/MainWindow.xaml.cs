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

namespace Novus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Uri[] pages = { new Uri(@"Resources\Home.xaml", UriKind.RelativeOrAbsolute), // Index 0
                                new Uri(@"Resources\All.xaml", UriKind.RelativeOrAbsolute),  // 1
                                new Uri(@"Resources\AddItem.xaml", UriKind.RelativeOrAbsolute), // 2
                                new Uri(@"Resources\EditItem.xaml", UriKind.RelativeOrAbsolute)}; //3 
        private GridItem curSelectedGrid;
        Classes.Logger logger = new Classes.Logger();

        public MainWindow()
        {
            logger.InitializeFile();
            InitializeComponent();
            ChangePage(1);
            //throw new Exception("test");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.CallShutDown(0);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void ChangePage(int index)
        {
            ChangePreviewUI(true, null);
            //logger.LogInfo("Changed page to index : " + index.ToString(), "main");
            switch (index)
            {
                case 0:
                    frmMain.Navigate(pages[0]);
                    break;
                case 1:
                    frmMain.Navigate(pages[1]);
                    break;
                case 2:
                    frmMain.Navigate(pages[2]);
                    break;
                case 3:
                    frmMain.Navigate(pages[3]);
                    break;
                default:
                    frmMain.Navigate(pages[0]);
                    break;
            }
        }

        public void BackPage()
        {
            if (frmMain.CanGoBack == true)
            {
                frmMain.GoBack();
                //logger.LogInfo("Went back to previous page", "main");
            }
        }

        public void ChangePreviewUI(bool changePage, GridItem gItem)
        {
            if (changePage == false)
            {
                if (Classes.GlobalVariables.curSelectedItem != null)
                {
                    lblName.Content = Classes.GlobalVariables.curSelectedItem.Name;
                    lblPath.Text = Classes.GlobalVariables.curSelectedItem.Path;
                    if (cnvPreview.Visibility == Visibility.Hidden)
                        cnvPreview.Visibility = Visibility.Visible;

                    if (curSelectedGrid != null)
                    {
                        curSelectedGrid.DisableOutline();
                        curSelectedGrid = gItem;
                        curSelectedGrid.EnableOutline();
                    }
                    if (curSelectedGrid == null)
                    {
                        curSelectedGrid = gItem;
                        curSelectedGrid.EnableOutline();
                    }
                }
            }
            else
            {
                if (cnvPreview.Visibility == Visibility.Visible)
                    cnvPreview.Visibility = Visibility.Hidden;
            }
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.GlobalVariables.curSelectedItem != null)
            {
                if (Classes.GlobalVariables.curSelectedItem.LaunchPara == null || Classes.GlobalVariables.curSelectedItem.LaunchPara == "")
                {
                    Classes.ProcessManager.Start(Classes.GlobalVariables.curSelectedItem.Path);
                }
                else
                {
                    Classes.ProcessManager.Start(Classes.GlobalVariables.curSelectedItem.Path, Classes.GlobalVariables.curSelectedItem.LaunchPara);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(3);
        }

        private void btnPref_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Classes.Preference pref = new Classes.Preference { Version = Classes.GlobalVariables.prefVersion };
            if (Classes.GlobalVariables.isLogging == true)
                pref.EnableLog = "true";
            else
                pref.EnableLog = "false";
            Novus.Resources.PrefWindow prefW = new Novus.Resources.PrefWindow(pref);
            prefW.Show();
        }
    }
}
