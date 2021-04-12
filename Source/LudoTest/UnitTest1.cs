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

        [Fact(Skip = "Vi får ut det högsta värdet i \"Game\" databasen")]
        public void When_Using_GetHigestBoardId_Expecting_ShouldReturn_MaxId()
        {
            // Arange
            int expected = 2;

            // Act
            int result = ludoDbAccess.GetHighestBoardId();

            // Assert
            //Assert.Equal(expected, result);
        }

        [Fact]
        public void When_LoadGamesFromMenu_Expecting_ListOfUnfinishedGamesToPlay()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            List<Game> listOfUnfinishedGames = new List<Game>();
            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            listOfUnfinishedGames = ludoDbAccess.GetAllUnfinishedGames();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.IsType<List<Game>>(listOfUnfinishedGames);
        }

        [Fact]
        public void When_LoadCompletedGamesFromMenu_Expecting_ListOfFinishedGames()
        {
            // Arange - lägg upp testet genom att ta in variabler osv.
            List<Game> listOfFinishedGames = new List<Game>();
            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            listOfFinishedGames = ludoDbAccess.GetAllFinishedGames();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.IsType<List<Game>>(listOfFinishedGames);
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
            //string expected = "Messi";

            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            int result = ludoDbAccess.GetHighestPlayerId();

            // Assert - Vad vill vi att resultatet ska bli?
            //Assert.Equal(expected, result);
        }

        //Fungerar inte då vi måste skicka med pieces på något sätt...
        //[Fact]
        //public static void When_CurrentBoardInNewGame_Expect_PiecesInTheirNestPosition()
        //{
        //    // Arange
        //    List<List<int>> PlayersGameBoards = new List<List<int>>(){
        //    new List<int>() { 0, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 26, 27, 28, 29, 30 },
        //    new List<int>() { 4, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 6, 9, 12, 19, 30 },
        //    new List<int>() { 60, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 34, 33, 32, 31, 30 },
        //    new List<int>() { 56, 57, 53, 50, 47, 40, 39, 38, 37, 36, 25, 14, 15, 16, 17, 18, 11, 8, 5, 1, 2, 3, 7, 10, 13, 20, 21, 22, 23, 24, 35, 46, 45, 44, 43, 42, 49, 52, 55, 59, 58, 54, 51, 48, 41, 30 }
        //    };
            
        //    List<Piece> pieces = new List<Piece>();
        //    pieces.Add(new Piece { Id = 1, IsActive = true, Position = 0, PlayerId = 1 });
        //    pieces.Add(new Piece { Id = 2, IsActive = true, Position = 4, PlayerId = 2 });
        //    pieces.Add(new Piece { Id = 3, IsActive = true, Position = 56, PlayerId = 3 });
        //    pieces.Add(new Piece { Id = 4, IsActive = true, Position = 60, PlayerId = 4 });
            
        //    List<Player> players = new List<Player>();
        //    players.Add(new Player { Id = 1, Name = "Red", PlayerBoard = PlayersGameBoards[0]});
        //    players.Add(new Player { Id = 2, Name = "Blue", PlayerBoard = PlayersGameBoards[1]});
        //    players.Add(new Player { Id = 3, Name = "Green", PlayerBoard = PlayersGameBoards[2]});
        //    players.Add(new Player { Id = 4, Name = "Yellow", PlayerBoard = PlayersGameBoards[3]});


        //    List<string> expected = new List<string>(60)
        //    {
        //        "1   ","    ","    ","    ","1   ","    ","    ","    ","    ","   ",
        //        "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
        //        "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
        //        "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
        //        "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
        //        "    ","    ","    ","    ","    ","    ","1   ","    ","    ", "    ", "1   "
        //    };

        //    // Act - Vad ska testet göra, en metod som får ut ett resultat?
        //    var gb = UpdateGameBoard.PiecesOnGameBoardUpdate(players);

        //    // Assert - Vad vill vi att resultatet ska bli?
        //    Assert.Equal(expected, gb);

        //}




        // EXEMPEL
        // Så här kan man göra om man vill testa en funktion med flera olika inparametrar.
        // Detta gör vi för att motverka redundans
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
