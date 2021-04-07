using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.Initialize
{
   public class LoadGame
    {
        public int LoadAnyGame(int gameID) //Laddar parametrar och skickar det sedan till spelet som sedan körs
        {
            //TODO - Ta hem ett specifikt gameID från databasen och sedan returna det
            return 0;
        }

        public void ContinueGame()
        {
            // TODO - Här måste vi hämta GetHighestBoardId för att fortsätta spela klart senast sparade spelet
            //Ladda senast sparade spelet
            //LoadGame();
        }
    }
}
