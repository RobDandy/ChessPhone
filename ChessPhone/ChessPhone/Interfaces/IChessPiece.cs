using ChessPhone.Models;
using System.Collections.Generic;

namespace ChessPhone.Interfaces {
    public interface IChessPiece {
        public Position CurrentPosition { get; }
        public IEnumerable<Position> GetValidMoves();
    }
}
