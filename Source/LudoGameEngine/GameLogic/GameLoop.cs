using LudoBoard.DataModels;
using LudoGameEngine.GameLogic;
using LudoGameEngine.Initialize;
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
            bool isPlaying = true; // ska flyttas              (till? /Magnusson)

			while (isPlaying) // loopar omgångar
			{
				//for (int t = 0; t <= players.Count; t++) // loopar spelarnas turn
				//{
                    // Kollar vilkens spelares tur det är
                    Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);
                    
                    List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

                    Console.WriteLine("1. rolldice");
                    do
                    {
                        int.TryParse(Console.ReadLine(), out userInput);

                        if (userInput == 1)
                        {
                            // Roll Dice
                            diceValue = rollDice.RollDice();
                            Console.WriteLine($"You rolled {diceValue}");
                        }
                        else
                            Console.WriteLine("Please enter a valid number.");
                    } while (userInput != 1);

                    // Check if dicevalue is 6 & if there is a piece in the nest. 
                    List<int> nestChecker = new List<int>() { 0, 4, 56, 60 };

                    if (diceValue == 6)
                    {
                        foreach (var p in currentPlayerPieces)
                        {
                            if (nestChecker.Contains(Convert.ToInt32(p.Position)))
                            {
                                move.AskIfMoveFromNestOrMoveOnBoard(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard);
                            }
                        }
                    }

                    move.MovePiece(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard);

                    // TODO-Programmet ska avgöra vems tur det är att kasta tärningen.; 
                    // WhoesTurnToRollTheDice()

                    UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
               
                //}
            }
        }
    }
}
