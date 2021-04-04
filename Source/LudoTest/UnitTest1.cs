using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using System;
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
            int expected = 5;

            // Act - Vad ska testet göra, en metod som får ut ett resultat?
            int result = ludoDbAccess.GetHigestBoardId();

            // Assert - Vad vill vi att resultatet ska bli?
            Assert.Equal(expected, result);
        }
        //[Fact]    
        //public void Test1()
        //{
        //    // Arange - lägg upp testet genom att ta in variabler osv.

        //    // Act - Vad ska testet göra, en metod som får ut ett resultat?

        //    // Assert - Vad vill vi att resultatet ska bli?
            
        //}

        //// EXEMPEL
        //// Så här kan man göra om man vill testa en funktion med flera olika inparametrar.
        //// Detta gör vi för att motverka redundans
        //[Theory]
        //[InlineData(5, 2, 3)]
        //[InlineData(0, 5, -5)]
        //public void Test2(int expected, int x, int y)
        //{
        //    // Arange - lägg upp testet genom att ta in variabler osv.

        //    // Act - Vad ska testet göra, en metod som får ut ett resultat?

        //    // Assert - Vad vill vi att resultatet ska bli?

        //}

    }
}
