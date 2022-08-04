using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SolaraPayroll
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Employee> empList = Data.GetDataRecords();
        private List<Employee> payrollList = new List<Employee>();
        private Employee selectedPerson;
        private List<string> prOutput = new List<string>();

        List<string> tempEmpList = new List<string>();

        private string EmpName { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            txtFirst.IsEnabled = false;
            txtLast.IsEnabled = false;
            txtSiN.IsEnabled = false;
            dtpInputDOB.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtStreet.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtProvince.IsEnabled = false;
            txtZip.IsEnabled = false;
            txtEmail.IsEnabled = false;

            cboEmployeeType.IsEnabled = false;
            txtSalary.IsEnabled = false;
            txtHourWorked.IsEnabled = false;
            txtHourlyRate.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnAddNew.IsEnabled = false;
            btnCancel.IsEnabled = false;

            btnNewpayroll.IsEnabled = true;
            dtpkPayDate.IsEnabled = false;
            btnPRSubmit.IsEnabled = false;

            btnAddNew.Visibility = Visibility.Collapsed;

            cboEmpType.ItemsSource = new string[] { "Hourly", "Salary", "Software Developer", "Supply Manager" };
            lvEmpList.Items.Clear();
            lvEmpList.ItemsSource = empList;
            foreach (Employee emp in empList)
            {
                tempEmpList.Add(emp.FirstName);
            }

            lvStatements.ItemsSource = payrollList;

            cboFilterOptions.ItemsSource = new string[]
            {
                "Employee Types",
                "Activities",
                "Seniority",
                "Highest Pay",
                "Highest Pay Type",
                "Average Pay",
                "Highest Bonus"
            };

        }

        private async void cboEmpType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboEmpType.SelectedItem.ToString() == "Hourly")
                {
                    lvEmpList.ItemsSource = empList.OfType<Hourly>();
                }
                if (cboEmpType.SelectedItem.ToString() == "Salary")
                {
                    lvEmpList.ItemsSource = empList.OfType<Salary>();
                }
                if (cboEmpType.SelectedItem.ToString() == "Software Developer")
                {
                    lvEmpList.ItemsSource = empList.OfType<SoftwareDev>();
                }
                if (cboEmpType.SelectedItem.ToString() == "Supply Manager")
                {
                    lvEmpList.ItemsSource = empList.OfType<SupplyManager>();
                }

            }
            catch (Exception ex)
            {
                var message = new MessageDialog(ex.Message);
                await message.ShowAsync();
            }
        }

        private async void tbxEmpName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tbxEmpName.Text != "")
                {
                    lvEmpList.ItemsSource = empList.Where(p => p.FirstName == tbxEmpName.Text);
                }
                else
                {
                    lvEmpList.ItemsSource = empList;
                }
            }
            catch (Exception ex)
            {
                var message = new MessageDialog(ex.Message);
                await message.ShowAsync();
            }
        }

        private async void dtpkHiredDate_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            try
            {
                lvEmpList.ItemsSource = empList.Where(p => p.HireDate.Date.Date == dtpkHiredDate.Date.Date);
            }
            catch (Exception ex)
            {
                var message = new MessageDialog(ex.Message);
                await message.ShowAsync();
            }
        }
    }
}
