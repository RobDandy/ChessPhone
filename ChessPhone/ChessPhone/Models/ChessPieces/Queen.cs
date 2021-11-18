using ChessPhone.Interfaces;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class Queen : ChessPiece {
        public Queen(IKeyLayout keyLayout, Position currentPosition) : base(keyLayout, currentPosition) { }

        public override IEnumerable<Position> GetValidMoves() {
            // Diagonal moves
            var dirs = new[] { -1, 1 };

            foreach (var rowDir in dirs) {
                foreach (var colDir in dirs) {
                    foreach (var move in GetValidMovesInDirection(rowDir, colDir)) {
                        yield return move;
                    }
                }
            }

            // Horizontal and Vertical moves
            for (int row = 0; row < KeyLayout.RowCount; row++) {
                if (row != CurrentPosition.Row) {
                    if (KeyLayout.TryGetKey(row, CurrentPosition.Column, out _)) {
                        yield return new Position { Row = row, Column = CurrentPosition.Column };
                    }
                }
            }


            for (int col = 0; col < KeyLayout.ColumnCount; col++) {
                if (col != CurrentPosition.Column) {
                    if (KeyLayout.TryGetKey(CurrentPosition.Row, col, out _)) {
                        yield return new Position { Row = CurrentPosition.Row, Column = col };
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
