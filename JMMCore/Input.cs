using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class Input
    {
        public static int GetUserNumString()
        {
            int userInputParsed = 0;

            bool parsed = false;
            while (!parsed)
            {
                string userInput = Console.ReadLine();
                parsed = Int32.TryParse(userInput, out userInputParsed);
                if (!parsed)
                {
                    Console.Beep();
                    Console.WriteLine("Not a Valid Entry");
                }

            }

            return userInputParsed;
        }

        public static int GetUserNum(int min, int max)
        {
            bool isNum = false;
            int userInput = 0;
            while (!isNum)
            {
                ConsoleKeyInfo userAscii = Console.ReadKey();
                Console.WriteLine();
                userInput = userAscii.KeyChar - 48;
                if (userInput >= min && userInput <= max)
                    isNum = true;
                else
                    Console.WriteLine("Not a valid number. Try Again");
            }
            return userInput;
        }


        public static char GetUserAny()
        {
            char userInput;
            ConsoleKeyInfo userAscii = Console.ReadKey();
            Console.WriteLine();
            userInput = userAscii.KeyChar;
            return userInput;

        }

        public static bool GetUserYesNo()
        {
            bool validInput = false;
            char userInput = ' ';
            while (!validInput)
            {
                ConsoleKeyInfo userAscii = Console.ReadKey();
                Console.WriteLine();
                userInput = userAscii.KeyChar;
                if (userInput == 'y' || userInput == 'n')
                    validInput = true;
                else
                    Console.WriteLine("Invalid input. Say \"y\" for yes or \"n\" for no.");
            }
            if (userInput == 'y')
                return true;
            else
                return false;
        }

        public static int GetUserString()
        {
            int userInputParsed = 0;
            bool parsed = false;

            while (!parsed)
            {
                string userInput = Console.ReadLine();
                parsed = Int32.TryParse(userInput, out userInputParsed);
                if (!parsed)
                {
                    Console.Beep();
                    Console.WriteLine("Not a Valid Entry");
                }

            }

            return userInputParsed;
        }
    }
}
