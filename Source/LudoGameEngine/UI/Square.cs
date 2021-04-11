using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.UI
{
    // This class should be controlling all the movement
    // so that CurrentBoard() can print out each piece location.
    public static class Square
    {

        public static List<string> CurrentBoard(List<Player> allPlayersInGame)
        {
            List<string> updated = new List<string>();
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            List<Piece> pieces = ludoDbAccess.GetAllPieces(allPlayersInGame);

            var gb = UpdateGameBoard.PiecesOnGameBoardUpdate(allPlayersInGame);

            //Console.WriteLine($"\n[{gb[0]}]                  [{gb[1]}][{gb[2]}][{gb[3]}]                  [{gb[4]}]\n" +
            //                  $"                        [{gb[5]}][{gb[6]}][{gb[7]}]\n" +
            //                  $"                        [{gb[8]}][{gb[9]}][{gb[10]}]\n" +
            //                  $"                        [{gb[11]}][{gb[12]}][{gb[13]}]\n" +
            //                  $"[{gb[14]}][{gb[15]}][{gb[16]}][{gb[17]}][{gb[18]}][{gb[19]}][{gb[20]}][{gb[21]}][{gb[22]}][{gb[23]}][{gb[24]}]\n" +
            //                  $"[{gb[25]}][{gb[26]}][{gb[27]}][{gb[28]}][{gb[29]}][{gb[30]}][{gb[31]}][{gb[32]}][{gb[33]}][{gb[34]}][{gb[35]}]\n" +
            //                  $"[{gb[36]}][{gb[37]}][{gb[38]}][{gb[39]}][{gb[40]}][{gb[41]}][{gb[42]}][{gb[43]}][{gb[44]}][{gb[45]}][{gb[46]}]\n" +
            //                  $"                        [{gb[47]}][{gb[48]}][{gb[49]}]\n" +
            //                  $"                        [{gb[50]}][{gb[51]}][{gb[52]}]\n" +
            //                  $"                        [{gb[53]}][{gb[54]}][{gb[55]}]\n" +
            //                  $"[{gb[56]}]                  [{gb[57]}][{gb[58]}][{gb[59]}]                  [{gb[60]}]\n");

            Console.WriteLine("\n[".Red() + gb[0] + "]".Red() + "                  [" + gb[1] + "][" + gb[2] + "][" + gb[3] + "]                  " + "[".Blue() + gb[4] + "]\n".Blue() +
                              "                        [" + gb[5] + "]" + "[".Blue() + gb[6] + "]".Blue() + "[" + gb[7] + "]\n" +
                              "                        [" + gb[8] + "]" + "[".Blue() + gb[9] + "]".Blue() + "[" + gb[10] + "]\n" +
                              "                        [" + gb[11] + "]" + "[".Blue() + gb[12] + "]".Blue() + "[" + gb[13] + "]\n" +
                              "[" + gb[14] + "][" + gb[15] + "][" + gb[16] + "][" + gb[17] + "][" + gb[18] + "]" + "[".Blue() + gb[19] + "]".Blue() + "[" + gb[20] + "][" + gb[21] + "][" + gb[22] + "][" + gb[23] + "][" + gb[24] + "]\n" +
                              "[" + gb[25] + "]" + "[".Red() + gb[26] + "]".Red() + "[".Red() + gb[27] + "]".Red() + "[".Red() + gb[28] + "]".Red() + "[".Red() + gb[29] + "]".Red() + "[".Black() + gb[30] + "]".Black() + "[".Yellow() + gb[31] + "]".Yellow() + "[".Yellow() + gb[32] + "]".Yellow() + "[".Yellow() + gb[33] + "]".Yellow() + "[".Yellow() + gb[34] + "]".Yellow() + "[" + gb[35] + "]\n" +
                              "[" + gb[36] + "][" + gb[37] + "][" + gb[38] + "][" + gb[39] + "][" + gb[40] + "]" + "[".Green() + gb[41] + "]".Green() + "[" + gb[42] + "][" + gb[43] + "][" + gb[44] + "][" + gb[45] + "][" + gb[46] + "]\n" +
                              "                        [" + gb[47] + "]" + "[".Green() + gb[48] + "]".Green() + "[" + gb[49] + "]\n" +
                              "                        [" + gb[50] + "]" + "[".Green() + gb[51] + "]".Green() + "[" + gb[52] + "]\n" +
                              "                        [" + gb[53] + "]" + "[".Green() + gb[54] + "]".Green() + "[" + gb[55] + "]\n" +
                              "[".Green() + gb[56] + "]".Green() + "                  [" + gb[57] + "][" + gb[58] + "][" + gb[59] + "]                  " + "[".Yellow() + gb[60] + "]\n".Yellow() + "\n");
                             


            return updated;
        }
    }
}
