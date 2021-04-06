using LudoBoard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoBoard.DataModels
{
    public class Game
	{
        public int Id { get; set; }
        public DateTime? LastTimePlayedDate { get; set; } //Används vid laddning och sparning av spel
		public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string WinnerPlayerName { get; set; } = "N/A";
        public ICollection<Player> Players { get; set; }
    }
}
