using System;
using System.Collections.Generic;

namespace LudoBoard.DataModels
{
    public class Game
	{
        public int Id { get; set; }
        public DateTime? LastTimePlayedDate { get; set; } 
		public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string WinnerPlayerName { get; set; } = "N/A";
        public ICollection<Player> Players { get; set; }
    }
}
