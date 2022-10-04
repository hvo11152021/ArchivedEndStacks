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
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml.Documents;
using System.Linq;

//PROG1621 Lab 1B - Hy Vo

namespace GameInterface
{
    public sealed partial class MainPage : Page
    {
        public static string playerName = "Mercenary";      //a static string variable for player name. useful later for highscore records

        private GamePiece spaceShip;                        //player ship piece
        private GamePiece burgerPrize;                      //burger shop piece
        private GamePiece[] asteroids;                      //array of asteroids
        private List<Image> sprites = new List<Image>();    //list of image objects
        private GamePiece bullet;

        private List<Image> shellCases = new List<Image>(); //an extra list for clean removal of all bullet shells

        private int difficulty = 1;                         //difficult levels and scores
        private int points = 0;
        private int highestPoints = 0;

        public MainPage()
        {
            this.InitializeComponent();
            var size = new Size(1920, 1080);
            ApplicationView.PreferredLaunchViewSize = size;

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            LoadGame();
        }

        private void LoadGame()
        {
            ScoreBoard();
            LoadSprites();

            //game goal
            txtGoals.Text = "You're hungry. Some crispy chicken burgers are ideal before returning to Earth.";
            txtGoals.Text += " Navigate the Kuiper belt (10 levels).";

            recEndMenu.Visibility = Visibility.Collapsed;
            txtReplayQuestion.Visibility = Visibility.Collapsed;
            btnYes.Visibility = Visibility.Collapsed;
            btnNo.Visibility = Visibility.Collapsed;
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
            txtScoreBoard.Text = $"Player: {playerName}\n";
            txtScoreBoard.Text = $"Level: {difficulty}\n";
            txtScoreBoard.Text += $"Current Points: {points}\n";
            txtScoreBoard.Text += $"Highest Points: {highestPoints}\n";
            txtScoreBoard.Text += $"+100 points for reaching Burger Shop.\n";
            txtScoreBoard.Text += $"-50 points for every asteroid impact.\n";
            txtScoreBoard.Text += $"+25 points for every asteroid destroyed.\n";
        }

        private GamePiece Sprite(string imgSrc, int size, double left, double top)
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
            
            if (imgSrc == "dot")
                shellCases.Add(img);                //add bullets to its seperated list
                                                    //for ease of remove later
            gridMain.Children.Add(img);

            return new GamePiece(img, imgSrc);
        }

        private async void CoreWindow_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                spaceShip.Move(e.VirtualKey);

                var bulletLocation = new Rect();

                if (e.VirtualKey is VirtualKey.Space)       //new logic to generate bullet
                {
                    bullet = Sprite("dot", 10, spaceShip.Location.Left + 125, spaceShip.Location.Top + 25);
                    bullet.Fly(e.VirtualKey);               //use fly method for bullet generation
                    //save bullet location
                    bulletLocation = new Rect(bullet.Location.Left - 125, bullet.Location.Top - 25, 100, 100);
                }

                //ship location as a full rectangle
                var shipLocation = new Rect(spaceShip.Location.Left, spaceShip.Location.Top, 100, 100);
                //shop location as a full rectangle (improved collision detection)
                var shopLocation = new Rect(burgerPrize.Location.Left - 75, burgerPrize.Location.Top + 25, 100, 100);

                //var astObj = new Rect(asteroids[0].Location.Left - 75, asteroids[0].Location.Top, 100, 100);

                //new points variables for more accurate collisions
                var shipCoordPoint = new Point(shipLocation.Left, shipLocation.Top);
                var bulletCoord = new Point(bulletLocation.Left, bulletLocation.Top);

                if (shopLocation.Contains(shipCoordPoint) == true)
                {
                    await new MessageDialog($"Level {difficulty} Completed!").ShowAsync();  //alert message for shop contact

                    foreach (var spr in sprites)
                    {
                        gridMain.Children.Remove(spr);      //remove all game pieces
                    }

                    difficulty++;                           //increase difficulty by 1 and increase the amount of asteroids
                    points += 100;                          //add points
                    highestPoints += 100;                   //add high points
                    ScoreBoard();                           //update the scoreboard
                    LoadSprites();                          //reload game pieces

                    txtGoals.Text = "";                     //clear game goal text, since user have read it already in lvl 1
                }

                //txtDebug.Text = shipLocation.ToString() + "\n";           //co-ordinate check while building
                //txtDebug.Text += shipLocation.ToString() + "\n";
                //txtDebug.Text += astObj.ToString() + "\n";
                //txtDebug.Text += bulletLocation.ToString() + "\n";

                for (int i = 0; i < asteroids.Length; i++)  //loop through array to check every asteroid
                {
                    //separate asteroid location into its own line
                    var tempLocation = new Rect(asteroids[i].Location.Left - 75, asteroids[i].Location.Top, 100, 100);
                    
                    if (tempLocation.Contains(shipCoordPoint) == true)  //check collisions using iequatable
                    {
                        points -= 50;       //then remove points and update the scoreboard
                        gridMain.Children.Remove(sprites.FirstOrDefault(a => a.Margin == new Thickness(asteroids[i].Location.Left, asteroids[i].Location.Top, 0, 0)));
                        ScoreBoard();
                    }
                    if (tempLocation.Contains(bulletCoord) == true)     //check collisions using iequatable
                    {
                        points += 25;                       //add points for destroying asteroids
                        ScoreBoard();

                        //clear the asteroid
                        gridMain.Children.Remove(sprites.FirstOrDefault(a => a.Margin == new Thickness(asteroids[i].Location.Left, asteroids[i].Location.Top, 0, 0)));
                        
                        //remove bullets after each collision
                        foreach (var shell in shellCases)
                        {
                            gridMain.Children.Remove(shell);
                        }
                    }
                }

                if (points < 0)       //check if the player pass 10 levels
                {
                    txtScoreBoard.Text = "";    //then clear the score board and alert results one last time
                    await new MessageDialog($"Mission Failed. Your ship has been destroyed.\nPlease try again :)").ShowAsync();

                    foreach (var spr in sprites)
                    {
                        gridMain.Children.Remove(spr);      //remove all game pieces
                    }

                    txtGoals.Text = "";                     //clear the goal

                    recEndMenu.Visibility = Visibility.Visible;             //enable replay window
                    txtReplayQuestion.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                }

                if (difficulty >= 11)       //check if the player pass 10 levels
                {
                    txtScoreBoard.Text = "";    //then clear the score board and alert results one last time

                    await new MessageDialog($"Level {difficulty - 1} Completed!\nYou have escaped the Keiper belt!\nYour best score is {points}!\nThank you for playing.").ShowAsync();

                    FolderPicker fp = new FolderPicker();
                    fp.SuggestedStartLocation = PickerLocationId.Desktop;
                    fp.FileTypeFilter.Add("*");

                    StorageFolder folder = await fp.PickSingleFolderAsync();
                    if (folder == null) return;
                    StorageFile resultFile = await folder.CreateFileAsync("spacepoints.txt", CreationCollisionOption.OpenIfExists);
                    IRandomAccessStream stream = await resultFile.OpenAsync(FileAccessMode.ReadWrite);
                    using (IOutputStream outputstream = stream.GetOutputStreamAt(stream.Size))
                    {
                        using (DataWriter datawriter = new DataWriter(outputstream))
                        {
                            datawriter.WriteString(
                                $"Player: {playerName}\n\n" +
                                $"Level: {difficulty - 1}\n" +
                                $"Current Points: {points}\n" +
                                $"Highest Points: {highestPoints}\n" +
                                $"+100 points for reaching Burger Shop.\n" +
                                $"-50 points for every asteroid impact.\n");
                            await datawriter.StoreAsync();
                            await datawriter.FlushAsync();
                        }
                    }
                    stream.Dispose();

                    foreach (var spr in sprites)
                    {
                        gridMain.Children.Remove(spr);      //remove all game pieces
                    }

                    points = 0;

                    recEndMenu.Visibility = Visibility.Visible;             //enable replay window
                    txtReplayQuestion.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)     //catch system exceptions
            {
                await new MessageDialog($"Your game ran into an error.\n{ex}\n.Please contact the administrator.").ShowAsync();
                
                Application.Current.Exit(); //finally, close the application
            }

        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 1;
            points = 0;
            LoadGame();
        }

        private async void btnNo_Click(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("See you next time!").ShowAsync();          //send a goodbye message for exit
            Application.Current.Exit(); //finally, close the application
        }
    }

}
