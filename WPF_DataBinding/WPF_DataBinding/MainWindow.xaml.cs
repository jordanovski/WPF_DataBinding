using System;
using System.Linq;
using System.Windows;
using WPF_DataBinding.DataModel;

namespace WPF_DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ZzaEntities dataContext = new ZzaEntities();
        Customer firstCustomer;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            firstCustomer = dataContext.Customer.FirstOrDefault();
            lblCustolerId.Text = firstCustomer.Id.ToString();
            txtCustomerLastName.Text = firstCustomer.LastName;

            var orderDates = dataContext.Order.Where(w => w.CustomerId == firstCustomer.Id).Select(s => s.OrderDate).ToList();
            OrdersList.ItemsSource = orderDates;
        }

        private void OnOrderSelected(object sender, RoutedEventArgs e)
        {
            var selectedOrder = dataContext.Order.Include("OrderItem").Where(w => w.OrderDate == (DateTime)OrdersList.SelectedItem && w.CustomerId == firstCustomer.Id).FirstOrDefault();
            OrderItemsDataGrid.ItemsSource = selectedOrder.OrderItem;
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            firstCustomer.LastName = txtCustomerLastName.Text;

            dataContext.SaveChanges();
        }

    }
}
