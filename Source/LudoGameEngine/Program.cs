using LudoBoard.DataModels;
using System;

namespace LudoGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var runMenu = new UserInterface();

            runMenu.MainMenu();


            //var rolledDice = new Dice();
            //rolledDice.RollDice();

            //Console.WriteLine($"{rolledDice.RollDice()}");
        }
    }
}
