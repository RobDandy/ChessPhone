using ChessPhone.Interfaces;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class Bishop : ChessPiece {
        public Bishop(IKeyLayout keyLayout, Position currentPosition) : base(keyLayout, currentPosition) { }

        public override IEnumerable<Position> GetValidMoves() {
            var dirs = new[] { -1, 1 };

            foreach (var rowDir in dirs) {
                foreach (var colDir in dirs) {
                    foreach (var move in GetValidMovesInDirection(rowDir, colDir)) {
                        yield return move;
                    }
                }
            }
        }

        private IEnumerable<Position> GetValidMovesInDirection(int rowDir, int colDir) {
            int row = CurrentPosition.Row + rowDir;
            int col = CurrentPosition.Column + colDir;

            while (row >= 0 && row < KeyLayout.RowCount && 
                   col >= 0 && col < KeyLayout.ColumnCount) {

                if (KeyLayout.TryGetKey(row, col, out _)) {
                    yield return new Position { Row = row, Column = col };
                }
                
                row += rowDir;
                col += colDir;
            }
        }
    }
}
