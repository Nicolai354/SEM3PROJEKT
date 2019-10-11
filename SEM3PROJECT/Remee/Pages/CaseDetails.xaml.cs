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
    /// Interaction logic for CaseDetails.xaml
    /// </summary>
    public partial class CaseDetails : Page
    {
        RemeeSupportClient client = new RemeeSupportClient();
        CaseController caseCtrl = new CaseController();
        CommentController commentCtrl = new CommentController();
        Case @case;

        StatusController statusCtrl = new StatusController();

        public CaseDetails(int id)
        {
            InitializeComponent();
            @case = caseCtrl.GetCase(id);

            this.DataContext = @case;

            List<Category> categories = new CategoryController().GetCategories();
            @case.Category = categories.Find(c => c.Id == @case.Category.Id);
            cbCategory.ItemsSource = categories;

            List<Status> statuses = new StatusController().GetStatuses();
            @case.Status = statuses.Find(s => s.Id == @case.Status.Id);
            cbStatus.ItemsSource = statuses;

            FillComments();

        }

        private void FillComments()
        {
            List<Comment> comments = commentCtrl.GetComments(@case.Id);
            dgComments.ItemsSource = comments;
        }

        private void BtnCaseTake_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caseCtrl.CaseTake(@case);
                NavigationService.Navigate(new CaseDetails(@case.Id));
            }
            catch (Exception)
            {
                MessageBox.Show("Noget gik galt");
            }
        }

        private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Subcategory> subcategories = new SubcategoryController().GetSubcategories((Category)cbCategory.SelectedItem);

            if (@case.Subcategory != null)
                @case.Subcategory = subcategories.Find(s => s.Id == @case.Subcategory?.Id);

            cbSubcategory.ItemsSource = subcategories;
        }

        private void SetEditable(bool editable)
        {
            cbCategory.IsEnabled = editable;
            cbSubcategory.IsEnabled = editable;
            txtOperatingSystem.IsReadOnly = !editable;
            txtPriority.IsReadOnly = !editable;
            txtDescription.IsReadOnly = !editable;
            btnCaseEdit.Visibility = editable ? Visibility.Collapsed : Visibility.Visible;
            btnCaseSave.Visibility = editable ? Visibility.Visible : Visibility.Collapsed;
            btnCaseEditCancel.Visibility = editable ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnCaseEdit_Click(object sender, RoutedEventArgs e)
        {
            SetEditable(true);
        }

        private void BtnCaseSave_Click(object sender, RoutedEventArgs e)
        {
            SetEditable(false);

            try
            {
                caseCtrl.CaseEdit(@case);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetEditable(true);
            }
        }

        private void BtnCaseEditCancel_Click(object sender, RoutedEventArgs e)
        {
            @case = caseCtrl.GetCase(@case.Id);
            this.DataContext = @case;

            SetEditable(false);
        }

        
        private void CbSubcategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (@case.Status != cbStatus.SelectedItem)
            {
                try
                {
                    caseCtrl.CaseChangeStatus(@case, (int)cbStatus.SelectedValue, 1);
                    ((MainWindow)Application.Current.MainWindow).SetStatus("Status er ændret");
                }
                catch (Exception ex)
                {
                    ((MainWindow)Application.Current.MainWindow).SetStatus(ex.Message);
                }
            }
        }

        private void BtnCreateComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                commentCtrl.CreateComment(@case.Id, SupporterController.LoggedInSupporter, txtNewComment.Text);

                FillComments();

                txtNewComment.Clear();
            }
            catch (Exception ex)
            {
                ((MainWindow)Application.Current.MainWindow).SetStatus(ex.Message);
            }
        }
    }
}
