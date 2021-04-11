using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.Initialize
{
   public class LoadGame
    {
        public void LoadAnyGame(int gameID) //Laddar parametrar och skickar det sedan till spelet som sedan körs
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

        public void ContinueGame()
        {
            // TODO - Här måste vi hämta GetHighestBoardId för att fortsätta spela klart senast sparade spelet
            //Ladda senast sparade spelet
            LudoDbAccess ludoDbAccess = new LudoDbAccess();

            // GET latest game from board id
            var gameBoard = ludoDbAccess.GetHighestBoardId();
            LoadAnyGame(gameBoard);

        }
    }
}
