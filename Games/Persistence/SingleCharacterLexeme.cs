namespace Games.Sgf
{
    public class SingleCharacterLexeme : Lexeme
    {
        public int ValueAsInt { get; private set; }
        public char ValueAsChar { get; private set; }

        public SingleCharacterLexeme(LexemeType type, int character) : base(type, new string(new char[] { (char)character }))
        {
            this.ValueAsInt = character;
            this.ValueAsChar = (char)character;
        }
    }
}
