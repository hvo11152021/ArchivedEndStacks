using System.Drawing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.System;
using System.Numerics;
using Windows.ApplicationModel.Background;
using System;

//PROG1621 Lab 1B - Hy Vo

namespace GameLibrary
{
    public class GamePiece : IEquatable<GamePiece>
    {
        public Thickness objectMargins;            //default codes...
        private Image onScreen;

        private string name;

        public Thickness Location => onScreen.Margin;

        public string Name => name;                 //output the name of the game piece

        public GamePiece(Image img, string spriteName)                 
        {                                           
            onScreen = img;
            objectMargins = img.Margin;
            name = spriteName;                      //set name of game pieces
        }

        public bool Move(VirtualKey direction)      //default codes...
        {
            switch (direction)
            {
                case VirtualKey.W:                  //add additional keys like wasd
                    objectMargins.Top -= 25;
                    break;
                case VirtualKey.S:
                    objectMargins.Top += 25;
                    break;
                case VirtualKey.A:
                    objectMargins.Left -= 25;
                    break;
                case VirtualKey.D:
                    objectMargins.Left += 25;
                    break;
                case VirtualKey.Up:
                    objectMargins.Top -= 25;
                    break;
                case VirtualKey.Down:
                    objectMargins.Top += 25;
                    break;
                case VirtualKey.Left:
                    objectMargins.Left -= 25;
                    break;
                case VirtualKey.Right:
                    objectMargins.Left += 25;
                    break;
                case VirtualKey.Space:
                    objectMargins.Left += 25;
                    break;
                default:
                    return false;
            }
            onScreen.Margin = objectMargins;
            return true;
        }

        public bool Fly(VirtualKey path)
        {
            if (path is VirtualKey.Space)
            {
                for (int i = 0; i < 10; i++)        //spawn bullets a few hundred pixels
                {                                   //in front of the ship
                    objectMargins.Left += 25;
                }
            }
            onScreen.Margin = objectMargins;
            return true;
        }

        public bool Equals(GamePiece other)
        {
            return this.Location == other.Location;         //set location of a game piece equals to the other
        }
    }
}
