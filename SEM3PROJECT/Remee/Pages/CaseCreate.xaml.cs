using System;
using System.Collections.Generic;
using System.Globalization;
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
using Remee.JackmanService;
using Remee.Model.Exceptions;

namespace Remee.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaseCreate : Page
    {
        RemeeSupportClient client = new RemeeSupportClient();
        Controller.CaseController caseController = new Controller.CaseController();

        public CaseCreate()
        {
            InitializeComponent();
            GetCategories();
        }

        public void GetCategories()
        {
            Category[] categorielist = client.GetCategories();
            
            foreach (Category category in categorielist)
                cbCategory.Items.Add(category);
        }

        private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear all items in the combobox
            cbSubcategory.Items.Clear();
            
            //Gets the selected Category and then show the subcategories
            Subcategory[] subcategorylist = client.GetSubcategories(((Category)cbCategory.SelectedItem).Id);

            // Add the items to the combobox
            foreach (Subcategory subcategory in subcategorylist)
                cbSubcategory.Items.Add(subcategory);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string priority = txtPriority.Text;
                Case c = caseController.CreateCase(txtOperatingSystem.Text, Int32.Parse(priority), txtDescription.Text, (Category)cbCategory.SelectedItem,(Subcategory)cbSubcategory.SelectedItem);
                NavigationService.Navigate(new CaseDetails(c.Id));
            }
            catch(Exception ex)
            {
                ((MainWindow)Application.Current.MainWindow).SetStatus(ex.Message);
            }
        }

        protected class EmptyInputValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                return new ValidationResult(value.ToString() != "", null);
            }
        }
    }
}