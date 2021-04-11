using LudoBoard.DataModels;
using LudoGameEngine.GameLogic;
using LudoGameEngine.Initialize;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using Edokan.KaiZen.Colors;
using System.Threading;

namespace LudoGameEngine
{
    public class GameLoop
    {
        public void InitializeGame(int choice)
        {
            CreateGame game = new CreateGame();
            LoadGame loadGame = new LoadGame();

            switch (choice)
            {
                case 1:
                    loadGame.ContinueGame();
                    break;
                case 2:
                    game.CreateNewGame();
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
        }

        public void RunGame(List<Player> players) //Kanske måste uppdelas eller göras om till initialize
        {
            while (true) // loopar omgångar
            {
                Dice rollDice = new Dice();
                Move move = new Move();
                int diceValue = 0;

                // View Current Board
                Console.Clear();
                Square.CurrentBoard(players);

                // Kollar vilkens spelares tur det är
                Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);
                List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

                Console.WriteLine("[1] Rolldice");
                int userInput = 0;
                do
                {
                    int.TryParse(Console.ReadLine(), out userInput);

                    if (userInput == 1)
                    {
                        // Roll Dice
                        diceValue = rollDice.RollDice();
                        Console.WriteLine("\nYou rolled: {0}", diceValue);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.Write("Please enter 1 to roll:");
                    }

                } while (userInput != 1);

                List<Piece> updatedPositions = new List<Piece>();

                // Check if dicevalue is 6 & if there is a piece in the nest. 
                List<int> nestChecker = new List<int>() { 0, 4, 60, 56 };
                if (diceValue == 6)
                {
                    foreach (var piece in currentPlayerPieces)
                    {
                        if (nestChecker.Contains(Convert.ToInt32(piece.Position)))
                        {
                            updatedPositions = move.MoveFromNestOrBoard(currentPlayerPieces, diceValue, players);
                            break;
                        }

                    }
                }

                else
                {
                    List<Piece> piecesOnBoard = new List<Piece>();
                    bool isTrueorFalse = true;
                    foreach (var piece in currentPlayerPieces)
                    {

                        if (piece.Position != currentPlayer.PlayerBoard[0] && piece.Position != 30)
                        {
                            int pieceId = 0;
                            do
                            {
                                pieceId = 0;
                                Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} Rolled {diceValue}\n" +
                                                    "Which piece do you want to move?");

                                for (int i = 0; i < currentPlayerPieces.Count; i++)
                                {
                                    if (currentPlayerPieces[i].Position != currentPlayer.PlayerBoard[0] && piece.Position != 30)
                                    {
                                        piecesOnBoard.Add(currentPlayerPieces[i]);
                                        Console.WriteLine($"[{i + 1}] Piece {i + 1}");
                                    }
                                }

                                while (pieceId < 1 || pieceId > piecesOnBoard.Count)
                                { 
                                    int.TryParse(Console.ReadLine(), out pieceId);
                                }

                                pieceId -= 1;
                                foreach (var p in piecesOnBoard)
                                {

                                    if (pieceId >= 0 && pieceId <= piecesOnBoard.Count)
                                    {
                                        if (p.Id == currentPlayerPieces[pieceId].Id)
                                        {
                                            isTrueorFalse = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You pressed wrong number! Try agin");
                                    }
                                }

                            } while (isTrueorFalse);

                            Console.Clear();

                            updatedPositions = move.MovePiece(currentPlayerPieces[pieceId], diceValue, currentPlayer.PlayerBoard, players);
                            break;
                        }
                    }
                }

                UpdateGameBoard.UpdatePlayerTurn(updatedPositions, players, diceValue);
            }
        }
    }
}
