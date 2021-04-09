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
        public List<Piece> MovePiece(Piece piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            List<Piece> updatedPositions = new List<Piece>();

            //int pieceId = 0;
            //do
            //{
            //    pieceId = 0;
            //    Console.WriteLine($"Which piece do you want to move? (1,2,3,4)");
            //    int.TryParse(Console.ReadLine(), out pieceId);

            //} while (pieceId < 1 || pieceId > 4);

            //pieceId -= 1;
            var index = playerGameBoard.IndexOf(piece.Position);

            index = index + diceValue;

            // TODO - Här måste vi göra en check så att position inte överstiger position 30 (sista steget)
            try
            {
                piece.Position = playerGameBoard[index];

                if (piece.Position == playerGameBoard[45])
                {
                    piece.IsActive = false;
                }

                updatedPositions.Add(piece);
            }
            catch (Exception e)
            {

            }

            var checkIfPushingAnotherPlayer = CheckPositions(updatedPositions[0], players);
            foreach (var p in checkIfPushingAnotherPlayer)
            {
                updatedPositions.Add(p);
            }

            return updatedPositions;
        }

        //Kollar så att användaren vill flytta en pjäs från nest eller flytta en pjäs som redan finns på bordet.
        public List<Piece> MoveFromNestOrBoard(List<Piece> pieces, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            int userInput = 0;
            GameLoop runGame = new GameLoop();
            Player currentPlayer = UpdateGameBoard.GetPlayerTurn(players);
            //List<Piece> currentPlayerPieces = UpdateGameBoard.GetPlayerPieces(currentPlayer);
            List<Piece> updatedPositions = new List<Piece>();

            Console.Clear();
            Square.CurrentBoard(players);
            Console.WriteLine($"Player {currentPlayer.PlayerColor}: {currentPlayer.Name}, you rolled {diceValue}!");

            // Kollar pieces i nästet
            List<Piece> piecesInNest = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (GameBoard.nestPositions.Contains(Convert.ToInt32(piece.Position)))
                {
                    piecesInNest.Add(piece);
                }
            }

            // Kollar pieces på spelbordet
            List<Piece> piecesOnGameBoard = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (piece.Position != currentPlayer.PlayerBoard[0] && piece.Position != 30)
                {
                    piecesOnGameBoard.Add(piece);
                }
            }


            // Kollar om en pjäs finns i nest eller ute på spelbrädet
            bool isPieceInNest = false;
            bool isPieceOnBoard = false;

            foreach (var piece in pieces)
            {
                if (piece.Position == currentPlayer.PlayerBoard[0])
                {
                    isPieceInNest = true;
                }

                else
                {
                    for (int i = 1; i < currentPlayer.PlayerBoard[45]; i++)
                    {
                        if (piece.Position == currentPlayer.PlayerBoard[i])
                        {
                            isPieceOnBoard = true;
                        }
                    }
                }
            }

            if (isPieceInNest && isPieceOnBoard)
            {
                Console.WriteLine("You rolled 6! Choose a piece from the nest or on the board!");


                //TODO - Måste titta över denna, fastnar i loopen
                while (userInput < 1 || userInput > 4)
                {
                    userInput = 0;
                    int.TryParse(Console.ReadLine(), out userInput);
                    Console.WriteLine("Fast i loopen");
                }

                for (int i = 0; i < piecesInNest.Count; i++)
                {

                    if (piecesInNest[i].Id == pieces[userInput - 1].Id)
                    {
                        Console.Clear();
                        Square.CurrentBoard(players);
                        Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!");

                        // Måste ändra att MovePiece tar emot en piece, inte en lista.
                        updatedPositions = MovePiece(piecesInNest[i], 1, currentPlayer.PlayerBoard, players);
                        break;
                    }
                }

                for (int i = 0; i < piecesOnGameBoard.Count; i++)
                {
                    if (piecesOnGameBoard[i].Id == pieces[userInput - 1].Id)
                    {
                        Console.Clear();
                        Square.CurrentBoard(players);
                        Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!");
                        updatedPositions = MovePiece(piecesOnGameBoard[i], diceValue, currentPlayer.PlayerBoard, players);
                        break;
                    }
                }
            }

            else if (isPieceInNest && !isPieceOnBoard)
            {
                //Här ska vi flytta en pjäs från nest:et.
                Console.Clear();
                Square.CurrentBoard(players);

                Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!\n" +
                                        "Which piece do you want to move from the nest?");
                bool isRunning = true;
                do
                {
                    userInput = 0;
                    int.TryParse(Console.ReadLine(), out userInput);

                    for (int i = 0; i < piecesInNest.Count; i++)
                    {
                        if (piecesInNest[i].Id == pieces[userInput - 1].Id)
                        {
                            Console.Clear();
                            Square.CurrentBoard(players);

                            updatedPositions = MovePiece(piecesInNest[i], 1, currentPlayer.PlayerBoard, players);
                            isRunning = false;
                            break;
                        }
                    }

                } while (isRunning);
            }

            Console.Clear();
            return updatedPositions;
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

            for (int i = 0; i < eachPlayersPieces.Count; i++)
            {
                foreach (var piece in eachPlayersPieces[i])
                {
                    if (piece.Position == movedPiece.Position && piece.PlayerId != piece.PlayerId)
                    {
                        Console.WriteLine($"KNUFF!");
                        piece.Position = GameBoard.nestPositions[i];
                        updatedPieces.Add(piece);
                    }
                }
            }
            return updatedPieces;
        }
    }
}
