using LudoBoard.DataModels;
using LudoGameEngine.GameLogic;
using LudoGameEngine.Initialize;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;

namespace LudoGameEngine
{
    public class GameLoop
    {
        public static bool isPlaying = false;

        public void InitializeGame(int choice)
        {
            CreateGame game = new CreateGame();
            LoadGame loadGame = new LoadGame();

            switch (choice)
            {
                case 1:
                    loadGame.ContinueGame(); //Kolla vilken vi ska använda automatiskt
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
            bool isPlaying = true;
            while (isPlaying) // loopar omgångar
            {
                // View Current Board
                Console.Clear();
                Square.CurrentBoard(players);

                Dice rollDice = new Dice();
                Move move = new Move();
                int userInput = 0;
                int diceValue = 0;

                // Kollar vilkens spelares tur det är
                Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);

                List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);
                List<Piece> updatedPositions = new List<Piece>();

                Console.WriteLine("1. rolldice");


                do
                {
                    int.TryParse(Console.ReadLine(), out userInput);

                    if (userInput == 1)
                    {
                        Console.Clear();
                        Square.CurrentBoard(players);

                        // Roll Dice
                        diceValue = rollDice.RollDice();
                    }
                    else
                    {
                        Console.Write("Please enter 1 to roll:");
                    }

                } while (userInput != 1);

                // Check if dicevalue is 6 & if there is a piece in the nest. 
                List<int> nestChecker = new List<int>() { 0, 4, 60, 56 };
                if (diceValue == 6)
                {
                    foreach (var piece in currentPlayerPieces)
                    {
                        if (nestChecker.Contains(Convert.ToInt32(piece.Position)))
                        {
                            updatedPositions = move.MoveFromNestOrBoard(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);
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
                                        Console.WriteLine($"{i + 1}. Piece");
                                    }
                                }
                                
                                int.TryParse(Console.ReadLine(), out pieceId);
                                pieceId -= 1;

                                foreach (var p in piecesOnBoard)
                                {
                                    if (p.Id == currentPlayerPieces[pieceId].Id)
                                    {
                                        isTrueorFalse = false;
                                    }
                                }

                            } while (isTrueorFalse);

                            

                            updatedPositions = move.MovePiece(currentPlayerPieces[pieceId], diceValue, currentPlayer.PlayerBoard, players);
                            break;
                        }
                    }
                }

                UpdateGameBoard.UpdatePlayerTurn(updatedPositions, players);
            }
        }
    }
}
