using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoBoard.DataAccess
{
    public class LudoDbAccess
    {
        LudoDbContext context = new LudoDbContext();

        public void SaveGame(Game game, List<Player> players, List<Piece> pieces)
        {

            game.LastTimePlayedDate = DateTime.Now;

            Console.WriteLine($"Created Date: {game.LastTimePlayedDate}, Game Completed: {game.IsCompleted}\n");
            context.Game.Add(game);

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"Adding Player Id {players[i].Id}, Name: {players[i].Name}, Color: {players[i].PlayerColor}\n");

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
                if (pieces[x].Position == 0)
                {
                    pieces[x].PlayerId = players[0].Id;

                }
                else if (pieces[x].Position == 4)
                {
                    pieces[x].PlayerId = players[1].Id;

                }
                else if (pieces[x].Position == 60)
                {
                    pieces[x].PlayerId = players[2].Id;

                }
                else if (pieces[x].Position == 56)
                {
                    pieces[x].PlayerId = players[3].Id;

                }

                // Add piece to DB set
                context.Piece.Add(pieces[x]);
            }

            // Save change in database
            context.SaveChanges();
            Console.WriteLine("Game saved to database");
        }

        public void ChangeIsActive(List<Piece> pieces)
        {
            

            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].IsActive == false)
                {
                    int pieceId = Convert.ToInt32(pieces[i].Id);
                    var allActive = context.Piece.Where(x => x.Id == pieceId).Single();
                    allActive.IsActive = false;
                    context.SaveChanges();
                }
            }
        }

        public void SavePositionsToDb(List<Piece> pieces, List<Player> players)
        {
            //using (var context = new LudoDbContext())
            {
                List<Piece> playerPieces = new List<Piece>();
                for (int i = 0; i < pieces.Count; i++)
                {

                    int id = Convert.ToInt32(pieces[i].Id);
                    var piece = context.Piece.Where(x => x.Id == id).Single();

                    piece.Position = pieces[i].Position;

                    playerPieces.Add(piece);
                }

                List<Player> allPlayers = new List<Player>();
                for (int z = 0; z < players.Count; z++)
                {

                    int id = Convert.ToInt32(players[z].Id);
                    var player = context.Player.Where(x => x.Id == id).Single();

                    player.PlayerTurn = players[z].PlayerTurn;

                    allPlayers.Add(player);
                }

                context.SaveChanges();
            }
        }

        public List<Player> GetAllPlayers()
        {

            return context.Player.ToList();
        }

        public List<Player> GetPlayersWhenLoadingGame(int gameId)
        {

            return context.Player.Where(x => x.GameId == gameId).ToList();
        }

        public List<Piece> GetCurrentPlayersPieces(int playerId)
        {

            return context.Piece.Where(x => x.PlayerId == playerId && x.IsActive == true).ToList();
        }

        public int GetHighestBoardId()
        {

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
            return boardId.Id;
        }

        public int GetHighestPlayerId()
        {

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
            return playerId.Id;
        }

        public List<Game> GetAllFinishedGames()
        {

            return context.Game.Where(t => t.IsCompleted == true).ToList();
        }

        public List<Game> GetAllUnfinishedGames()
        {

            return context.Game.Where(t => t.IsCompleted == false).ToList();
        }

        // IsActive = Kollar om pjäsen är aktiv på brädet eller om den har gått i mål.
        public List<Piece> GetAllPieces(List<Player> playersCurrentlyPlaying)
        {
            List<Piece> playersPieces = new List<Piece>();

            for (int i = 0; i < playersCurrentlyPlaying.Count; i++)
            {
                int id = Convert.ToInt32(playersCurrentlyPlaying[i].Id);

                var playersActivePieces = context.Piece.Where(t => t.IsActive == true && t.PlayerId == id).ToList();

                foreach (var pp in playersActivePieces)
                {
                    playersPieces.Add(pp);
                }
            }

            return playersPieces;
        }

        public void UpdatePlayerTurn(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].PlayerTurn == true)
                {
                    players[i].PlayerTurn = false;
                    players[i + 1].PlayerTurn = true;
                    break;
                }
            }
        }

        public void SetWinner(Player player)
        {

            var game = context.Game.Where(x => x.Id == player.GameId).Single();

            game.WinnerPlayerName = player.Name;
            game.IsCompleted = true;
            game.CompletedDate = DateTime.Now;

            // Save change in database
            context.SaveChanges();

            Console.WriteLine($"PLAYER {player.PlayerColor.ToUpper()} {player.Name.ToUpper()} HAS WON!");
        }
    }
}
