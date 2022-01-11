using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Store_Produces
{
    class Product
    {   
        //private fields for information about the product
        private string productName;
        private double productPrice;
        private DateTime manufactureDate;
        private int productQuantity;
        private double productWeight;
        private string productDescription;
        private int qtyChange;
        private double profitAmount;

        //properties
        public string ProductName   
        {   //empty value is not allow
            get { return productName; }
            set { if (value != "") productName = value; }
        }
        public double Price
        {   //price input
            get { return productPrice; }
            set { productPrice = value; }
        }

        public DateTime Date
        {   //date input
            get { return manufactureDate; }
            set { manufactureDate = value; }
        }

        public int Quantity
        {   //amount value can't be less than 0
            get { return productQuantity; }
            set { if (value >= 0) productQuantity = value; }
        }

        public double Weight
        {   //weight input of the product
            get { return productWeight; }
            set { productWeight = value; }
        }

        public string Description
        {   //a few words about the item
            get { return productDescription; }
            set { productDescription = value; }
        }

        public int QtyChange
        {   //amount of item that's about to change
            get { return qtyChange; }
            set { qtyChange = value; }
        }

        public double AmountOfProfit
        {   //the profit after each sell
            get { return profitAmount; }
            set { profitAmount = value; }
        }

        //default constructors
        public Product() //this constructor set the default value for these fields
        {
            productName = "Unknown";
            productPrice = 0.00;
            manufactureDate = DateTime.MinValue;
            productQuantity = 0;
            productWeight = 0.00;
            productDescription = "Empty";
        }

        //overload constructors
        public Product(string n, double p, DateTime md, int q, double w, string d)
        {
            productName = n;
            productPrice = p;
            manufactureDate = md;
            productQuantity = q;
            productWeight = w;
            productDescription = d;
        }

        //instance methods
        public int Restock()    //this method stock more product
        {
            if (productQuantity >= 0)
                productQuantity++;
            return productQuantity;
        }

        public double Sell()    //this method sell the product
        {
            if (productQuantity == 0)
                throw new Exception($"There is no {productName} left!");
            else
                productQuantity--;
            return productQuantity;
        }

        public int Discontinue()    //this method clear product quantity
        {
            if (productQuantity == 0)
                throw new Exception($"There is no {productName} left!");
            else
                productQuantity = 0;
            return productQuantity;
        }
    }
}
