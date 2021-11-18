namespace ChessPhone.Interfaces {
    public interface IInputValidator {
        public int InputLength { get; }
        bool IsValid(string value);
        bool IsValidExcludingLength(string value);
    }
}
