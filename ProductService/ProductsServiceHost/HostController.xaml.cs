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
using System.ServiceModel;
using Products;

namespace ProductsServiceHost
{
    /// <summary>
    /// Interaction logic for HostController.xaml
    /// </summary>
    public partial class HostController : Window
    {
        private ServiceHost productsServiceHost;

        public HostController()
        {
            InitializeComponent();
        }

        private void handleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost = new ServiceHost(typeof(ProductsServiceImpl));
                productsServiceHost.Open();
                stop.IsEnabled = true;
                start.IsEnabled = false;
                status.Text = "Service Running";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost.Close();
                stop.IsEnabled = false;
                start.IsEnabled = true;
                status.Text = "Service Stopped";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
    }
}
