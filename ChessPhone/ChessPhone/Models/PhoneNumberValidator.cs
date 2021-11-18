using ChessPhone.Interfaces;
using System.Text.RegularExpressions;

namespace ChessPhone.Models {
    public class PhoneNumberValidator : IInputValidator {
        public int InputLength { get => 7; }
        private Regex regex = new Regex("^[2-9][0-9]*$");
        public bool IsValid(string value) {
            return value.Length == InputLength && regex.IsMatch(value);
        }

        public bool IsValidExcludingLength(string value) {
            return regex.IsMatch(value);
        }
    }
}
