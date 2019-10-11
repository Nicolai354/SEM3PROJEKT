using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using Remee.JackmanService;
using Remee.Controller;

namespace Remee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string title = "Remee Support System";
        Timer tmrStatusReset = new Timer(3000);
        List<Supporter> supporters = new SupporterController().GetSupporters();

        public MainWindow()
        {
            InitializeComponent();
            Main.NavigationService.Navigated += NavigationService_Navigated;
            tmrStatusReset.Elapsed += TmrStatusReset_Elapsed;
            cmbSupporter.ItemsSource = supporters;
            cmbSupporter.SelectedIndex = 0;
        }

        private void TmrStatusReset_Elapsed(object sender, ElapsedEventArgs e)
        {
            lblStatus.Dispatcher.Invoke(() => lblStatus.Content = "");
        }

        //Event when NavigationService has navigated (ie. to change title of MainWindow)
        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            this.Title = $"{title} - {((Page)e.Content).Title}";
        }

        private void mnuCaseCreate_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Pages.CaseCreate());
        }

        private void mnuCaseShowAll_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Pages.CaseShowAll());
        }

        public void ChangePage(Page p)
        {
            Main.NavigationService.Navigate(p);
        }

        public void SetStatus(string message)
        {
            lblStatus.Content = message;
            tmrStatusReset.Stop();
            tmrStatusReset.Start();
        }

        private void CbSupporter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CmbSupporter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SupporterController.LoggedInSupporter = (Supporter)cmbSupporter.SelectedItem;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new Pages.CaseShowMy());
        }
    }
}
