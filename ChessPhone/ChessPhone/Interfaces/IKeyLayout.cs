using ChessPhone.Models;

namespace ChessPhone.Interfaces {
    public interface IKeyLayout {
        public Immutable2dArray<string> Keys { get; }
        int RowCount { get; }
        int ColumnCount { get; }
        bool TryGetKey(int row, int col, out string key);
    }
}
