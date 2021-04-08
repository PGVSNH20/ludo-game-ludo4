using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public class Move
    {
        public List<Piece> MovePiece(List<Piece> piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            List<Piece> updatedPositions = new List<Piece>();
            
            int pieceId = 0;

            Console.WriteLine("Which piece do you want to move? (Id)");
            int.TryParse(Console.ReadLine(), out pieceId);
            pieceId -= 1;

            var index = playerGameBoard.IndexOf(piece[pieceId].Position);
            Console.WriteLine($"\nCurrent index of the piece: {index} | Current position: {piece[pieceId].Position}!");

            index = index + diceValue;
            piece[pieceId].Position = playerGameBoard[index];
            Console.WriteLine($"Updated index of the piece: {index} and the new position is {piece[pieceId].Position}!\n");

            updatedPositions.Add(piece[pieceId]);
            // Göra en loop för att hitta listan som CheckPositions håller.
            updatedPositions.Add(CheckPositions(updatedPositions[0], players));

            return updatedPositions; 
        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public void MoveFromNestOrBoard(List<Piece> piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            //Square square = new Square();
            bool isRunning = true;
            int userInput = 0;

            while (isRunning)
            {
                Console.WriteLine("Do you want move a piece from the nest or move a piece on board?\n" +
                                    "[1] Move piece from the nest\n" +
                                    "[2] Move piece on the board\n");

                int.TryParse(Console.ReadLine(), out userInput);
                
                //TODO-Kolla till om det finns pjäser ute på brädan. Kunna ge ett felmeddelande om det inte en pjäs ute på brädan ifall man trycker på "Move piece on the board"
                isRunning = false;
                switch (userInput)
                {
                    case 1:
                        //Här ska vi flytta en pjäs från nest:et.
                        isRunning = false;
                        break;

                    case 2:
                        MovePiece(piece, diceValue, playerGameBoard, players);
                        isRunning = false;
                        break;

                    case 3:
                        //Square.CurrentBoard();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("You need to press 1, 2 or 3");
                        isRunning = true;
                        break;

                }
            }
        }
        public List<Piece> CheckPositions(Piece movedPiece, List<Player> players)
        {            
            var ludoDbAccess = new LudoDbAccess();
            List<Piece> updatedPieces = new List<Piece>();
            List<List<Piece>> eachPlayersPieces = new List<List<Piece>>();

            for (int i = 0; i < players.Count; i++)
            {
                eachPlayersPieces.Add(ludoDbAccess.GetCurrentPlayersPieces(players[i].Id));
            }

            List<int> nest = new List<int>();
            GameBoard gameBoard = new GameBoard();
            nest = gameBoard.nestPositions;

            for (int i = 0; i < eachPlayersPieces.Count; i++)
            {
                foreach (var piece in eachPlayersPieces[i])
                {
                    if (piece.Position == movedPiece.Position)
                    {
                        Console.WriteLine($"KNUFF!{piece.Position}");
                        piece.Position = nest[i];
                        Console.WriteLine($"KNUFF!{piece.Position} knuff ny position");
                        updatedPieces.Add(piece);
                    }
                }
            }
            return updatedPieces;
        }
    }
}
