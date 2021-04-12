using Edokan.KaiZen.Colors;
using LudoGameEngine.UI;

namespace LudoGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var runMenu = new UserInterface();
            EscapeSequencer.Install(); //Install sequence parse
            EscapeSequencer.Bold = true; //Brighter colors

            runMenu.MainMenu();
        }
    }
}
