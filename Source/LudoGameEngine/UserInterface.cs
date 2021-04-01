using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine
{
	public class UserInterface
	{
        private static int userInput = 0;
        private static bool isRunning = true;


        public void MainMenu()
        {

            while (isRunning)
            {
                Console.WriteLine("Hello and welcome to Ludo game! What do you want to do?\n" +
                                    "[1] Create new game\n" +
                                    "[2] Load game");

                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {
                    case 1:
                        Game.CreateGame();
                        isRunning = false;
                        break;

                    case 2:
                        Game.LoadGame();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("You need to press 1 or 2");
                        break;

                }
            }
        }

	}
}
