using System;
using System.Collections.Generic;
using System.IO;

namespace Games.Sgf
{
    public class SgfReader
    {
        //public Collection ReadFile(string path)
        //{
        //    using (Stream stream = File.OpenRead(path))
        //    {
        //        return this.ReadStream(stream);
        //    }
        //}

        public Collection ReadStream(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                Lexer lexer = new Lexer(reader);
                return ParseCollection(lexer);
            }
        }

        private Collection ParseCollection(Lexer lexer)
        {
            Collection result;
            List<GameTree> trees = new List<GameTree>();
            GameTree tree = ParseGameTree(lexer);

            while (tree != null)
            {
                trees.Add(tree);
                tree = ParseGameTree(lexer);
            }

            if (trees.Count == 0)
            {
                result = null;
            }
            else
            {
                result = new Collection(trees);
            }

            return result;
        }

        private GameTree ParseGameTree(Lexer lexer)
        {
            GameTree result = null;

            if (lexer.Peek().Type == LexemeType.OpeningBracket)
            {
                lexer.Read();
                Sequence sequence = ParseSequence(lexer);
                
                if (sequence == null)
                {
                    throw new ArgumentException("Sequence expected");
                }
                else
                {
                    List<GameTree> trees = new List<GameTree>();
                    GameTree tree = ParseGameTree(lexer);

                    while (tree != null)
                    {
                        trees.Add(tree);
                        tree = ParseGameTree(lexer);
                    }

                    result = new GameTree(sequence, trees);
                }

                if (lexer.Read().Type != LexemeType.ClosingBracket)
                {
                    throw new ArgumentException("Closing bracket expected");
                }

                return result;
            }

            return result;
        }

        private Sequence ParseSequence(Lexer lexer)
        {
            List<Node> nodes = new List<Node>();
            Node node = ParseNode(lexer);

            while (node != null)
            {
                nodes.Add(node);
                node = ParseNode(lexer);
            }

            if (nodes.Count == 0)
            {
                return null;
            }
            else
            {
                return new Sequence(nodes);
            }
        }

        private Node ParseNode(Lexer lexer)
        {
            Node result = null;

            if (lexer.Peek().Type == LexemeType.Semicolon)
            {
                lexer.Read();
                NodeProperties properties = ParseNodeProperties(lexer);
                result = new Node(properties);
            }

            return result;
        }

        private NodeProperties ParseNodeProperties(Lexer lexer)
        {
            NodeProperties result = new NodeProperties();
            NodeProperty property = ParseNodeProperty(lexer);

            while (property != null)
            {
                result.Add(property);
                property = ParseNodeProperty(lexer);
            }

            return result;
        }

        private NodeProperty ParseNodeProperty(Lexer lexer)
        {
            NodeProperty result = null;

            if (lexer.Peek().Type == LexemeType.Letters)
            {
                Lexeme identLexeme = lexer.Read();
                Lexeme valueLexeme = lexer.Read();

                if (valueLexeme.Type == LexemeType.Value)
                {
                    NodePropertyIdent ident = new NodePropertyIdent(identLexeme.Value);
                    List<string> values = new List<string>();
                    values.Add(valueLexeme.Value);

                    while (lexer.Peek().Type == LexemeType.Value)
                    {
                        valueLexeme = lexer.Read();
                        values.Add(valueLexeme.Value);
                    }

                    result = new NodeProperty(ident, values);
                }
                else
                {
                    throw new ArgumentException("Property value expected");
                }
            }

            return result;
        }

        /*
        private string ParseNodePropertyValue(StreamReader reader)
        {
            if (this.ReadTerminal(reader, '['))
            {
                long streamPosition = reader.BaseStream.Position;
                StringBuilder result = new StringBuilder();
                string part;

                do
                {
                    part = reader.ReadUntil(c => c != ']');

                    if (part != null)
                    {
                        result.Append(part);
                    }
                }
                while (part != null && part.Length > 0 && part[part.Length - 1] == '\\');

                if (result.Length == 0)
                {
                    reader.DiscardBufferedData();
                    reader.BaseStream.Seek(streamPosition, SeekOrigin.Begin);
                    return null;
                }
                else
                {
                    reader.Read();
                    return result.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        private string ParseUcLetter(StreamReader reader)
        {
            long streamPosition = reader.BaseStream.Position;
            reader.ReadUntil(char.IsWhiteSpace);
            string text = reader.ReadUntil(this.IsUcLetter);

            if (text == null)
            {
                reader.DiscardBufferedData();
                reader.BaseStream.Seek(streamPosition, SeekOrigin.Begin);
            }

            return text;
        }

        private bool IsUcLetter(char c)
        {
            return char.IsLetter(c);
        }

        private bool ReadTerminal(StreamReader reader, char terminal)
        {
            long streamPosition = reader.BaseStream.Position;
            reader.ReadUntil(char.IsWhiteSpace);

            if (terminal == reader.Peek())
            {
                reader.Read();
                return true;
            }
            else
            {
                reader.DiscardBufferedData();
                reader.BaseStream.Seek(streamPosition, SeekOrigin.Begin);
                return false;
            }
        }
        */
    }

    public class Collection
    {
        public List<GameTree> Games { get; private set; }

        public Collection(List<GameTree> games)
        {
            Games = games;
        }
    }

    public class GameTree
    {
        public Sequence Sequence { get; private set; }
        public List<GameTree> Children { get; private set; }

        public GameTree(Sequence sequence, List<GameTree> children)
        {
            Sequence = sequence;
            Children = children;
        }
    }

    public class Sequence
    {
        public List<Node> Nodes { get; private set; }

        public Sequence(List<Node> nodes)
        {
            Nodes = nodes;
        }
    }

    public class Node
    {
        public NodeProperties Properties { get; private set; }

        public Node(NodeProperties properties)
        {
            Properties = properties;
        }
    }

    public class NodeProperties : List<NodeProperty>
    {

    }

    public class NodeProperty
    {
        public NodePropertyIdent Ident { get; private set; }
        public List<string> Values { get; private set; }

        public NodeProperty(NodePropertyIdent ident, List<string> values)
        {
            Ident = ident;
            Values = values;
        }
    }

    public class NodePropertyIdent
    {
        public NodePropertyType Type { get; private set; }
        public string Text { get; private set; }

        public NodePropertyIdent(string text)
        {
            //this.Type = type;
            Text = text;
        }
    }

    public enum NodePropertyType
    {

    }
}
