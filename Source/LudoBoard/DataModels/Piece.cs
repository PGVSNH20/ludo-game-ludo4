using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public class Piece
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public Player Player { get; set; }    
    }
}
