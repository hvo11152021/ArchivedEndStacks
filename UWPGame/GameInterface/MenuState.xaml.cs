using Newtonsoft.Json;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//PROG1621 Lab 1B - Hy Vo

namespace GameInterface
{
    public sealed partial class MenuState : Page
    {
        
        public MenuState()
        {
            this.InitializeComponent();
            txtUserName.Visibility = Visibility.Collapsed;                                  //hide username and proceed button, while enable start and exit button
            btnProceed.Visibility = Visibility.Collapsed;
            lblGuide.Visibility = Visibility.Collapsed;
            btnStart.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            txtUserName.Visibility = Visibility.Visible;                                   //...and vise versa if the player start the game
            btnProceed.Visibility = Visibility.Visible;
            lblGuide.Visibility = Visibility.Visible;
            btnStart.Visibility = Visibility.Collapsed;
            btnExit.Visibility = Visibility.Collapsed;
        }

        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("See you next time!").ShowAsync();          //send a goodbye message for exit
            Application.Current.Exit();
        }

        private async void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "")
            {
                await new MessageDialog("Please enter a user name :)").ShowAsync();     //prompt the user to enter something
            }
            else if (txtUserName.Text != "" && txtUserName.Text != MainPage.playerName)
            {
                MainPage.playerName = txtUserName.Text;                                  //if any name was provided, then player name will be the input name
                this.Frame.Navigate(typeof(MainPage));                          //the name will save and the program switch screen
            }
        }

        
    }
}
