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

namespace Novus
{
    /// <summary>
    /// Interaction logic for GridItem.xaml
    /// </summary>
    public partial class GridItem : UserControl
    {
        Item item;
        MainWindow mWindow;

        public GridItem(Item _item)
        {
            InitializeComponent();
            item = _item;
            Initialize();
        }

        private void Trigger_MouseEnter(object sender, MouseEventArgs e)
        {
            rectPlay.IsEnabled = true;
            rectPlay.Visibility = Visibility.Visible;

            icnEdit.IsEnabled = true;
            icnEdit.Visibility = Visibility.Visible;

            var dAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            dAnimation.From = 0;
            dAnimation.To = 1;
            dAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            Utilities.StartAnimation(dAnimation, outBorder.Name, new PropertyPath(Border.OpacityProperty), this);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            rectPlay.IsEnabled = false;
            rectPlay.Visibility = Visibility.Hidden;

            icnEdit.IsEnabled = false;
            icnEdit.Visibility = Visibility.Hidden;

            var dAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            dAnimation.From = 1;
            dAnimation.To = 0;
            dAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            Utilities.StartAnimation(dAnimation, outBorder.Name, new PropertyPath(Border.OpacityProperty), this);
        }

        private void Grid_MouseClick(object sender, MouseButtonEventArgs e)
        {
            GlobalVariables.curSelectedItem = item;
            mWindow.ChangePreviewUI(false, this);
        }

        private void Initialize()
        {
            lblName.Content = item.Name;
            if (item.IconLocation != null)
            {
                var bmpImg = Utilities.BitmapImage(new Uri(item.IconLocation), 160, 90);
                if (bmpImg != null)
                {
                    imgArt.RenderSize = (new Size(100, 100));
                    imgArt.Stretch = Stretch.UniformToFill;
                    imgArt.Source = bmpImg;
                }
                else
                    UpdateImgToIcon();

            }
            else
                UpdateImgToIcon();

        }

        private void rectPlay_MouseUp(object sender, MouseButtonEventArgs e)
        {


            if (item.LaunchPara == null || item.LaunchPara == "")
            {
                ProcessManager.Start(item.Path);
            }
            else
                ProcessManager.Start(item.Path, item.LaunchPara);


        }

        private void icnEdit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GlobalVariables.curSelectedItem = item;
            mWindow.ChangePage(3);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mWindow = Window.GetWindow(this) as MainWindow;
        }

        private void UpdateImgToIcon()
        {
            var ico = System.Drawing.Icon.ExtractAssociatedIcon(item.Path);
            var bmpSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                ico.Handle, Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(100, 100));
            ico.Dispose();
            imgArt.Stretch = Stretch.Uniform;
            imgArt.Height = 50; imgArt.Width = 50;
            imgArt.Source = bmpSrc;
        }

        public void EnableOutline()
        {
            this.outlineSelection.Visibility = Visibility.Visible;
        }

        public void DisableOutline()
        {
            this.outlineSelection.Visibility = Visibility.Hidden;
        }
    }
}