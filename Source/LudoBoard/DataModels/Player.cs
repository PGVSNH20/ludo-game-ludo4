using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public Color PlayerColor { get; set; }
		public bool IsWinner { get; set; }
	}
}
