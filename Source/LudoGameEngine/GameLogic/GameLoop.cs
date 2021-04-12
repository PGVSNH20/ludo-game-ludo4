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

        public void RunGame(List<Player> players)
        {
            while (true) // loops rounds
            {
                Dice rollDice = new Dice();
                Move move = new Move();
                int diceValue = 0;

                // View Current Board
                Console.Clear();
                Square.CurrentBoard(players);

                // checks player turn
                Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);
                List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

                Console.WriteLine("[1] Rolldice");
                int pieceId = 0;
                do
                {
                    int.TryParse(Console.ReadLine(), out pieceId);

                    if (pieceId == 1)
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

                } while (pieceId != 1);

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
                    for (int i = 0; i < currentPlayerPieces.Count; i++)
                    {
                        if (currentPlayerPieces[i].Position != currentPlayer.PlayerBoard[0] && currentPlayerPieces[i].Position != 30)
                        {
                            piecesOnBoard.Add(currentPlayerPieces[i]);
                        }
                    }

                    if (piecesOnBoard.Count != 0)
                    {

                        Console.WriteLine($"\nWhich piece do you want to move?");

                        for (int i = 0; i < currentPlayerPieces.Count; i++)
                        {
                            if (currentPlayerPieces[i].Position != currentPlayer.PlayerBoard[0] && currentPlayerPieces[i].Position != 30)
                            {
                                Console.WriteLine($"[{i + 1}] Piece {i + 1}");
                            }
                        }

                        bool isRunning = true;
                        do
                        {
                            int.TryParse(Console.ReadLine(), out pieceId);

                            for (int i = 0; i < piecesOnBoard.Count; i++)
                            {
                                try
                                {
                                    if (piecesOnBoard[i].Id == currentPlayerPieces[pieceId - 1].Id)
                                    {
                                        updatedPositions = move.MovePiece(piecesOnBoard[i], diceValue, currentPlayer.PlayerBoard, players);
                                        isRunning = false;
                                        break;
                                    }
                                }
                                catch (Exception)
                                {
                                    // If id doesnt exist, try again
                                    break;
                                }
                            }

                        } while (isRunning);
                    }
                }

                UpdateGameBoard.UpdatePlayerTurn(updatedPositions, players, diceValue);
            }
        }
    }
}
