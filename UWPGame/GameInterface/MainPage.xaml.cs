using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameLibrary;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Core;
using System.Numerics;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.Foundation;
using Windows.UI.ViewManagement;

//PROG1621 Lab 1A - Hy Vo Updated

//Asteroid collision is fixed and actually works.

/*Additional assets for the game include:
    - asteroid
    - a spaceship
    - a burger shop
*/

namespace GameInterface
{
    public sealed partial class MainPage : Page
    {
        private GamePiece spaceShip;                        //player ship piece
        private GamePiece burgerPrize;                      //burger shop piece
        private GamePiece[] asteroids;                      //array of asteroids
        private List<Image> sprites = new List<Image>();    //list of image objects

        private int difficulty = 1;                         //difficult levels and scores
        private int points = 0;


        public MainPage()
        {
            this.InitializeComponent();
            var size = new Size(1923, 1083);
            ApplicationView.PreferredLaunchViewSize = size;

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            ScoreBoard();
            LoadSprites();

            //game goal
            txtGoals.Text = "You're hungry. Some crispy chicken burgers are ideal before returning to Earth.";
            txtGoals.Text += " Navigate the Kuiper belt (10 levels).";
        }

        private void LoadSprites()
        {
            spaceShip = Sprite("spaceship", 150, 100, 550);         //create ship
            burgerPrize = Sprite("space_burger", 100, 1600, 550);   //create shop

            asteroids = new GamePiece[difficulty];              //create asteroid array with size increased by difficulty
            asteroids[0] = Sprite("asteroid", 100, 800, 550);

            for (int i = 1; i < asteroids.Length; i++)          //loop through the entirety of the asteroid array
            {
                int tempX;
                int tempY;
                for (int t = 0; t < 1000000; t++)               //allow a substantial end value to locate co-ordinate in time
                {
                    Random rand = new Random();                 //generate random co-ordinates
                    tempX = rand.Next(300, 1920);               //co=ordinates within hd screen resolution, but outside player spawn
                    tempY = rand.Next(1080);
                    if (tempX % 25 == 0 &&                      //keep co-ordinates value only divisible by 25
                        tempY % 25 == 0)                        //since movement take 25 pixels
                    {
                        asteroids[i] = Sprite("asteroid", 100, tempX, tempY);   //populate asteroids on random co-ordinates on the screen
                        break;      //break out of if statement return true
                    }
                }
            }
        }

        private void ScoreBoard()       //a method to display the scoreboard. it includes difficulty level and total points
        {
            txtScoreBoard.Text = $"Level: {difficulty}\n";
            txtScoreBoard.Text += $"Points: {points}\n";
            txtScoreBoard.Text += $"+100 points for reaching Burger Shop.\n";
            txtScoreBoard.Text += $"-50 points for every asteroid impact.\n";
        }

        private GamePiece Sprite(string imgSrc, int size, int left, int top)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{imgSrc}.png"));
            img.Width = size;
            img.Height = size;
            img.Name = $"img{imgSrc}";                          //default codes
            img.Margin = new Thickness(left, top, 0, 0);
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.CenterPoint = new Vector3(0.5F, 0.5F, 0);           //failed attempted at search collision based on co-ordinates

            sprites.Add(img);                       //add each image object to list of images
            gridMain.Children.Add(img);

            return new GamePiece(img, imgSrc);
        }

        private async void CoreWindow_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                spaceShip.Move(e.VirtualKey);

                //ship location as a full rectangle
                var shipLocation = new Rect(spaceShip.Location.Left, spaceShip.Location.Top, 100, 100);
                //shop location as a full rectangle (improved collision detection)
                var shopLocation = new Rect(burgerPrize.Location.Left - 75, burgerPrize.Location.Top + 25, 100, 100);
                
                if (shipLocation == shopLocation)
                {
                    await new MessageDialog($"Level {difficulty} Completed!").ShowAsync();  //alert message for shop contact

                    foreach (var spr in sprites)
                    {
                        gridMain.Children.Remove(spr);      //remove all game pieces
                    }

                    difficulty++;                           //increase difficulty by 1 and increase the amount of asteroids
                    points += 100;                          //add points
                    ScoreBoard();                           //update the scoreboard
                    LoadSprites();                          //reload game pieces

                    txtGoals.Text = "";                     //clear game goal text, since user have read it already in lvl 1
                }

                for (int i = 0; i < asteroids.Length; i++)  //loop through array to check every asteroid
                {
                    if (shipLocation == new Rect(
                        asteroids[i].Location.Left - 75,
                        asteroids[i].Location.Top, 100, 100))    //then check collisions
                    {
                        points -= 50;       //then remove points and update the scoreboard
                        ScoreBoard();
                    }
                }

                if (points < 0)       //check if the player pass 10 levels
                {
                    txtScoreBoard.Text = "";    //then clear the score board and alert results one last time
                    await new MessageDialog($"Mission Failed. Your ship has been destroyed.\nPlease try again :)").ShowAsync();
                    Application.Current.Exit(); //finally, close the application
                }

                if (difficulty >= 11)       //check if the player pass 10 levels
                {
                    txtScoreBoard.Text = "";    //then clear the score board and alert results one last time
                    await new MessageDialog($"Level {difficulty - 1} Completed!\nYou have escaped the Keiper belt!\nYour best score is {points}!\nThank you for playing.").ShowAsync();
                    Application.Current.Exit(); //finally, close the application
                }
            }
            catch(Exception ex)     //catch system exceptions
            {
                await new MessageDialog($"Your game ran into an error.\n{ex}\n.Please contact the administrator.").ShowAsync();
                Application.Current.Exit(); //finally, close the application
            }

        }
    }

}
