using Labs.Agents.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class WorkspaceSpaces : IEnumerable<ISpaceTemplateFactory>
    {
        public FileInfo SpacesFile { get; }
        public DirectoryInfo SpacesDirectory { get; }
        List<ISpaceTemplateFactory> Items;

        public WorkspaceSpaces(FileInfo spacesFile, DirectoryInfo spacesDirectory)
        {
            SpacesFile = spacesFile;
            SpacesDirectory = spacesDirectory;
            bool createSpacesFile = false;

            if (SpacesFile.Exists)
            {
                try
                {
                    Items = SpacesFile.Deserialize<List<ISpaceTemplateFactory>>();
                }
                catch (Exception exception)
                {
                    if (MessageBox.Show($"Do you want to remove file '{SpacesFile.Name}'?\r\n\r\n{exception.Message}", "Deserialisation error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        SpacesFile.Delete();
                        createSpacesFile = true;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                createSpacesFile = true;
            }
           
            if (createSpacesFile)
            {
                Items = new List<ISpaceTemplateFactory>();
                Add(new SpaceTemplateGeneratingDefinition(new SpaceTemplateGeneratorProperties("Generated Example 1")));
                Add(new SpaceTemplateGeneratingDefinition(new SpaceTemplateGeneratorProperties("Generated Example 2") { Seed = 1 }));
            }

            if (spacesDirectory.Exists == false)
            {
                spacesDirectory.Create();

                foreach (var property in typeof(Resources).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (property.PropertyType == typeof(Bitmap))
                    {
                        Bitmap image = property.GetValue(null) as Bitmap;

                        if (image != null)
                        {
                            image.Save(Path.Combine(spacesDirectory.FullName, $"{property.Name}.png"));
                        }
                    }
                }
            }

            foreach (var environmentMapFile in spacesDirectory.EnumerateFiles("*.png"))
            {
                Items.Add(new SpaceTemplateBitmapDefinition(environmentMapFile.Name));
            }
        }

        public ISpaceTemplateFactory GetByName(string name)
        {
            return Items.FirstOrDefault(space => space.Name == name);
        }

        public bool Remove(ISpaceTemplateFactory space)
        {
            bool removed = space != null && Items.Remove(space);

            if (removed)
            {
                SpacesFile.Serialize(Items.Where(s => s is SpaceTemplateBitmapDefinition == false).ToList());
            }

            return removed;
        }

        public bool Remove(string name)
        {
            return Remove(GetByName(name));
        }

        public void Add(ISpaceTemplateFactory space)
        {
            Items.Add(space);
            SpacesFile.Serialize(Items.Where(s => s is SpaceTemplateBitmapDefinition == false).ToList());
        }

        public IEnumerator<ISpaceTemplateFactory> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
