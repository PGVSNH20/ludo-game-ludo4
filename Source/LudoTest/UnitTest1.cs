using System;
using Xunit;

namespace LudoTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.

            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?

            // Assert - Vad vill vi att resultatet ska bli?
            
        }

        // EXEMPEL
        // S� h�r kan man g�ra om man vill testa en funktion med flera olika inparametrar.
        // Detta g�r vi f�r att motverka redundans
        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(0, 5, -5)]
        public void Test2(int expected, int x, int y)
        {
            // Arange - l�gg upp testet genom att ta in variabler osv.

            // Act - Vad ska testet g�ra, en metod som f�r ut ett resultat?

            // Assert - Vad vill vi att resultatet ska bli?

        }
    }
}
