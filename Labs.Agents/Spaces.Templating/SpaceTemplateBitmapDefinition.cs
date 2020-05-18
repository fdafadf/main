using System.Drawing;

namespace Labs.Agents
{
    public class SpaceTemplateBitmapDefinition : ISpaceTemplateFactory
    {
        public string Name { get; set; }

        public SpaceTemplateBitmapDefinition(string name)
        {
            Name = name;
        }

        public SpaceTemplate CreateSpaceTemplate()
        {
            using (var bitmap = new Bitmap(Settings.SpacesDirectory.GetFile(Name).FullName))
            {
                return new SpaceTemplate(bitmap);
            }
        }
    }
}
