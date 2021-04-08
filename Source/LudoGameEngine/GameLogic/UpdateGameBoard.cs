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
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<string> gb = new GameBoard().CompleteGameBoard;

            // Tar ut alla spelares pjäser till 4 olika listor 0 = spelare 1, 1 = spelare 2 osv.
            List<List<Piece>> PlayersGameBoards = new List<List<Piece>>();
            for (int i = 0; i < allActivePlayers.Count; i++)
            {
                PlayersGameBoards.Add(ludoDbAccess.GetCurrentPlayersPieces(allActivePlayers[i].Id));
            }

            // Går igenom varje fyrkant på kartan
            for (int i = 0; i < gb.Count; i++)
            {
                // Sätter en counter (Kan bara vara max 4 pieces på en ruta)
                int counter = 4;

                for (int x = 0; x < PlayersGameBoards.Count; x++) // Går igenom varje spelare
                {
                    int pieceCounter = 0; // Värdet på denna counter är id 1, 2 ,3 ,4 på en spelares pjäser.
                                          // Detta för att vi ska kunna skriva ut "1", "2", "3" eller "4" på kartan.

                    foreach (var piece in PlayersGameBoards[x]) // Går igenom varje spelares pjäser (4st)
                    {
                        pieceCounter += 1;

                        if (piece.Position == i)
                        {
                            counter -= 1;

                            if (x == 0) // Player 1
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1";
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2";
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3";
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4";
                                }
                            }
                            else if (x == 1) // Player 2
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1";
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2";
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3";
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4";
                                }
                            }
                            else if (x == 2) // Player 3
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1";
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2";
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3";
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4";
                                }
                            }
                            else if (x == 3) // Player 4
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1";
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2";
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3";
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4";
                                }
                            }
                        }
                    }
                }

                while (counter != 0)
                {
                    gb[i] = gb[i] + " ";
                    counter -= 1;
                }
            }

            return gb;
        }
    }
}

