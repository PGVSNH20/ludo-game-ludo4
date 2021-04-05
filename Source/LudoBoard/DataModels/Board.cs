using System;
using System.Collections.Generic;

namespace LudoBoard.DataModels
{
    public class Board
    {
        public void MovePiece(List<Piece> piece, int diceValue, List<object> playerGameBoard)
        {
            int pieceId = 0;
            Console.WriteLine("Which piece do you want to move? (Id)");
            int.TryParse(Console.ReadLine(), out pieceId);

            var index = playerGameBoard.IndexOf(piece[pieceId].Position);
            index = index + diceValue;
            piece[pieceId].Position = Game.player1GameBoard[index];

            Console.WriteLine($"Index is: {index} and the new position is {piece[pieceId].Position}!");
            
            // TODO - Kanske skapa en metod i LudoDbAccess för att uppdatera databasen.
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
                        //MovePiece();
                        break;

                    case 3:
                        square.CurrentBoard();
                        break;

                    default:
                        Console.WriteLine("You need to press 1, 2 or 3");
                        isRunning = true;
                        break;

                }
            }
        }

        //TODO-Programmet ska avgöra vems tur det är att kasta tärningen. WhoesTurnToRollTheDice(); 
        public void WhoesTurnToRollTheDice()
        {

        }

    }
}
