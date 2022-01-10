//Hy Vo Gia - Lab 3 Banner Printing App

using System;

namespace Banner_Printing_App
{
    class Program
    {
        static void Main(string[] args)
        {
            bool notRunning = false;
            Console.WriteLine("Welcome to Banner Printing App\r"); //A friendly title
            Console.WriteLine("------------------------------\n");
            
            //A while loop allows the program to continue after the user created their banner
            //As long as the condition is true, the program will repeat iself
            while (!notRunning)
            {
                string message;
                int width;
                bool validate = false;

                //This ask the user for the message on the banner
                Console.WriteLine("Please enter a message for the banner: ");
                message = Console.ReadLine();

                //This check if there's no input
                while (message == "")
                {
                    Console.WriteLine("The input is invalid. Please enter a message: ");
                    message = Console.ReadLine();
                }

                //This ask the user how wide they want their banner to be
                Console.WriteLine("Please enter the width of the banner: ");
                width = int.Parse(Console.ReadLine());

                //A while loop when the width of the banner is less than 1
                while (width < 1)
                {
                    Console.WriteLine("Please enter an approapriate number: ");
                    width = int.Parse(Console.ReadLine());
                }

                //A while loop when the message's length is longer than the banner's width
                while (message.Length > width)
                {
                    Console.WriteLine("The banner's length must be greater than the message's length. Please try again: ");
                    width = int.Parse(Console.ReadLine());
                }

                //Ask the user to choose whether they want their message in the middle of their banner
                Console.WriteLine("Do you want the message in the center of the banner?");
                Console.WriteLine("y - Yes");
                Console.WriteLine("n - No");
                if (Console.ReadLine() == "y") validate = true; //Set condition to true when the user accept

                //The blank space inside the banner is determined by the input for the width of the banner
                //minus 3 for two asterisks by the side, and minus the length of the input message.
                int extraSpace = width - 3 - message.Length;

                //The first for loop repeatedly write the string until 
                //the total amount equals to the length of the banner.
                for (int row = 1; row <= width; row++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();

                //The second for loop repeatedly write the string,  
                //and the message from the user until 
                //the total amount equals to the length of the banner.
                for (int row = 1; row <= 1; row++)
                {
                    //The extra space prevent the distortion of the banner
                    //if the length of the input message is odd
                    Console.Write("* ");

                    //When the user choose to have their message at the center of the banner,
                    //the blank space in the banner will be split in two,
                    //each now locate on both sides of input message
                    if (validate)
                    {
                        extraSpace = extraSpace / 2; //the blank space inside will be halve

                        for (int spot = 1; spot <= extraSpace; spot++) //and with one by the left of input message
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.Write(message); //the message will be in the middle
                    
                    for (int spot = 1; spot <= extraSpace; spot++) //and one by the right per usual
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*");
                }
                Console.WriteLine();

                //The final for loop to finish the banner
                for (int row = 1; row <= width; row++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();

                //Ask the user to make another banner or exit
                Console.WriteLine("Do you wish to create another banner?");
                Console.WriteLine("c - Continue");
                Console.WriteLine("e - Exit");

                //If the user choose e, the loop ends
                if (Console.ReadLine() == "e") notRunning = true;
                Console.WriteLine("------------------------------\n"); //A line to sperate the previous complete portion
            }
            return;
        }
    }
}
