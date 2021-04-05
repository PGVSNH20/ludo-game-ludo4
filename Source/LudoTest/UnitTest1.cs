using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine;
using System;
using System.Collections.Generic;
using Xunit;

namespace LudoTest
{
    public class UnitTest1
    {
        [Fact]
        public void When_Using_GetHigestBoardId_Expecting_ShouldReturn_MaxId()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int expected = 2;

            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            int result = ludoDbAccess.GetHighestBoardId();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.Equal(expected, result);
        }

        [Fact]
        public void When_LoadGamesFromMenu_Expecting_ListOfUnfinishedGamesToPlay()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.

            // Act - Vad ska testet göra, en metod som får ut ett resultat?

            // Assert - Vad vill vi att resultatet ska bli?
        }

        [Fact]
        public void When_RollDice_ExpectNumberBetween_1_And_6()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            Dice dice = new Dice();
            int result = 0;
            
            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            result = dice.RollDice();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.True(result >= 0 && result <= 6);

        }

        [Fact (Skip = "Vi får ut namnet på den högsta värdet i \"player\" databasen")]
        public void When_Using_GetHigestPlayerId_Expecting_ShouldReturn_Name_Messi()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            string expected = "Messi";

            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            int result = ludoDbAccess.GetHighestPlayerId();

            // Assert - Vad vill vi att resultatet ska bli?
            //Assert.Equal(expected, result);
        }

        [Fact]
        public void When_CurrentBoard_Expect_PiecesToTheirPosition_ShouldReturn_UpdatedBoard()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            Square square = new Square();

            List<string> expected = new List<string>(60)
            {
                "1234","    ","    ","    ","1234","    ","    ","    ","    ","   ",
                "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                "    ","    ","    ","    ","    ","    ","1234","    ","    ", "    ", "1234"
            };
            List<string> result = new List<string>();

            Console.WriteLine($"[{expected[0]}]                  [{expected[1]}][{expected[2]}][{expected[3]}]                  [{expected[4]}]\n" +
                             $"                        [{expected[5]}][{expected[6]}][{expected[7]}]\n" +
                             $"                        [{expected[8]}][{expected[9]}][{expected[10]}]\n" +
                             $"                        [{expected[11]}][{expected[12]}][{expected[13]}]\n" +
                             $"[{expected[14]}][{expected[15]}][{expected[16]}][{expected[17]}][{expected[18]}][{expected[19]}][{expected[20]}][{expected[21]}][{expected[22]}][{expected[23]}][{expected[24]}]\n" +
                             $"[{expected[25]}][{expected[26]}][{expected[27]}][{expected[28]}][{expected[29]}][{expected[30]}][{expected[31]}][{expected[32]}][{expected[33]}][{expected[34]}][{expected[35]}]\n" +
                             $"[{expected[36]}][{expected[37]}][{expected[38]}][{expected[39]}][{expected[40]}][{expected[41]}][{expected[42]}][{expected[43]}][{expected[44]}][{expected[45]}][{expected[46]}]\n" +
                             $"                        [{expected[47]}][{expected[48]}][{expected[49]}]\n" +
                             $"                        [{expected[50]}][{expected[51]}][{expected[52]}]\n" +
                             $"                        [{expected[53]}][{expected[54]}][{expected[55]}]\n" +
                             $"[{expected[56]}]                  [{expected[57]}][{expected[58]}][{expected[59]}]                  [{expected[60]}]\n");

            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            result = square.CurrentBoard();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.Equal(expected, result);

        }




        // EXEMPEL
        // Så här kan man göra om man vill testa en funktion med flera olika inparametrar.
        // Detta gör vi för att motverka redundans
        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(0, 5, -5)]
        public void Test2(int expected, int x, int y)
        {
            // Arange - lägg upp testet genom att ta in variabler osv.

            // Act - Vad ska testet göra, en metod som får ut ett resultat?

            // Assert - Vad vill vi att resultatet ska bli?

        }

    }
}
