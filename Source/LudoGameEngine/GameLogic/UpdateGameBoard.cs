using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoGameEngine.GameLogic
{
    public class UpdateGameBoard
    {

        // Start Of Gameloop, Check Player Turn
        public static Player GetPlayerTurn(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerTurn == true)
                {
                    Console.WriteLine($"It's Player {player.PlayerColor}: {player.Name}'s time to roll! ");

                    return player;
                }
            }

            return null;
        }

        // End Of Gameloop, Update Player Turn
        public static void UpdatePlayerTurn(List<Piece> pieces, List<Player> currentPlayers, int diceValue)
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int playercounter = currentPlayers.Count;

            ludoDbAccess.ChangeIsActive(pieces);
            try
            {
                int gameId = Convert.ToInt32(currentPlayers[0].GameId);
                var currentGame = ludoDbAccess.GetAllFinishedGames().Where(x => x.Id == gameId).Single();

                // Checks If Game Is Completed
                // If True, End Game.
                if (currentGame.IsCompleted == true)
                {
                    var runMenu = new UserInterface();

                    runMenu.MainMenu();
                }
            }
            catch (Exception e)
            {
                // If Game IsCompleted == False, continue next Gameloop
            }

            // Changing Player Turn
            if(diceValue != 6)
            {
                for (int i = 0; i < currentPlayers.Count; i++)
                {
                    if (currentPlayers[i].PlayerTurn == true)
                    {
                        currentPlayers[i].PlayerTurn = false;

                        if (i == playercounter - 1)
                        {
                            currentPlayers[0].PlayerTurn = true;
                            ludoDbAccess.SavePositionsToDb(pieces, currentPlayers, diceValue);
                            break;
                        }
                        else
                        {
                            currentPlayers[i + 1].PlayerTurn = true;

                            ludoDbAccess.SavePositionsToDb(pieces, currentPlayers, diceValue);
                            break;
                        }
                    }
                }
            }
            else // If diceValue == 6, Skip update player turn
            {
                ludoDbAccess.SavePositionsToDb(pieces, currentPlayers, diceValue);
            }
        }

        public static List<Piece> GetPlayerPieces(Player player)
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<Piece> playersPieces = ludoDbAccess.GetCurrentPlayersPieces(player.Id);

            return playersPieces;
        }

        // Updates all the pieces positions on the game board
        public static List<string> PiecesOnGameBoardUpdate(List<Player> allActivePlayers)
        {
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<string> gb = new GameBoard().CompleteGameBoard;

            // Get all player, and put their pieces to individual lists (up to 4 lists)
            // list[0] == player 1, list[1] == player 2... and so on.
            List<List<Piece>> eachPlayersPieces = new List<List<Piece>>();
            for (int i = 0; i < allActivePlayers.Count; i++)
            {
                eachPlayersPieces.Add(ludoDbAccess.GetCurrentPlayersPieces(allActivePlayers[i].Id));
            }

            // Checking each square on the game board
            for (int i = 0; i < gb.Count; i++)
            {
                // Set counter, (can only be 4 pieces on each square)
                int counter = 4;

                // Checking each player
                for (int x = 0; x < eachPlayersPieces.Count; x++)
                {
                    int pieceCounter = 0; // Piece counter
                                          // This is to see what piece it is.. "1", "2", "3" or "4"

                    foreach (var piece in eachPlayersPieces[x]) // Looping each player pieces (each player has x4 pieces)
                    {
                        pieceCounter += 1;

                        if (piece.Position == i)
                        {
                            counter -= 1;

                            if (x == 0) // Player 1
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1".Red();
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2".Red();
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3".Red();
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4".Red();
                                }

                            }

                            else if (x == 1) // Player 2
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1".Blue();
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2".Blue();
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3".Blue();
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4".Blue();
                                }
                            }
                            else if (x == 2) // Player 3
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1".Yellow();
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2".Yellow();
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3".Yellow();
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4".Yellow();
                                }
                            }
                            else if (x == 3) // Player 4
                            {
                                if (pieceCounter == 1) // piece Id
                                {
                                    gb[i] = gb[i] + "1".Green();
                                }
                                else if (pieceCounter == 2)
                                {
                                    gb[i] = gb[i] + "2".Green();
                                }
                                else if (pieceCounter == 3)
                                {
                                    gb[i] = gb[i] + "3".Green();
                                }
                                else if (pieceCounter == 4)
                                {
                                    gb[i] = gb[i] + "4".Green();
                                }
                            }
                        }
                    }
                }

                // If there's no piece, fill square with blanks
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

