using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public class Dice
    {        
        public int RollDice()   
        {            
            Random diceValue = new Random();
            int DiceValue = diceValue.Next(1, 7);

            return DiceValue;
        }
    }
}
