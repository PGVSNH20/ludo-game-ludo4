namespace LudoBoard.DataModels
{
    public class Piece
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int? PlayerId { get; set; } 
        public Player Player { get; set; }

        public bool IsActive { get; set; }
    }
}
