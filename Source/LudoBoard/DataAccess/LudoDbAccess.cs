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
                
                // Add player to DB
                context.Player.Add(players[i]);
            }

            for (int x = 0; x < pieces.Count; x++)
            {
                Console.WriteLine($"Adding piece Id: {pieces[x].Id}, Position: {pieces[x].Position}");
                
                // Add piece to DB set
                context.Piece.Add(pieces[x]);
            }

            Console.WriteLine("Press a key to save to database");
            Console.ReadKey();

            // Save change in database
            context.SaveChanges();
            Console.WriteLine("Order saved to database");
        }
        public int GetHigestBoardId()
        {
            var context = new LudoDbContext();
            int boardId;
            try
            {
                boardId = Convert.ToInt32(context.Game.OrderByDescending(t => t.Id).First());
            }
            catch (Exception)
            {
                boardId = 0;
            }

            Console.WriteLine($"Returning highest Id: {boardId}");
            Console.ReadLine();
            return boardId;
        }

        public int GetHigestPlayerId()
        {
            var context = new LudoDbContext();
            int playerId;
            try
            {
                playerId = Convert.ToInt32(context.Player.OrderByDescending(t => t.Id).First());
            }
            catch (Exception)
            {
                playerId = 0;
            }

            Console.WriteLine($"Returning highest Id: {playerId}");
            Console.ReadLine();
            return playerId;
        }


        public void CompleteGameAndApplyStatsToList() //Kan kanske tas bort
		{
            //Spelet är slut och lägger till stats i en lista på spelade spel
		}

        public void SaveStats()
		{

		}

        public void LoadStats()
		{

		}
    }
}
