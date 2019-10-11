using Remee.Controller;
using Remee.JackmanService;
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

namespace Remee.Pages
{
    /// <summary>
    /// Interaction logic for CaseShowMy.xaml
    /// </summary>
    public partial class CaseShowMy : Page
    {
        private List<Case> cases;
        RemeeSupportClient client = new RemeeSupportClient();
        public CaseShowMy()
        {
            InitializeComponent();
            cases = GetCasesForSelectedSupporter();
            this.DataContext = cases;
        }

        private void DgCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Case c = (Case)dgCases.SelectedItem;
            if (c != null)
                NavigationService.Navigate(new CaseDetails(c.Id));
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cases != null && txtSearch.Text != "Søg")
            {
                var filtered = cases.Where(cases =>
                    cases.Customer.Name.ToLower().StartsWith(txtSearch.Text.ToLower())
                    || cases.Id.ToString().StartsWith(txtSearch.Text)
                    || cases.Description.ToLower().StartsWith(txtSearch.Text.ToLower()));

                dgCases.ItemsSource = filtered;
            }
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Søg")
                txtSearch.Text = "";
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
                txtSearch.Text = "Søg";
        }

        public List<Case> GetCasesForSelectedSupporter()
        {
            List<Case> tempCases = new List<Case>();
            foreach (Case c in client.GetCases().ToList())
            {
                if(c.Supporter != null)
                {
                    if (SupporterController.LoggedInSupporter.Id == c.Supporter.Id)
                    {
                        tempCases.Add(c);
                    }
                }
            }

            return tempCases;
        }
    }
}
