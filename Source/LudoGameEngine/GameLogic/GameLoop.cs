﻿using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.GameLogic;
using LudoGameEngine.Initialize;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Dice rollDice = new Dice();
            Move move = new Move();
            int userInput = 0;
            int diceValue = 0;
            bool isPlaying = true;

            //Square square = new Square();
            Square.CurrentBoard(players);

            while (isPlaying) // loopar omgångar
            {
                
                // Kollar vilkens spelares tur det är
                Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);

                List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

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
                        Console.WriteLine($"It's {currentPlayer.Name}'s time to roll! ");
                        Console.WriteLine($"{currentPlayer.Name} rolled {diceValue}!");

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

                    foreach (var p in currentPlayerPieces)
                    {
                        if (nestChecker.Contains(Convert.ToInt32(p.Position)))
                        {
                        
                            move.MoveFromNestOrBoard(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);
                   
                
                        }
                    }
                }
                else
                { 
                    foreach (var piece in currentPlayerPieces)
                    {
                        if (piece.Position > currentPlayer.PlayerBoard[0] && piece.Position < currentPlayer.PlayerBoard[45])
                        {
                            
                            currentPlayerPieces = move.MovePiece(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);
         
             

                        }
                    }
                }
               
                    UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
                
            }
        }
    }
}
