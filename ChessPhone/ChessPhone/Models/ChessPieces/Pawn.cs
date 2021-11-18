using ChessPhone.Interfaces;
using System;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public class Pawn : ChessPiece {
        public PieceDirection Direction { get; }

        public Pawn(IKeyLayout keyLayout, Position currentPosition, PieceDirection direction) : base(keyLayout, currentPosition) {
            Direction = direction;
        }

        public override IEnumerable<Position> GetValidMoves() {
            Position move = null;

            switch (Direction) {
                case PieceDirection.Up:
                    move = new Position { Row = CurrentPosition.Row + 1, Column = CurrentPosition.Column };
                    break;
                case PieceDirection.Down:
                    move = new Position { Row = CurrentPosition.Row - 1, Column = CurrentPosition.Column };
                    break;
                case PieceDirection.Left:
                    move = new Position { Row = CurrentPosition.Row, Column = CurrentPosition.Column - 1 };
                    break;
                case PieceDirection.Right:
                    move = new Position { Row = CurrentPosition.Row, Column = CurrentPosition.Column + 1 };
                    break;
                default:
                    throw new NullReferenceException("No valid direction provided for Pawn");
            }

            if (KeyLayout.TryGetKey(move.Row, move.Column, out _)) {
                yield return move;
            }
        }
    }
}
