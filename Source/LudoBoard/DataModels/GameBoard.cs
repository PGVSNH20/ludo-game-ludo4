using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{
    public class GameBoard
    {
        // Board Nest Positions @Current map.
        public readonly List<int> nestPositions = new List<int> { 0, 4, 60, 56 };

        //46 squares total (0-45 index)
        // Each Players Game Board
        public readonly List<List<int>> PlayersGameBoards = new List<List<int>>(){ 
        new List<int>() { 0, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 26, 27, 28, 29, 30 },
        new List<int>() { 4, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 6, 9, 12, 19, 30 },
        new List<int>() { 60, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 34, 33, 32, 31, 30 },
        new List<int>() { 56, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 54, 51, 48, 41, 30 }
        };
        // The whole Game Board
        public List<string> CompleteGameBoard = new List<string>(61)
            {
                "","","","","","","","","","","","","","","","","","","","",
                "","","","","","","","","","","","","","","","","","","","",
                "","","","","","","","","","","","","","","","","","","", "", "", ""
            };
    }
}
