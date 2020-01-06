using Games.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Games.Sgf
{
    public class SgfWriter
    {
        //public void WriteFile(Collection collection, string path)
        //{
        //    using (Stream stream = File.Create(path))
        //    {
        //        this.WriteStream(collection, stream);
        //    }
        //}

        public void WriteStream(Collection collection, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                this.WriteCollection(collection, writer);
            }
        }

        private void WriteCollection(Collection collection, StreamWriter writer)
        {
            collection.Games.ForEach(t => this.WriteGameTree(t, writer, false));
        }

        private void WriteGameTree(GameTree gameTree, StreamWriter writer, bool escape)
        {
            writer.Write(escape ? "\n(" : "(");
            this.WriteSequence(gameTree.Sequence, writer);
            gameTree.Children.ForEach(t => this.WriteGameTree(t, writer, true));
            writer.Write(")");
        }

        private void WriteSequence(Sequence sequence, StreamWriter writer)
        {
            bool escape = false;
            sequence.Nodes.ForEach(t => { this.WriteNode(t, writer, escape); escape = true; });
        }

        private void WriteNode(Node node, StreamWriter writer, bool escape)
        {
            writer.Write(escape ? "\n;" : ";");
            node.Properties.ForEach(p => this.WriteNodeProperty(p, writer));
        }

        private void WriteNodeProperty(NodeProperty nodeProperty, StreamWriter writer)
        {
            string[] escapedProperties = new string[] { "RU", "PW" };

            if (escapedProperties.Contains(nodeProperty.Ident.Text))
            {
                writer.Write("\n");
            }

            writer.Write(nodeProperty.Ident.Text);
            nodeProperty.Values.ForEach(value =>
            {
                writer.Write("[");
                writer.Write(value);
                writer.Write("]");
            });
        }
    }
}
