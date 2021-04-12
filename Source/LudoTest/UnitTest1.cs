using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine;
using LudoGameEngine.GameLogic;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace LudoTest
{
    public class UnitTest1
    {
        LudoDbAccess ludoDbAccess = new LudoDbAccess();

        //[Fact(Skip = "Vi f�r ut det h�gsta v�rdet i \"Game\" databasen")]
        [Fact]
        public void When_Using_GetHigestBoardId_Expecting_ShouldReturn_MaxId()
        {
            // Arange
            int expected = 2;

            // Act
            int result = ludoDbAccess.GetHighestBoardId();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void When_LoadGamesFromMenu_Expecting_ListOfUnfinishedGamesToPlay()
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.
            List<Game> listOfUnfinishedGames = new List<Game>();
            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?
            listOfUnfinishedGames = ludoDbAccess.GetAllUnfinishedGames();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.IsType<List<Game>>(listOfUnfinishedGames);
        }

        [Fact]
        public void When_LoadCompletedGamesFromMenu_Expecting_ListOfFinishedGames()
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.
            List<Game> listOfFinishedGames = new List<Game>();
            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?
            listOfFinishedGames = ludoDbAccess.GetAllFinishedGames();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.IsType<List<Game>>(listOfFinishedGames);
        }

        [Fact]
        public void When_RollDice_ExpectNumberBetween_1_And_6()
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.
            Dice dice = new Dice();
            int result = 0;
            
            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?
            result = dice.RollDice();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.True(result >= 0 && result <= 6);
        }

        //[Fact (Skip = "Vi f�r ut namnet p� den h�gsta v�rdet i \"player\" databasen")]
        [Fact]
        public void When_Using_GetHigestPlayerId_Expecting_ShouldReturn_Name_Messi()
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int expected = 8;

            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?
            int result = ludoDbAccess.GetHighestPlayerId();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.Equal(expected, result);
        }

        // EXEMPEL
        // S� h�r kan man g�ra om man vill testa en funktion med flera olika inparametrar.
        // Detta g�r vi f�r att motverka redundans
        //[Theory]
        //[InlineData(5, 2, 3)]
        //[InlineData(0, 5, -5)]
        //public void Test2(int expected, int x, int y)
        //{
        //    // Arange - your objects, creating and setting them up as necessary.

        //    // Act - on an object.

        //    // Assert - that something is as expected.

        //}
    }
}
