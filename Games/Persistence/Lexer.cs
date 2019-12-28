using Games.Utilities;
using System.IO;

namespace Games.Sgf
{
    public class Lexer
    {
        private StreamReader reader;
        private Lexeme bufferedLexeme;

        public Lexer(StreamReader reader)
        {
            this.reader = reader;
        }

        public Lexeme Peek()
        {
            if (bufferedLexeme == null)
            {
                bufferedLexeme = Read();
            }

            return bufferedLexeme;
        }

        public Lexeme Read()
        {
            Lexeme result;

            if (bufferedLexeme == null)
            {
                reader.ReadUntil(char.IsWhiteSpace);
                int n = reader.Peek();

                if (Lexeme.Semicolon.ValueAsInt == n)
                {
                    reader.Read();
                    result = Lexeme.Semicolon;
                }
                else if (Lexeme.OpeningBracket.ValueAsInt == n)
                {
                    reader.Read();
                    result = Lexeme.OpeningBracket;
                }
                else if (Lexeme.ClosingBracket.ValueAsInt == n)
                {
                    reader.Read();
                    result = Lexeme.ClosingBracket;
                }
                else if (-1 == n)
                {
                    result = Lexeme.End;
                }
                else if ('[' == n)
                {
                    reader.Read();
                    string value = reader.ReadUntil(c => c != ']');
                    reader.Read();
                    result = new Lexeme(LexemeType.Value, value);
                }
                else
                {
                    string text = reader.ReadUntil(char.IsLetter);
                    result = new Lexeme(LexemeType.Letters, text);
                }
            }
            else
            {
                result = bufferedLexeme;
                bufferedLexeme = null;
            }

            return result;
        }
    }
}
