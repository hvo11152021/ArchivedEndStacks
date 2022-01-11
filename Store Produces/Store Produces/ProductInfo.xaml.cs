using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store_Produces
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductInfo : Page
    {
        Product shopItem;

        public ProductInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //event
            base.OnNavigatedTo(e);

            string parameterString = e.Parameter.ToString();
            //deserialized the Json string from MainPage with data of the product
            shopItem = JsonSerializer.Deserialize<Product>(parameterString);

            //the text is display for the user to see
            txtProductName.Text = shopItem.ProductName;
            txtProductPrice.Text = shopItem.Price.ToString();
            txtDateCreated.Text = shopItem.Date.ToString();
            txtProductQuantity.Text = shopItem.Quantity.ToString();
            txtProductWeight.Text = shopItem.Weight.ToString();
            txtProductDescription.Text = shopItem.Description;
        }

        private void btnRestock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtProductQuantity.Text = shopItem.Restock().ToString();
            }
            catch (Exception ex)
            {
                MessageDialog message = new MessageDialog(ex.Message);
                message.ShowAsync();
            }
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtProductQuantity.Text = shopItem.Sell().ToString();
            }
            catch (Exception ex)
            {
                MessageDialog message = new MessageDialog(ex.Message);
                message.ShowAsync();
            }
        }

        private void btnDiscontinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                txtProductQuantity.Text = shopItem.Discontinue().ToString();
            }
            catch (Exception ex)
            {
                MessageDialog message = new MessageDialog(ex.Message);
                message.ShowAsync();
            }
        }
    }
}
