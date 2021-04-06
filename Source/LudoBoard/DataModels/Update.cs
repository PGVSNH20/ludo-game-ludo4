using LudoBoard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public static class Update
    {
        public static Player GetPlayerTurn(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerTurn == true)
                {
                    Console.WriteLine($"It's player {player.Name} turn to roll the dice.");

                    return player;
                }
            }

            return null;
        }

        public static List<Piece> GetPlayerPieces(Player player)
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<Piece> playersPieces = ludoDbAccess.GetCurrentPlayersPieces(player.Id);

            return playersPieces;
        }
    }
}
