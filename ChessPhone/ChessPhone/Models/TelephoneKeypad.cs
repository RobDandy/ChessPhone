using ChessPhone.Interfaces;
using System;

namespace ChessPhone.Models {
    public class TelephoneKeypad : IKeyLayout {
        public Immutable2dArray<string> Keys { get; }
        public int RowCount { get; }
        public int ColumnCount { get; }

        public TelephoneKeypad() {
            var keys = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "*", "0", "#" };
            var rowLength = 3;

            if (keys.Length % rowLength != 0)
                throw new ArgumentException($"Number of keys provided ({keys.Length}) with row length {rowLength} leaves an incomplete row of {keys.Length % rowLength} keys");

            RowCount = keys.Length / rowLength;
            ColumnCount = rowLength;

            Keys = new Immutable2dArray<string>(keys, rowLength);
        }

        public bool TryGetKey(int row, int col, out string key) {
            return Keys.TryGetValue(col, row, out key);
        }
    }
}
