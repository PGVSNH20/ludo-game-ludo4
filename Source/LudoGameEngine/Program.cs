using LudoBoard.DataAccess;

namespace LudoGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //var runMenu = new UserInterface();

            //runMenu.MainMenu();

            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            ludoDbAccess.GetHigestBoardId();
        }
    }
}
