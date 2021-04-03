using System.Collections.Generic;

namespace LudoBoard.DataModels
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string PlayerColor { get; set; }
        public int? GameId { get; set; }
        public bool PlayerTurn { get; set; }
        public Game Game { get; set; }
        public ICollection<Piece> Pieces { get; set; }
    }
}
