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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestCode
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 2
    /// </summary>
    
    enum Status
    {
        Honours,
        Passing,
        Failing
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //Testig Objects
            Person professor = new Person(1234567, "Cliff Patrick");
            lblOutput.Text = professor.ToString();

            lblOutput.Text += $"\n\nPerson Property Test: " +
                              $"\nName: {professor.PersonName}" +
                              $"\nID: {professor.PersonID}";

            Course programResult = new Course();
            lblOutput.Text += $"\n\nCourse Structure (Default Constructor): " +
                              $"\nCourse Code: {programResult.CourseCode}" +
                              $"\nCourse Grade: {programResult.CourseGrade}";

            Course anotherResult = new Course("PROG1901", 80.20);
            lblOutput.Text += $"\n\nCourse Structure (Two Parameter Constructor): " +
                              $"\nCourse Code: {programResult.CourseCode}" +
                              $"\nCourse Grade: {programResult.CourseGrade}";

            Student undergrads = new Student(Status.Passing);
            lblOutput.Text += "\n\n" + undergrads.ToString();

            undergrads[0] = new Course("PROG1111", 77.70);
            undergrads[1] = new Course("PROG2222", 73.30);
            undergrads[2] = new Course("PROG3333", 74.40);
            undergrads[3] = new Course("PROG4444", 79.90);
            undergrads[4] = new Course("PROG5555", 62.20);
            undergrads[5] = new Course("PROG6666", 69.90);

            lblOutput.Text += "\n\n" + "Student Indexer Test";

            //int i = 0;
            //while (i < undergrads.Length){
            //    lblOutput.Text += $"\n{undergrads[i]}";
            //}


        }
    }
}
