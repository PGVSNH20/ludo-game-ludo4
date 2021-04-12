using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    // TODO - Måste fixa så att man inte kan gå med en pjäs i mitten (så att den skippar rundan), den måste då gå med någon annan pjäs
    public class Move
    {
        public List<Piece> MovePiece(Piece piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            List<Piece> updatedPositions = new List<Piece>();

            var index = playerGameBoard.IndexOf(piece.Position);

            index = index + diceValue;

            // Checking if position <= 30, else Catch
            try
            {
                piece.Position = playerGameBoard[index];

                if (piece.Position == 30)
                {                   
                    piece.IsActive = false;
                }

                updatedPositions.Add(piece);
            }
            catch (Exception e)
            {
                // If Position > FinishLine, Continue
            }


            if (updatedPositions.Count > 0)
            {
                var checkIfPushingAnotherPlayer = CheckPositions(updatedPositions[0], players);
                foreach (var p in checkIfPushingAnotherPlayer)
                {
                    updatedPositions.Add(p);
                }
            }

            return updatedPositions;
        }

        // Asks if the player wants to move a piece from nest or a piece on the game board.
        public List<Piece> MoveFromNestOrBoard(List<Piece> pieces, int diceValue, List<Player> players)
        {
            int userInput = 0;
            Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);

            Console.Clear();
            Square.CurrentBoard(players);

            Console.WriteLine($"Player {currentPlayer.PlayerColor}: {currentPlayer.Name}, you rolled {diceValue}!");


            // Checking if there are pieces in nest or on the game board
            bool isPieceInNest = false;
            bool isPieceOnBoard = false;

            // Checking Nest
            List<Piece> piecesInNest = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (GameBoard.nestPositions.Contains(Convert.ToInt32(piece.Position)))
                {
                    piecesInNest.Add(piece);
                    isPieceInNest = true;
                }
            }

            // Checking Game Board
            List<Piece> piecesOnGameBoard = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (piece.Position != currentPlayer.PlayerBoard[0] && piece.Position != 30)
                {
                    piecesOnGameBoard.Add(piece);
                    isPieceOnBoard = true;
                }
            }

            List<Piece> updatedPositions = new List<Piece>();

            // Choose To Move Piece From Nest Or Board
            if (isPieceInNest && isPieceOnBoard)
            {
                Console.WriteLine("You rolled 6! Choose a piece from the nest or on the board!");

                var counter = piecesOnGameBoard.Count + piecesInNest.Count;

                while (userInput < 1 || userInput > counter)
                {
                    int.TryParse(Console.ReadLine(), out userInput);
                    
                    if(userInput < 1 || userInput > counter)
                    {
                        Console.WriteLine($"You pressed wrong number! Try agin");
                    }
                }


                for (int i = 0; i < piecesInNest.Count; i++)
                {
                    if (userInput >= 0 && userInput <= 4)
                    {
                        if (piecesInNest[i].Id == pieces[userInput - 1].Id)
                        {
                            Console.Clear();
                            Square.CurrentBoard(players);
                            Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!");

                            updatedPositions = MovePiece(piecesInNest[i], 1, currentPlayer.PlayerBoard, players);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"You pressed wrong number!");
                        }
                    }
                }

                for (int i = 0; i < piecesOnGameBoard.Count; i++)
                {
                    if (userInput >= 0 && userInput <= 4)
                    {
                        if (piecesOnGameBoard[i].Id == pieces[userInput - 1].Id)
                        {
                            Console.Clear();
                            Square.CurrentBoard(players);
                            Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!");

                            updatedPositions = MovePiece(piecesOnGameBoard[i], diceValue, currentPlayer.PlayerBoard, players);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"You pressed wrong number!");
                        }
                    }
                }
            }

            // Move Piece From Nest
            else if (isPieceInNest && !isPieceOnBoard)
            {
                Console.Clear();
                Square.CurrentBoard(players);

                Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!\n" +
                                        "Which piece do you want to move from the nest?");
                bool isRunning = true;
                do
                {
                    int.TryParse(Console.ReadLine(), out userInput);

                    for (int i = 0; i < piecesInNest.Count; i++)
                    {
                        if(userInput <= piecesInNest.Count && userInput > 0)
                        {
                            if (piecesInNest[i].Id == pieces[userInput - 1].Id)
                            {
                                Console.Clear();
                                Square.CurrentBoard(players);

                                updatedPositions = MovePiece(piecesInNest[i], 1, currentPlayer.PlayerBoard, players);
                                isRunning = false;
                                break;
                            }
                        }
                    }

                } while (isRunning);
            }

            // Move Piece On The Game Board
            else
            {
                Console.Clear();
                Square.CurrentBoard(players);

                Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!\n" +
                                        "Which piece do you want to move on the board");
                bool isRunning = true;
                do
                {
                    int.TryParse(Console.ReadLine(), out userInput);

                    for (int i = 0; i < piecesOnGameBoard.Count; i++)
                    {
                        if (userInput <= piecesOnGameBoard.Count && userInput > 0)
                        {
                            if (piecesOnGameBoard[i].Id == pieces[userInput - 1].Id)
                            {
                                Console.Clear();
                                Square.CurrentBoard(players);

                                updatedPositions = MovePiece(piecesOnGameBoard[i], diceValue, currentPlayer.PlayerBoard, players);
                                isRunning = false;
                                break;
                            }
                        }
                    }

                } while (isRunning);
            }

            Console.Clear();
            return updatedPositions;
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

            for (int i = 0; i < eachPlayersPieces.Count; i++)
            {
                foreach (var piece in eachPlayersPieces[i])
                {
                    // Checking for knuff
                    if (piece.Position == movedPiece.Position && piece.PlayerId != movedPiece.PlayerId)
                    {
                        Console.WriteLine("KNUFF!".Rainbow());
                        piece.Position = GameBoard.nestPositions[i];
                        updatedPieces.Add(piece);

                        Thread.Sleep(3000);
                    }
                }
            }
            return updatedPieces;
        }
    }
}
