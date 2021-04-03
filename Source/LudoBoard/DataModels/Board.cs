using System;

namespace LudoBoard.DataModels
{
    public class Board
    {
        public static void MovePiece()
        {
            
        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public void AskIfMoveFromNestOrMoveOnBoard()
        {
            Square square = new Square();
            bool isRunning = false;
            int userInput = 0;
            while (isRunning)
            {
                Console.WriteLine("Do you want move a piece from the nest or move a piece on board?\n" +
                                    "[1] Move piece from the nest\n" +
                                    "[2] Move piece on the board\n" +
                                    "[3] View current board");

                int.TryParse(Console.ReadLine(), out userInput);
                //TODO-Kolla till om det finns pjäser ute på brädan. Kunna ge ett felmeddelande om det inte en pjäs ute på brädan ifall man trycker på "Move piece on the board"
                isRunning = false;
                switch (userInput)
                {
                    case 1:
                        //Här ska vi flytta en pjäs från nest:et.
                        break;

                    case 2:
                        MovePiece();
                        break;

                    case 3:
                        Square.CurrentBoard();
                        break;

                    default:
                        Console.WriteLine("You need to press 1, 2 or 3");
                        isRunning = true;
                        break;

                }

                

            }

        }

        //TODO-Programmet ska göra en randomnize på spelarna som är registrerade, för att avgöra vem som går först. 
        public void WhoGoesFirst()
        {

        }


        //TODO-Programmet ska avgöra vems tur det är att kasta tärningen. 
        public void WhoesTurnToRollTheDice()
        {

        }

    }
}
