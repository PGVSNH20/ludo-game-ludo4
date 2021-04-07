using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.Initialize
{
    public class CreateGame
    {
        private static int userInput = 0;
        private static bool isRunning = true;


        /// <summary>
        /// INITIALIZE GAME
        /// </summary>
        public void CreateNewGame()
        {
            // Creating a new board
            Game board = new Game();
            GameLoop gameLoop = new GameLoop();

            // Creating amount of players
            Console.WriteLine("How many players? (2-4)");

            List<Player> player = new List<Player>();
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int highestId = ludoDbAccess.GetHighestPlayerId();
            GameBoard gameBoards = new GameBoard();

            // Checking the amount of players
            while (isRunning)
            {
                int.TryParse(Console.ReadLine(), out userInput);

                if (userInput > 1 && userInput < 5)
                {
                    for (int i = 0; i < userInput; i++)
                    {
                        player.Add(new Player() {
                            Id = highestId + i + 1,
                            PlayerBoard = gameBoards.PlayersGameBoards[i]
                        });                
                    }

                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            CreatePlayer(board, player);
            gameLoop.RunGame(player);
        }

        private static void CreatePlayer(Game board, List<Player> player)
        {
            // Slumpar fram vem som startar.
            Random randomStartPlayer = new Random();
            int playerId = randomStartPlayer.Next(1, player.Count);

            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow" };
            using (var context = new LudoDbContext())
            {
                for (int i = 0; i < player.Count; i++)
                {
                    // SET Player Color
                    player[i].PlayerColor = colors[i];

                    // SET Player Name
                    bool isRunning = true;
                    while (isRunning)
                    {
                        Console.Write($"\nPlayer {i + 1} Name: ");
                        player[i].Name = Console.ReadLine().ToString();

                        bool containsInt = player[i].Name.Any(char.IsDigit);

                        if (containsInt == true)
                        {
                            Console.Write("No numbers as a name, try again.");
                        }
                        else if (player[i].Name == String.Empty)
                        {
                            Console.WriteLine("Please enter a name..");
                        }
                        else
                        {
                            Console.WriteLine($"Added Player {i + 1} | Name: {player[i].Name} | Color: {player[i].PlayerColor} |");
                            isRunning = false;
                        }
                    }

                    if (playerId == i + 1)
                    {
                        player[i].PlayerTurn = true;
                        Console.WriteLine($"{player[i].Name} starts.");
                    }
                }
            }

            CreatePieces(board, player);
        }

        private static void CreatePieces(Game board, List<Player> player)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            // Board Nest Positions @Current map.
            List<int> nestPositions = new List<int> { 0, 4, 56, 60 };

            // SET Piece Id, Position
            List<Piece> piece = new List<Piece>();
            var counter = 0;
            for (int i = 0; i < player.Count; i++)
            {
                piece.Add(new Piece());
                piece.Add(new Piece());
                piece.Add(new Piece());
                piece.Add(new Piece());

                for (int x = 0; x < 4; x++)
                {
                    piece[counter + x].Position = nestPositions[i];
                    piece[counter + x].IsActive = true;
                }
                counter += 4;
            }

            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            ludoDbAccess.SaveGame(board, player, piece);

        }

        public void ContinueGame()
        {
            // TODO - Här måste vi hämta GetHighestBoardId för att fortsätta spela klart senast sparade spelet
            //Ladda senast sparade spelet
            //LoadGame();
        }

        


        /// <summary>
        /// GAME LOGIC
        /// </summary>
        public void MovePiece(List<Piece> piece, int diceValue, List<int> playerGameBoard)
        {
            int pieceId = 0;
            Console.WriteLine("Which piece do you want to move? (Id)");
            int.TryParse(Console.ReadLine(), out pieceId);

            var index = playerGameBoard.IndexOf(piece[pieceId].Position);
            Console.WriteLine($"\nPiece (id = {pieceId}) had GameBoard-index: {index} | GameBoard-value (position) was: {piece[pieceId].Position}!");

            index = index + diceValue;
            piece[pieceId].Position = playerGameBoard[index];
            Console.WriteLine($"Updated new GameBoard-index: {index} | new GameBoard-value (position)is {piece[pieceId].Position}!\n");

            // TODO - Kanske skapa en metod i LudoDbAccess för att uppdatera databasen.
            // Spara Piece.position
            // Uppdatera Player.PlayerTurn
        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public void AskIfMoveFromNestOrMoveOnBoard(List<Piece> piece, int diceValue, List<int> playerGameBoard)
        {
            Square square = new Square();
            bool isRunning = true;
            int userInput = 0;

            while (isRunning)
            {
                Console.WriteLine("Do you want move a piece from the nest or move a piece on board?\n" +
                                    "[1] Move piece from the nest\n" +
                                    "[2] Move piece on the board\n" +
                                    "[3] View current board");

                int.TryParse(Console.ReadLine(), out userInput);
                //TODO-Kolla till om det finns pjäser ute på brädan. Kunna ge ett felmeddelande om det inte en pjäs ute på brädan ifall man trycker på "Move piece on the board"
                isRunning = false;
                switch (userInput)
                {
                    case 1:
                        //Här ska vi flytta en pjäs från nest:et.
                        isRunning = false;
                        break;

                    case 2:
                        MovePiece(piece, diceValue, playerGameBoard);
                        isRunning = false;
                        break;

                    case 3:
                        square.CurrentBoard();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("You need to press 1, 2 or 3");
                        isRunning = true;
                        break;

                }
            }
        }

        //TODO-Programmet ska avgöra vems tur det är att kasta tärningen. WhoesTurnToRollTheDice(); 
        public void WhoesTurnToRollTheDice()
        {

        }
    }
}
