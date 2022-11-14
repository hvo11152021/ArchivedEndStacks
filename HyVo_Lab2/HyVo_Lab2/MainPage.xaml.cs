using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;

//Name: Hy Vo
//PROG1621: Lab 2

namespace HyVo_Lab2
{
    public sealed partial class MainPage : Page
    {
        //the only queue object in the main page
        private PriorityQueue newGoals = new PriorityQueue();
        //message alert for error handling
        private MessageDialog alertDuplicate;

        public MainPage()
        {
            this.InitializeComponent();

            //set date priority combo box
            cboAddTimeline.ItemsSource = Enum.GetNames(typeof(Timeline)); ;
            //hide remove button until later appearance
            btnRemove.Visibility = Visibility.Collapsed;
        }

        private void btnAddGoal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //add goal and priority to the queue
                newGoals.Enqueue(new Goal(txtGoals.Text, Enum.Parse<Timeline>((string)cboAddTimeline.SelectedValue)));
            }
            //handle exception from the enqueue method
            catch (Exception ex)
            {
                //alert error message
                alertDuplicate = new MessageDialog(ex.Message);
                alertDuplicate.ShowAsync();
            }
            
            //update the list view
            Refresh();
        }

        //button sample insert test data
        private void btnSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newGoals.Enqueue(new Goal("Watch movie", Enum.Parse<Timeline>("This_Week")));
                newGoals.Enqueue(new Goal("Vacuum rug", Enum.Parse<Timeline>("This_Week")));
                newGoals.Enqueue(new Goal("Put out fire", Enum.Parse<Timeline>("Today")));
                newGoals.Enqueue(new Goal("Buy groceries", Enum.Parse<Timeline>("Tomorrow")));
                newGoals.Enqueue(new Goal("Dust bookshelf", Enum.Parse<Timeline>("This_Month")));
                newGoals.Enqueue(new Goal("Finish Lab", Enum.Parse<Timeline>("Tomorrow")));
                newGoals.Enqueue(new Goal("Visit Grandma", Enum.Parse<Timeline>("This_Week")));
                newGoals.Enqueue(new Goal("Weed garden", Enum.Parse<Timeline>("This_Month")));
            }
            catch (Exception ex)
            {
                alertDuplicate = new MessageDialog(ex.Message);
                alertDuplicate.ShowAsync();
            }

            Refresh();
        }

        //remove top goal with dequeue
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            newGoals.Dequeue();
            Refresh();
        }

        //update the list view
        private void Refresh()
        {
            //clear the list
            lsvOutput.Items.Clear();
            //loop through the enumerator object
            using (var loopGoals = newGoals.GetEnumerator())
            {
                //output each goal while reading through the object
                while (loopGoals.MoveNext())
                {
                    lsvOutput.Items.Add(loopGoals.Current);
                }
            }
            //hide remove button if there's no goal and show it vise versa
            btnRemove.Visibility = newGoals.Count <= 0? Visibility.Collapsed : Visibility.Visible;
            //update number of goals using count
            txtCounter.Text = newGoals.Count.ToString();
        }

        //update the list bu showing top result
        private void btnPeak_Click(object sender, RoutedEventArgs e)
        {
            //switch type of list view using button content
            switch (btnSample.Content)
            {
                case "Peak":
                    lsvOutput.Items.Clear();
                    lsvOutput.Items.Add(newGoals.Peak(newGoals));
                    
                    btnRemove.Visibility = newGoals.Count <= 0 ? Visibility.Collapsed : Visibility.Visible;
                    txtCounter.Text = newGoals.Count.ToString();
                    btnSample.Content = "Back";
                    break;
                case "Back":
                    Refresh();
                    btnSample.Content = "Peak";
                    break;
            }
        }
    }
}
