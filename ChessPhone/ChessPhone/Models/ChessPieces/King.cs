using ChessPhone.Interfaces;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class King : ChessPiece {
        public King(IKeyLayout keyLayout, Position currentPosition) : base(keyLayout, currentPosition) { }

        public override IEnumerable<Position> GetValidMoves() {
            var moves = new[,] {
                { 1, 1 },
                { -1, -1 },
                { 1, -1 },
                { -1, 1 },
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 }
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
