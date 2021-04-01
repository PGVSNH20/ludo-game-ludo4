using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public class Board
    {
        public int Id { get; set; }
        public Piece Piece { get; set; }

        public static void Move()
        {

        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public static void AskIfMoveFromNestOrMoveOnBoard()
        {
            bool isRunning = true;
            int userInput = 0;
            while (isRunning)
            {
                Console.WriteLine("Do you want move a piece from the nest or move a piece on board?\n" +
                                    "[1] Move piece from the nest\n" +
                                    "[2] Move piece on the board");

                int.TryParse(Console.ReadLine(), out userInput);
                //TODO-Kolla till om det finns pjäser ute på brädan. Kunna ge ett felmeddelande om det inte en pjäs ute på brädan ifall man trycker på "Move piece on the board"
                switch (userInput)
                {
                    case 1:
                        //Här ska vi flytta en pjäs från nest:et.
                        isRunning = false;
                        break;

                    case 2:
                        Move();
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
