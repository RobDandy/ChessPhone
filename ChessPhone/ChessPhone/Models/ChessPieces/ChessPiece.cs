using ChessPhone.Interfaces;
using System;
using System.Collections.Generic;

namespace ChessPhone.Models.ChessPieces {
    public abstract class ChessPiece : IChessPiece {
        private readonly IKeyLayout _keyLayout;
        protected IKeyLayout KeyLayout { get => _keyLayout; }

        private Position _currentPosition;
        public Position CurrentPosition { get => _currentPosition; }

        public ChessPiece(IKeyLayout keyLayout, Position currentPosition) {
            if (keyLayout.TryGetKey(currentPosition.Row, currentPosition.Column, out _) == false) {
                throw new ArgumentOutOfRangeException($"Unable to create chess piece at column index: {currentPosition.Column} and row index: {currentPosition.Row}. " +
                                                      $"KeyLayout has {keyLayout.ColumnCount} columns and {keyLayout.RowCount} rows.");
            }

            _keyLayout = keyLayout;
            _currentPosition = currentPosition;
        }

        public abstract IEnumerable<Position> GetValidMoves();
    }
}
