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
                        Console.WriteLine($"Player {currentPlayer.PlayerColor}: {currentPlayer.Name} rolled {diceValue}!");

                    }
                    else
                    {
                        Console.Write("Please enter 1 to roll:");
                    }
                  
                } while (userInput != 1);

                // Check if dicevalue is 6 & if there is a piece in the nest. 
                List<int> nestChecker = new List<int>() { 0, 4, 56, 60 };

                if (diceValue == 6)
                {
                    foreach (var piece in currentPlayerPieces)
                    {
                        if (nestChecker.Contains(Convert.ToInt32(piece.Position)))
                        {

                            updatedPositions = move.MoveFromNestOrBoard(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);
                        }
                    }
                }
                else
                {
                    // Fixad
                    // piece.Position > & < currentPlayer.PlayerBoard[0] denna är fel, spelare 3 har piece.position == 60. Då blir den aldrig > än [0]
                    foreach (var piece in currentPlayerPieces)
                    {
                        if ( !currentPlayer.PlayerBoard[0].Equals(piece.Position) && piece.Position != 30)
                        {

                            updatedPositions = move.MovePiece(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);
                        }
                    }
                }
               
                    UpdateGameBoard.UpdatePlayerTurn(updatedPositions, players);
            }
        }
    }
}
