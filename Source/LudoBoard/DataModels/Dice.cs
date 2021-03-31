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
            Random diceValue = new Random();
            int DiceValue = diceValue.Next(1, 7);

            if (DiceValue == 6)
            {
                Board.AskIfMoveFromNestOrMoveOnBoard();
            }

            return DiceValue;
        }
    }
}
