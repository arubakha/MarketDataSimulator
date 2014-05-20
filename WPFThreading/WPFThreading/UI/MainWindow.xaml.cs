using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFThreading.Presentation;
using System.Diagnostics;

namespace WPFThreading.UI
{
    public interface IMainWindow
    { 
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            //this.btnButton.Dispatcher
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ButtonItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ItemsControl_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Debug.WriteLine(sender.GetType());              //System.Windows.Controls.ItemsControl
            Debug.WriteLine(e.OriginalSource.GetType());    //System.Windows.Documents.Hyperlink
        }
    }
}
