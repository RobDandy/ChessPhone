using ChessPhone.Interfaces;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class Rook : ChessPiece {
        public Rook(IKeyLayout keyLayout, Position currentPosition) : base(keyLayout, currentPosition) { }

        public override IEnumerable<Position> GetValidMoves() {
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
    }
}
