namespace LudoBoard.DataModels
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string PlayerColor { get; set; }
        public Game GameId { get; set; }
	}
}
