﻿using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoBoard.DataAccess
{
    public class LudoDbAccess
    {
        private object db;

        // Spara det nuvarande spelet
        // TODO - Sikta på att spara varje gång något händer i spelet.  
        public void SaveGame(Game game, List<Player> players, List<Piece> pieces)
        {
            var context = new LudoDbContext();

            game.LastTimePlayedDate = DateTime.Now;

            Console.WriteLine($"Added Game Id {game.Id}, Created Date: {game.LastTimePlayedDate}, Game Completed: {game.IsCompleted}");
            context.Game.Add(game);

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"Adding Player Id {players[i].Id}, Name: {players[i].Name}, Color: {players[i].PlayerColor}");

                int gameId;
                try
                {
                    gameId = Convert.ToInt32(game.Id);
                    players[i].GameId = gameId;
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                }
                //Add player to DB
                context.Player.Add(players[i]);
            }

            for (int x = 0; x < pieces.Count; x++)
            {
                Console.WriteLine($"Adding Piece with Position: {pieces[x].Position}");
                if (pieces[x].Position == 0)
                {
                    pieces[x].PlayerId = players[0].Id;

                }
                else if (pieces[x].Position == 4)
                {
                    pieces[x].PlayerId = players[1].Id;

                }
                else if (pieces[x].Position == 56)
                {
                    pieces[x].PlayerId = players[2].Id;

                }
                else if (pieces[x].Position == 60)
                {
                    pieces[x].PlayerId = players[3].Id;

                }
                // Add piece to DB set
                context.Piece.Add(pieces[x]);
            }

            Console.WriteLine("Press a key to save to database");
            Console.ReadKey();

            // Save change in database
            context.SaveChanges();
            Console.WriteLine("Game saved to database");
            Console.ReadKey();
        }

        public List<Player> GetAllPlayers()
        {
            using (var context = new LudoDbContext())
            {
                return context.Player.ToList();
            }
        }

        public List<Piece> GetCurrentPlayersPieces(int playerId)
        {
            using (var context = new LudoDbContext())
            {
                return context.Piece.Where(x => x.PlayerId == playerId).ToList();
            }
        }

        public int GetHighestBoardId()
        {

            var context = new LudoDbContext();
            Game boardId = null;
            try
            {
                boardId = context.Game.OrderByDescending(t => t.Id).First();
                Console.WriteLine(boardId.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine($"Returning highest Id: {boardId}");
            return boardId.Id;
        }

        public int GetHighestPlayerId()
        {
            var context = new LudoDbContext();
            Player playerId = null;
            int id = 0;
            try
            {
                playerId = context.Player.OrderByDescending(t => t.Id).First();
            }
            catch (Exception)
            {
                return id;
            }

            Console.WriteLine($"Returning highest Id: {playerId.Id}");
            return playerId.Id;
        }


        public void CompleteGameAndApplyStatsToList() //Kan kanske tas bort
        {
            //Spelet är slut och lägger till stats i en lista på spelade spel
        }

        public void SaveStats()
        {

        }

        public List<Game> GetAllUnfinishedGames()
        {

            using (var context = new LudoDbContext())
            {
                return context.Game.Where(t => t.IsCompleted == false).ToList();
            }
        }

        public List<Piece> GetAllPieces()
        {
            List<Piece> pieces = new List<Piece>();
            var context = new LudoDbContext();
            try
            {
                pieces.Add((Piece)context.Piece.Where(t => t.IsActive == true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return pieces;
        }
    }
}
