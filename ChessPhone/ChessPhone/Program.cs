using ChessPhone.Interfaces;
using ChessPhone.Models;
using ChessPhone.Models.ChessPieces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChessPhone {
    class Program {
        static void Main(string[] args) {
            var telephoneKeypad = new TelephoneKeypad();
            var phoneNumberValidator = new PhoneNumberValidator();

            var pawnOutputCount = CountValidOutputsForPawn(phoneNumberValidator, telephoneKeypad);
            var knightOutputCount = CountValidOutputsForPieceType(phoneNumberValidator, telephoneKeypad, (pos) => new Knight(telephoneKeypad, pos));
            var bishopOutputCount = CountValidOutputsForPieceType(phoneNumberValidator, telephoneKeypad, (pos) => new Bishop(telephoneKeypad, pos));
            var rookOutputCount = CountValidOutputsForPieceType(phoneNumberValidator, telephoneKeypad, (pos) => new Rook(telephoneKeypad, pos));
            var kingOutputCount = CountValidOutputsForPieceType(phoneNumberValidator, telephoneKeypad, (pos) => new King(telephoneKeypad, pos));
            var queenOutputCount = CountValidOutputsForPieceType(phoneNumberValidator, telephoneKeypad, (pos) => new Queen(telephoneKeypad, pos));

            Console.WriteLine($"Pawn: {pawnOutputCount}");
            Console.WriteLine($"Knight: {knightOutputCount}");
            Console.WriteLine($"Bishop: {bishopOutputCount}");
            Console.WriteLine($"Rook: {rookOutputCount}");
            Console.WriteLine($"King: {kingOutputCount}");
            Console.WriteLine($"Queen: {queenOutputCount}");
        }

        static int CountValidOutputsForPieceType<T>(IInputValidator validator, IKeyLayout layout, Func<Position, T> pieceFactory) where T : IChessPiece {
            var validOutputCount = 0;

            for (int row = 0; row < layout.RowCount; row++) {
                for (int col = 0; col < layout.ColumnCount; col++) {
                    var piece = pieceFactory(new Position { Row = row, Column = col });
                    validOutputCount += CountValidOutputsForPiece("", piece, validator, layout, pieceFactory);
                }
            }

            return validOutputCount;
        }

        static int CountValidOutputsForPiece<T>(string output, IChessPiece piece, IInputValidator validator, IKeyLayout layout, Func<Position, T> pieceFactory) where T : IChessPiece {
            var validOutputCount = 0;

            if (layout.TryGetKey(piece.CurrentPosition.Row, piece.CurrentPosition.Column, out string key)) {
                output += key;

                if (validator.IsValid(output)) {
                    validOutputCount++;
                } else if (validator.IsValidExcludingLength(output)) {
                    Parallel.ForEach(piece.GetValidMoves(), move => {
                        var newPiece = pieceFactory(move);
                        var outputCountForPiece = CountValidOutputsForPiece(output, newPiece, validator, layout, pieceFactory);
                        Interlocked.Add(ref validOutputCount, outputCountForPiece);
                    });
                }
            }

            return validOutputCount;
        }

        static int CountValidOutputsForPawn(IInputValidator validator, IKeyLayout layout) {
            var validOutputCount = 0;

            var pawnFactories = new Func<Position, Pawn>[] {
                (pos) => new Pawn(layout, pos, PieceDirection.Up),
                (pos) => new Pawn(layout, pos, PieceDirection.Down),
                (pos) => new Pawn(layout, pos, PieceDirection.Left),
                (pos) => new Pawn(layout, pos, PieceDirection.Right),
            };

            Parallel.ForEach(pawnFactories, factory => {
                var outputCountForFactory = CountValidOutputsForPieceType(validator, layout, factory);
                Interlocked.Add(ref validOutputCount, outputCountForFactory);
            });

            return validOutputCount;
        }
    }
}
