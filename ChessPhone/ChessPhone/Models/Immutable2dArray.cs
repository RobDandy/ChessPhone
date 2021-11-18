using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;


namespace ChessPhone.Models {
    public class Immutable2dArray<T> : IEnumerable<T> {
        private int _width { get; }
        private int _height { get; }
        private IImmutableList<T> _list { get; }

        public Immutable2dArray(T[] values, int width) {
            if (values.Length % width != 0)
                throw new ArgumentException($"Length of provided values array ({values.Length}) with matrix width {width} leaves an incomplete row of {values.Length % width} values");


            _width = width;
            _height = values.Length / width;
            _list = ImmutableArray.Create(values);
        }

        public bool TryGetValue(int x, int y, out T value) {
            value = default(T);

            if (x < 0 || x >= _width) {
                return false;
            }

            if (y < 0 || y >= _height) {
                return false;
            }

            value = _list[y * _width + x];

            return true;
        }

        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _list.GetEnumerator();
        }
    }
}
