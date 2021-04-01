using LudoBoard.DataModels;
using System;

namespace LudoGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var runMenu = new UserInterface();

            runMenu.MainMenu();


            //var rolledDice = new Dice();
            //rolledDice.RollDice();

            //Console.WriteLine($"{rolledDice.RollDice()}");


            List<string> gb = new List<string>(60) {"    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ", "    ", "    "};


            Console.WriteLine($"[{gb[0]}]                  [{gb[1]}][{gb[2]}][{gb[3]}]                  [{gb[4]}]\n" +
                              $"                        [{gb[5]}][{gb[6]}][{gb[7]}]\n" +
                              $"                        [{gb[8]}][{gb[9]}][{gb[10]}]\n" +
                              $"                        [{gb[11]}][{gb[12]}][{gb[13]}]\n" +
                              $"[{gb[14]}][{gb[15]}][{gb[16]}][{gb[17]}][{gb[18]}][{gb[19]}][{gb[20]}][{gb[21]}][{gb[22]}][{gb[23]}][{gb[24]}]\n" +
                              $"[{gb[25]}][{gb[26]}][{gb[27]}][{gb[28]}][{gb[29]}][{gb[30]}][{gb[31]}][{gb[32]}][{gb[33]}][{gb[34]}][{gb[35]}]\n" +
                              $"[{gb[36]}][{gb[37]}][{gb[38]}][{gb[39]}][{gb[40]}][{gb[41]}][{gb[42]}][{gb[43]}][{gb[44]}][{gb[45]}][{gb[46]}]\n" +
                              $"                        [{gb[47]}][{gb[48]}][{gb[49]}]\n" +
                              $"                        [{gb[50]}][{gb[51]}][{gb[52]}]\n" +
                              $"                        [{gb[53]}][{gb[54]}][{gb[55]}]\n" +
                              $"[{gb[56]}]                  [{gb[57]}][{gb[58]}][{gb[59]}]                  [{gb[60]}]\n");

        }
    }
}
