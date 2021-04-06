using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine
{
	public class GameLoop
	{
        public static void InitializeGame(int choice)
		{
            UserInterface ui = new UserInterface();
            ui.MainMenu();
            Game game = new Game();
			switch (choice)
			{
                case 1:
					game.CreateGame();
                    break;
                case 2:
                    game.ContinueGame(); //Kolla vilken vi ska använda automatiskt
                    game.LoadGame();
                    break;
				default:
					break;
			}
		}

        public static void RunGame(List<Player> players) //Kanske måste uppdelas eller göras om till initialize
        {
            Board board = new Board();
            Dice rollDice = new Dice();

            bool isPlaying = true; //ska flyttas
			while (isPlaying) //loopar omgångar
			{
				for (int t = 0; t <= players.Count; t++) //loopar spelarnas turn
				{
                    Player currentPlayer = Update.GetPlayerTurn(players);

                    List<Piece> currentPlayerPieces = Update.GetPlayerPieces(currentPlayer);

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
                            board.AskIfMoveFromNestOrMoveOnBoard(currentPlayerPieces, i, currentPlayer.PlayerBoard);
                        }
                    }

                    board.MovePiece(currentPlayerPieces, i, currentPlayer.PlayerBoard);
                }
            }
        }
    }
}
