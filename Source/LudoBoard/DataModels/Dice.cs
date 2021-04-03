using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public class Dice
    {        
        public int RollDice()
        {
            Board board = new Board();
            Random diceValue = new Random();
            int DiceValue = diceValue.Next(1, 7);

            // Kan behövas en loop för att slå igen när man slagit 6
            if (DiceValue == 6)
            {
                board.AskIfMoveFromNestOrMoveOnBoard();
            }

            return DiceValue;
        }
    }
}
