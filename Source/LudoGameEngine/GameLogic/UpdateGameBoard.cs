using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public static class UpdateGameBoard
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

        public static void UpdatePlayerTurn(List<Piece> pieces, List<Player> currentPlayers)
        {
            Player updateThisplayer = null;
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int playercounter = currentPlayers.Count;
            Console.WriteLine($"PlayerCounter = {playercounter}");

            for (int i = 0; i < currentPlayers.Count; i++)
            {
                Console.WriteLine($"i = {i}");

                if (currentPlayers[i].PlayerTurn == true)
                {
                    updateThisplayer = currentPlayers[i];
                    Console.WriteLine($"currentplayer[i-1] = {currentPlayers[i].Name}");

                    currentPlayers[i].PlayerTurn = false;

                    if (i == playercounter -1)
                    {
                        Console.WriteLine($"i == playercounter");

                        currentPlayers[0].PlayerTurn = true;
                        ludoDbAccess.SavePositionsToDb(pieces, currentPlayers);
                    }
                    else
                    {
                        currentPlayers[i+1].PlayerTurn = true;

                        Console.WriteLine($"{currentPlayers[i+1].Name} Sätter player turn till true");
                        ludoDbAccess.SavePositionsToDb(pieces, currentPlayers);
                    }
                }
            }
        }

        public static List<Piece> GetPlayerPieces(Player player)
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<Piece> playersPieces = ludoDbAccess.GetCurrentPlayersPieces(player.Id);

            return playersPieces;
        }
    }
}
