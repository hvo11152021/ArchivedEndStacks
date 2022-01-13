using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lab3PartA_HyVo;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab3PartB_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3B
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly List<Employee> employees;
        private Employee person;
        private Hourly hourly;
        private Supervisor salary;
        private EngineeringManager emMonthly;
        private GlobalSupplyManager gsmSalary;
        private int indexNum;

        public MainPage()
        {
            this.InitializeComponent();

            employees = Data.GetDataRecords(); //retrieve data from part A library
            DisableEditingArea(); //disable employee edit area
            DisplayList(); //load employee list into a combobox
        }

        private void cboName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            person = employees[cboName.SelectedIndex]; //retrieve information of employees based on index of combobox
            
            //set employee type based on index
            hourly = person is Hourly ? (Hourly)person : null;
            salary = person is Supervisor ? (Supervisor)person : null;
            emMonthly = person is EngineeringManager ? (EngineeringManager)person : null;
            gsmSalary = person is GlobalSupplyManager ? (GlobalSupplyManager)person : null;

            indexNum = cboName.Items.IndexOf(cboName.SelectedItem.ToString()); //find index of employee
            txtInfo.Text = person.ToString(); //display employee information

        }

        private void txtEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            indexNum = employees.FindIndex(x => x.First == txtEmployee.Text); //retrieve index of the employee
            //if input match first name of an employee, display that person
            txtInfo.Text = employees.Exists(x => x.First == txtEmployee.Text) ? employees[indexNum].FullName : "No Employee Found";
        }

        private void dpBirthDate_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            //turn chosen date in datepicker to datetime
            DateTime inputDate = dtpBirthDate.SelectedDate != null ? new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day) : DateTime.Parse(null);
            //return index of an employee if their date of birth match input date
            indexNum = employees.FindIndex(x => x.DateOfBirth.ToShortDateString() == inputDate.ToShortDateString());
            //display employee if their index was found
            txtInfo.Text = indexNum != -1 ? employees[indexNum].FullName : "No Employee Found";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //disable textboxes based on employee type
            if (person is Hourly)
            {
                txtSalary.IsEnabled = false;
                txtMontlyRate.IsEnabled = false;
            }
            if (person is Supervisor)
            {
                txtHourWorked.IsEnabled = false;
                txtHourlyRate.IsEnabled = false;
                txtMontlyRate.IsEnabled = false;
            }
            if (person is EngineeringManager)
            {
                txtHourWorked.IsEnabled = false;
                txtHourlyRate.IsEnabled = false;
            }
            if (person is GlobalSupplyManager)
            {
                txtHourWorked.IsEnabled = false;
                txtHourlyRate.IsEnabled = false;
                txtMontlyRate.IsEnabled = false;
            }

            //display employee's information
            txtFirst.Text = employees[indexNum].First;
            txtLast.Text = employees[indexNum].Last;
            txtSiN.Text = employees[indexNum].SINumber;
            txtDoB.Text = employees[indexNum].DateOfBirth.ToString();
            txtPhone.Text = employees[indexNum].Phone;
            txtAddress.Text = employees[indexNum].Address;
            txtEmail.Text = employees[indexNum].Email;

            txtHourWorked.Text = person is Hourly ? hourly.HourWorked.ToString() : "";
            txtHourlyRate.Text = person is Hourly ? hourly.HourlyRate.ToString() : "";
            txtSalary.Text = person is Supervisor ? salary.SpvrSalary.ToString() : "";
            txtMontlyRate.Text = person is EngineeringManager ? emMonthly.MonthlyWage.ToString() : "";
            txtSalary.Text = person is GlobalSupplyManager ? gsmSalary.BsmSalary.ToString() : "";

            ClearSearch(); //clear search textbox
            DisableSearch(); //disable combobox, search textbox and datepicker
            EnableEditingArea(); //enable employee edit area
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearSearch(); //clear search textbox
            DisableSearch(); //disable combobox, search textbox and datepicker
            EnableEditingArea(); //enable employee edit area
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //insert information based on employee type
            if (person is Hourly)
            {
                employees.Add(new Hourly(txtSiN.Text, txtFirst.Text, txtLast.Text, DateTime.Now, DateTime.Parse(txtDoB.Text), txtPhone.Text, txtAddress.Text, txtEmail.Text, int.Parse(txtHourWorked.Text), decimal.Parse(txtHourlyRate.Text)));
            }
            if (person is Supervisor)
            {
                employees.Add(new Supervisor(txtSiN.Text, txtFirst.Text, txtLast.Text, DateTime.Now, DateTime.Parse(txtDoB.Text), txtPhone.Text, txtAddress.Text, txtEmail.Text, decimal.Parse(txtSalary.Text)));
            }
            if (person is EngineeringManager)
            {
                employees.Add(new EngineeringManager(txtSiN.Text, txtFirst.Text, txtLast.Text, DateTime.Now, DateTime.Parse(txtDoB.Text), txtPhone.Text, txtAddress.Text, txtEmail.Text, decimal.Parse(txtSalary.Text), decimal.Parse(txtMontlyRate.Text)));
            }
            if (person is GlobalSupplyManager)
            {
                employees.Add(new GlobalSupplyManager(txtSiN.Text, txtFirst.Text, txtLast.Text, DateTime.Now, DateTime.Parse(txtDoB.Text), txtPhone.Text, txtAddress.Text, txtEmail.Text, decimal.Parse(txtSalary.Text)));
            }

            ClearEditingArea(); //clear employee edit area
            DisableEditingArea(); //disable all boxes and buttons in employee edit area
            DisplayList(); //refresh combobox
            EnableSearch(); //enable search area
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //update employee information based on their type
            if (person is Hourly)
            {
                UpdateEmployee();
            }
            if (person is Supervisor)
            {
                UpdateEmployee();
            }
            if (person is EngineeringManager)
            {
                UpdateEmployee();
            }
            if (person is GlobalSupplyManager)
            {
                UpdateEmployee();
            }
            ClearEditingArea(); //clear employee edit area
            DisableEditingArea(); //disable all boxes and buttons in employee edit area
            EnableSearch(); //enable search area

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditingArea(); //clear employee edit area
            DisableEditingArea(); //disable all boxes and buttons in employee edit area
            EnableSearch(); //enable search area
        }

        public void DisplayList()
        {
            foreach (Employee emp in employees)
            {
                cboName.Items.Add(emp.FullName);
            }
        }

        public void DisableSearch()
        {
            cboName.IsEnabled = false;
            txtEmployee.IsEnabled = false;
            dtpBirthDate.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnAdd.IsEnabled = false;
        }

        public void EnableSearch()
        {
            cboName.IsEnabled = true;
            txtEmployee.IsEnabled = true;
            dtpBirthDate.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnAdd.IsEnabled = true;
        }

        public void ClearSearch()
        {
            txtEmployee.Text = string.Empty;
        }

        public void DisableEditingArea()
        {
            txtFirst.IsEnabled = false;
            txtLast.IsEnabled = false;
            txtSiN.IsEnabled = false;
            txtDoB.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtHourWorked.IsEnabled = false;
            txtHourlyRate.IsEnabled = false;
            txtSalary.IsEnabled = false;
            txtMontlyRate.IsEnabled = false;
            btnInsert.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnCancel.IsEnabled = false;
        }

        public void EnableEditingArea()
        {
            txtFirst.IsEnabled = true;
            txtLast.IsEnabled = true;
            txtSiN.IsEnabled = true;
            txtDoB.IsEnabled = true;
            txtPhone.IsEnabled = true;
            txtAddress.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtHourWorked.IsEnabled = true;
            txtHourlyRate.IsEnabled = true;
            txtSalary.IsEnabled = true;
            txtMontlyRate.IsEnabled = true;
            btnInsert.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            btnCancel.IsEnabled = true;
        }

        public void ClearEditingArea()
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            txtSiN.Text = string.Empty;
            txtDoB.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtHourWorked.Text = string.Empty;
            txtHourlyRate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtMontlyRate.Text = string.Empty;
        }

        public void UpdateEmployee()
        {
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.First = txtFirst.Text);
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.Last = txtLast.Text);
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.SINumber = txtSiN.Text);
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.DateOfBirth = DateTime.Parse(txtDoB.Text));
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.Phone = txtPhone.Text);
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.Address = txtAddress.Text);
            employees.Where(w => w.SINumber == employees[indexNum].SINumber).ToList().ForEach(s => s.Email = txtEmail.Text);
        }

    }
}
