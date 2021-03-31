using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
	class Game
	{
		public int Id { get; set; }
		public Piece Piece { get; set; }
		public DateTime LastTimePlayedDate { get; set; } //Används vid laddning och sparning av spel
		public DateTime CompletedDate { get; set; }
		public bool IsCompleted { get; set; }
	}
}
