using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoGameEngine.Initialize
{
   public class LoadGame
    {
        public void LoadAnyGame(int gameID) // Loading The Game Chosen In Loading Menu
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();

            // GET all players from game id
            var players = ludoDbAccess.GetPlayersWhenLoadingGame(gameID);

            // SET players Game Board
            GameBoard gameBoard = new GameBoard();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].PlayerBoard = gameBoard.PlayersGameBoards[i];
            }

            // Run Game
            GameLoop gameLoop = new GameLoop();
            gameLoop.RunGame(players);
        }

        // Loading The Latest Game
        public void ContinueGame()
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            UserInterface userInterface = new UserInterface();

            // GET latest game from board id
            var gameBoard = ludoDbAccess.GetHighestBoardId();
            if (gameBoard == 0)
            {
                Console.WriteLine("No game found.. " +
                                  "\nCreate a new game..");
                Thread.Sleep(2000);
                userInterface.MainMenu();
                
            }

            LoadAnyGame(gameBoard);
        }
    }
}
