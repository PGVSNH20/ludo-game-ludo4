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

			switch (choice)
			{
                case 1:
                    game.ContinueGame(); //Kolla vilken vi ska använda automatiskt
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

            bool isPlaying = true; //ska flyttas
			while (isPlaying) //loopar omgångar
			{
				for (int t = 0; t <= players.Count; t++) //loopar spelarnas turn
				{
                    Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);

                    List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

                    Console.WriteLine("1. rolldice");
                    Console.ReadKey();

                    // Slå tärning            
                    int i = rollDice.RollDice();
                    Console.WriteLine($"You rolled {i}");

                    // TODO - Lägg till check om det finns pieces i nest.
                    List<int> nestChecker = new List<int>() { 0, 4, 56, 60 };
                    foreach (var p in currentPlayerPieces)
                    {
                        if (i == 6 && nestChecker.Contains(Convert.ToInt32(p.Position)))
                        {
                            move.AskIfMoveFromNestOrMoveOnBoard(currentPlayerPieces, i, currentPlayer.PlayerBoard);
                        }
                    }

                    move.MovePiece(currentPlayerPieces, i, currentPlayer.PlayerBoard);
                }
            }
        }
    }
}
