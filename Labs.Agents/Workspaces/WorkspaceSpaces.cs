using Labs.Agents.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class WorkspaceSpaces : IEnumerable<ISpaceDefinition>
    {
        public FileInfo SpacesFile { get; }
        public DirectoryInfo SpacesDirectory { get; }
        List<ISpaceDefinition> Items;

        public WorkspaceSpaces(FileInfo spacesFile, DirectoryInfo spacesDirectory)
        {
            SpacesFile = spacesFile;
            SpacesDirectory = spacesDirectory;

            if (SpacesFile.Exists)
            {
                Items = SpacesFile.Deserialize<List<ISpaceDefinition>>();
            }
            else
            {
                Items = new List<ISpaceDefinition>();
                Add(new SpaceTemplateGeneratingDefinition(new SpaceTemplateGeneratorProperties("Test1")));
                Add(new SpaceTemplateGeneratingDefinition(new SpaceTemplateGeneratorProperties("Test2") { Seed = 1 }));
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

        public ISpaceDefinition GetByName(string name)
        {
            return Items.FirstOrDefault(space => space.Name == name);
        }

        public bool Remove(ISpaceDefinition space)
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

        public void Add(ISpaceDefinition space)
        {
            Items.Add(space);
            SpacesFile.Serialize(Items.Where(s => s is SpaceTemplateBitmapDefinition == false).ToList());
        }

        public IEnumerator<ISpaceDefinition> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
