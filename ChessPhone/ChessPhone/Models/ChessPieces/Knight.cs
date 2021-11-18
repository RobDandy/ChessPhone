using ChessPhone.Interfaces;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class Knight : ChessPiece {
        public Knight(IKeyLayout keyLayout, Position currentPosition) : base(keyLayout, currentPosition) { }

        public override IEnumerable<Position> GetValidMoves() {
            var moves = new[,] {
                { 2, 1 },
                { 2, -1 },
                { -2, 1 },
                { -2, -1 },
                { 1, 2 },
                { 1, -2 },
                { -1, 2 },
                { -1, -2 }
            };

            // This is a 2d array so iterate over half the indexes
            for (int idx = 0; idx < moves.Length / 2; idx++) {
                var move = new Position { Row = CurrentPosition.Row + moves[idx, 0], Column = CurrentPosition.Column + moves[idx, 1] };
                if (KeyLayout.TryGetKey(move.Row, move.Column, out _)) {
                    yield return move;
                }
            }
        }
    }
}
