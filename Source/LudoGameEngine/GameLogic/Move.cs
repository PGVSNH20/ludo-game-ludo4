using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public class Move
    {
        public List<Piece> MovePiece(List<Piece> piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            List<Piece> updatedPositions = new List<Piece>();
            
            int pieceId = 0;
            do
            {//TODO: Ändra id nr baserat på vilka pieces man kan flytta och ta bort dom som är i mål
                Console.WriteLine($"Which piece do you want to move? (1,2,3,4)");
                int.TryParse(Console.ReadLine(), out pieceId);
            } while (pieceId != 1 && pieceId != 2 && pieceId != 3 && pieceId != 4);
            pieceId -= 1;
            //TODO: ArgumentOutOfRange kolla om idt existerar och att det inte går över eller under
            var index = playerGameBoard.IndexOf(piece[pieceId].Position);

            index = index + diceValue;
            try
            {
                piece[pieceId].Position = playerGameBoard[index];
            }
            catch (Exception)
            {
                piece[pieceId].IsActive = false;

                if (true)
                {

                }
            }

            updatedPositions.Add(piece[pieceId]);
            // Göra en loop för att hitta listan som CheckPositions håller.
            
            var checkSomething = CheckPositions(updatedPositions[0], players);
            foreach (var c in checkSomething)
            {
                updatedPositions.Add(c);
            }
            
            return updatedPositions; 
        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public void MoveFromNestOrBoard(List<Piece> piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            //Square square = new Square();
            bool isRunning = true;
            int userInput = 0;
            GameLoop runGame = new GameLoop();
            Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);
            List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);

            while (isRunning)
            {
                Console.Clear();
                Square.CurrentBoard(players);

                //
                bool isPieceInNest = false;
                bool isPieceOnBoard = false;
				foreach (var pieces in currentPlayerPieces)
				{
					if (pieces.Position == currentPlayer.PlayerBoard[0])
					{
                        isPieceInNest = true;
					}
					else
					{
						for (int i = 1; i < currentPlayer.PlayerBoard[i]; i++)
						{
							if (pieces.Position == currentPlayer.PlayerBoard[i])
							{
                                isPieceOnBoard = true;
							}
						}
					}
                }

				if (isPieceInNest && isPieceOnBoard)
				{
                    Console.WriteLine("You rolled 6!!!! Do you want move a piece from the nest or move a piece on board?\n" +
                                    "[1] Move piece from the nest\n" +
                                    "[2] Move piece on the board\n");

                    int.TryParse(Console.ReadLine(), out userInput);
                }
				else if (isPieceInNest && !isPieceOnBoard)
				{
                    Console.WriteLine("You rolled 6!!!!\n");

                    //Här ska vi flytta en pjäs från nest:et.
                    isRunning = false;
                    Console.Clear();
                    Square.CurrentBoard(players);
                    currentPlayerPieces = MovePiece(currentPlayerPieces, 1, currentPlayer.PlayerBoard, players);

                    UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
                    Console.Clear();
                    runGame.RunGame(players);
				}
				else
				{
                    isRunning = false;
                    Console.Clear();
                    Square.CurrentBoard(players);
                    currentPlayerPieces = MovePiece(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);

                    UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
                    Console.Clear();
                    runGame.RunGame(players);
                }
                
                //TODO-Kolla till om det finns pjäser ute på brädan. Kunna ge ett felmeddelande om det inte en pjäs ute på brädan ifall man trycker på "Move piece on the board"
                //isRunning = false;
             
                switch (userInput)
                {
                    case 1:
                        //Här ska vi flytta en pjäs från nest:et.
                        isRunning = false;
                        Console.Clear();
                        Square.CurrentBoard(players);
                        currentPlayerPieces = MovePiece(currentPlayerPieces, 1, currentPlayer.PlayerBoard, players);

                        UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
                        Console.Clear();
                        runGame.RunGame(players);
                        break;

                    case 2:
                        isRunning = false;
                        Console.Clear();
                        Square.CurrentBoard(players);
                        currentPlayerPieces = MovePiece(currentPlayerPieces, diceValue, currentPlayer.PlayerBoard, players);

                        UpdateGameBoard.UpdatePlayerTurn(currentPlayerPieces, players);
                        Console.Clear();
                        runGame.RunGame(players);

                        break;

                    case 3:
                        //Square.CurrentBoard();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("You need to press 1, 2 or 3");
                        isRunning = true;
                        break;

                }
            }
        }
        public List<Piece> CheckPositions(Piece movedPiece, List<Player> players)
        {            
            var ludoDbAccess = new LudoDbAccess();
            List<Piece> updatedPieces = new List<Piece>();
            List<List<Piece>> eachPlayersPieces = new List<List<Piece>>();

            for (int i = 0; i < players.Count; i++)
            {
                eachPlayersPieces.Add(ludoDbAccess.GetCurrentPlayersPieces(players[i].Id));
            }

            List<int> nest = new List<int>();
            GameBoard gameBoard = new GameBoard();
            nest = gameBoard.nestPositions;

            for (int i = 0; i < eachPlayersPieces.Count; i++)
            {
                foreach (var piece in eachPlayersPieces[i])
                {
                    if (piece.Position == movedPiece.Position)
                    {
                        Console.WriteLine($"KNUFF!");
                        piece.Position = nest[i];
                        updatedPieces.Add(piece);
                    }
                }
            }
            return updatedPieces;
        }

        public void FinishLine()
        {
            
        }
    }
}
