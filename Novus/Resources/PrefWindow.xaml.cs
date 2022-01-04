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
using System.Windows.Shapes;

namespace Novus.Resources
{
    /// <summary>
    /// Interaction logic for PrefWindow.xaml
    /// </summary>
    public partial class PrefWindow : Window
    {
        private Classes.Preference pref;
        public PrefWindow(Classes.Preference _pref)
        {
            pref = _pref;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (pref.EnableLog == "true")
                chkBoxLogging.IsChecked = true;
            else
                chkBoxLogging.IsChecked = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkBoxLogging.IsChecked == true)
                pref.EnableLog = "true";
            else
                pref.EnableLog = "false";

            if (XmlParser.Serialize(pref))
            {
                lblConfirmText.IsEnabled = true;
                lblConfirmText.Visibility = Visibility.Visible;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
