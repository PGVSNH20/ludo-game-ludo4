using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public class UpdateGameBoard
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

                    if (i == playercounter - 1)
                    {
                        Console.WriteLine($"i == playercounter");

                        currentPlayers[0].PlayerTurn = true;
                        ludoDbAccess.SavePositionsToDb(pieces, currentPlayers);
                        break;
                    }
                    else
                    {
                        currentPlayers[i + 1].PlayerTurn = true;

                        Console.WriteLine($"{currentPlayers[i + 1].Name} Sätter player turn till true");
                        ludoDbAccess.SavePositionsToDb(pieces, currentPlayers);
                        break;
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

        // TODO - PRIORITERING, FÅ DENNA ATT FUNGERA. Den skall uppdatera spelbrädet
        public static List<string> PiecesOnGameBoardUpdate(List<Piece> allActivePieces, List<Player> allActivePlayers)
        {

            List<string> gb = new GameBoard().CompleteGameBoard;

            // i = 60
            for (int i = 0; i < gb.Count; i++)
            {
                int positionCounter = 0;


                foreach (var piece in allActivePieces)
                {
                    //if (piece.PlayerId == allActivePlayers[x].Id)
                    //{

                    if (piece.Position == i)
                    {
                        positionCounter++;
                    }


                }


                for (int x = 0; x < allActivePlayers.Count; x++)
                {
                    foreach (var piece in allActivePieces)
                    {
                        if (piece.Position == i)
                        {
                            if (piece.PlayerId == allActivePlayers[x].Id)
                            {
                                if (x == 0)
                                {
                                    for (int u = 0; u < positionCounter; u++)
                                    {
                                        gb[i] += "1";
                                    }
                                    for (int l = positionCounter; l < 4; l++)
                                    {
                                        gb[i] += " ";
                                    }
                                    break;
                                }
                                if (x == 1)
                                {
                                    for (int u = 0; u < positionCounter; u++)
                                    {
                                        gb[i] += "2";
                                    }
                                    for (int l = positionCounter; l < 4; l++)
                                    {
                                        gb[i] += " ";
                                    }
                                    break;
                                }
                                if (x == 2)
                                {
                                    for (int u = 0; u < positionCounter; u++)
                                    {
                                        gb[i] += "3";
                                    }
                                    for (int l = positionCounter; l < 4; l++)
                                    {
                                        gb[i] += " ";
                                    }
                                    break;
                                }
                                if (x == 3)
                                {
                                    for (int u = 0; u < positionCounter; u++)
                                    {
                                        gb[i] += "4";
                                    }
                                    for (int l = positionCounter; l < 4; l++)
                                    {
                                        gb[i] += " ";
                                    }
                                    break;
                                }
                                
                                break;
                            }
                            //else
                            //{
                            //    int ii = 4 - positionCounter;
                            //    List<string> mellanrum = new List<string>(){
                            //        " ", "  ","   ","    " };

                            //    gb[i] += mellanrum[ii];
                            //}
                        }
 
                    }
                }
            }







            //if (piece.PlayerId == allActivePlayers[x].Id)
            //{
            //    if (x == 0)
            //    {
            //        Console.WriteLine("PositionCounter" + positionCounter);

            //        string p = "1";
            //        for (int y = 0; y < positionCounter; y++)
            //        {
            //            p += "1";
            //        }

            //        gb[i] = gb[i].Replace(" ", "1");
            //        break;
            //    }
            //    if (x == 1)
            //    {
            //        gb[i] = gb[i].Replace(" ", "2");
            //        break;
            //    }
            //    if (x == 2)
            //    {
            //        gb[i] = gb[i].Replace(" ", "3");
            //        break;
            //    }
            //    if (x == 3)
            //    {
            //        gb[i] = gb[i].Replace(" ", "4");
            //        break;
            //    }

            //    break;
            //}

            //Console.WriteLine($"MapIndex: {i}");


            //}

            return gb;
        }
    }
}

