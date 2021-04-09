﻿using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoGameEngine.GameLogic
{
    public class Move
    {
        public List<Piece> MovePiece(Piece piece, int diceValue, List<int> playerGameBoard, List<Player> players)
        {
            List<Piece> updatedPositions = new List<Piece>();

            var index = playerGameBoard.IndexOf(piece.Position);

            index = index + diceValue;

            // TODO - Här måste vi göra en check så att position inte överstiger position 30 (sista steget)
            try
            {
                piece.Position = playerGameBoard[index];

                if (piece.Position == 30)
                {                   
                    piece.IsActive = false;
                }
            }
            catch (Exception e)
            {

            }

            updatedPositions.Add(piece);

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


            // Kollar om en pjäs finns i nest eller ute på spelbrädet
            bool isPieceInNest = false;
            bool isPieceOnBoard = false;


            // Kollar pieces i nästet
            List<Piece> piecesInNest = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (GameBoard.nestPositions.Contains(Convert.ToInt32(piece.Position)))
                {
                    piecesInNest.Add(piece);
                    isPieceInNest = true;
                }
            }

            // Kollar pieces på spelbordet
            List<Piece> piecesOnGameBoard = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (piece.Position != currentPlayer.PlayerBoard[0] && piece.Position != 30)
                {
                    piecesOnGameBoard.Add(piece);
                    isPieceOnBoard = true;
                }
            }

            if (isPieceInNest && isPieceOnBoard)
            {
                Console.WriteLine("You rolled 6! Choose a piece from the nest or on the board!");

                var counter = piecesOnGameBoard.Count + piecesInNest.Count;
                //TODO - Måste titta över denna, fastnar i loopen
                while (userInput < 1 || userInput > counter)
                {
                    userInput = 0;
                    int.TryParse(Console.ReadLine(), out userInput);
                    
                    if(userInput < 1 || userInput > counter)
                    {
                        Console.WriteLine($"You pressed wrong number! Try agin");
                    }
                }

                for (int i = 0; i < piecesInNest.Count; i++)
                {
                    if (userInput >= 0 && userInput <= 4)
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
                        else
                        {
                            Console.WriteLine($"You pressed wrong number! Try agin you bastard!");
                        }
                    }
                }

                for (int i = 0; i < piecesOnGameBoard.Count; i++)
                {
                    if (userInput >= 0 && userInput <= 4)
                    {
                        if (piecesOnGameBoard[i].Id == pieces[userInput - 1].Id)
                        {
                            Console.Clear();
                            Square.CurrentBoard(players);
                            Console.WriteLine($"Player {currentPlayer.PlayerColor} {currentPlayer.Name} rolled 6!");
                            updatedPositions = MovePiece(piecesOnGameBoard[i], diceValue, currentPlayer.PlayerBoard, players);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"You pressed wrong number! Try agin You fuckface!!!");
                        }
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
                        if(userInput <= piecesInNest.Count && userInput > 0)
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
                    if (piece.Position == movedPiece.Position && piece.PlayerId != movedPiece.PlayerId)
                    {
                        Console.WriteLine("KNUFF!".Rainbow());
                        piece.Position = GameBoard.nestPositions[i];
                        updatedPieces.Add(piece);
                        // Explosion from hell!!!
                        Thread.Sleep(3000);

                   }
                }
            }
            return updatedPieces;
        }
    }
}
