namespace Games.Sgf
{
    public class Lexeme
    {
        public static SingleCharacterLexeme Semicolon = new SingleCharacterLexeme(LexemeType.Semicolon, ';');
        public static SingleCharacterLexeme OpeningBracket = new SingleCharacterLexeme(LexemeType.OpeningBracket, '(');
        public static SingleCharacterLexeme ClosingBracket = new SingleCharacterLexeme(LexemeType.ClosingBracket, ')');
        public static SingleCharacterLexeme End = new SingleCharacterLexeme(LexemeType.End, -1);

        public LexemeType Type { get; private set; }
        public string Value { get; private set; }

        public Lexeme(LexemeType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
