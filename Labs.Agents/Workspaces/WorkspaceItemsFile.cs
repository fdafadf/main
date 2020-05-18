using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class WorkspaceItemsFile<T> : IEnumerable<T>
    {
        FileInfo File;
        List<T> Items;

        public WorkspaceItemsFile(FileInfo file)
        {
            File = file;

            if (File.Exists)
            {
                try
                {
                    Items = File.Deserialize<List<T>>();
                }
                catch (Exception exception)
                {
                    if (MessageBox.Show($"Do you want to remove file '{file.Name}'?\r\n\r\n{exception.Message}", "Deserialisation error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        File.Delete();
                        Items = new List<T>();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                Items = new List<T>();
            }
        }

        public void Add(T item)
        {
            Items.Add(item);
            File.Serialize(Items);
        }

        public bool Remove(T item)
        {
            bool removed = item != null && Items.Remove(item);

            if (removed)
            {
                File.Serialize(Items);
            }

            return removed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
