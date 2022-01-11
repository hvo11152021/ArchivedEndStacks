using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Text.Json.Serialization;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Store_Produces
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnSaveInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //input variables
                string name = txtName.Text;
                double price = double.Parse(txtPrice.Text);
                DateTime date = dtpManufactureDate.Date.DateTime;
                int quantity = int.Parse(txtQuantity.Text);
                double weight = double.Parse(txtWeight.Text);
                string description = txtDescription.Text;

                //validating statements
                if (string.IsNullOrEmpty(name))
                {
                    MessageDialog message = new MessageDialog("Name can't be empty! Please enter a name");
                    message.ShowAsync();
                }
                else if (txtPrice.Text == "0")
                {
                    MessageDialog message = new MessageDialog("Price can't be empty! Please enter a price");
                    message.ShowAsync();
                }
                else if (txtQuantity.Text == "0")
                {
                    MessageDialog message = new MessageDialog("Quantity can't be empty! Please enter a quantity");
                    message.ShowAsync();
                }
                else if (txtWeight.Text == "0")
                {
                    MessageDialog message = new MessageDialog("Weight can't be empty! Please enter a weight");
                    message.ShowAsync();
                }
                else if (string.IsNullOrEmpty(description))
                {
                    MessageDialog message = new MessageDialog("A product should have a short description");
                    message.ShowAsync();
                }
                else
                {   //Json Serializer to convert Product to a string and pass it to ProductInfo
                    Product userItem = new Product(name, price, date, quantity, weight, description);

                    string productString = JsonSerializer.Serialize<Product>(userItem);
                    this.Frame.Navigate(typeof(ProductInfo), productString);
                }
            }
            catch (FormatException ex)
            {   //a dialog for invalid string
                MessageDialog message = new MessageDialog("Please enter a number");
                message.ShowAsync();
            }
            catch (OverflowException ex)
            {   //a dialog for value greater than int
                MessageDialog message = new MessageDialog("Your number is too big");
                message.ShowAsync();
            }
            catch
            {   //a dialog if the program crash unknowingly
                MessageDialog message = new MessageDialog("Program Crashed");
                message.ShowAsync();
            }
            
        }

    }
}
