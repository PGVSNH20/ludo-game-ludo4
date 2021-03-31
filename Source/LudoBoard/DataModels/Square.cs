using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    class Square
    {
		//[]
		public int Id { get; set; }
		public bool IsOccupied { get; set; }
		public Player PlayersOnSquare { get; set; }
	}
}
