using System.Drawing;

namespace Labs.Agents
{
    public class SpaceTemplateBitmap : ISpaceTemplateFactory
    {
        public string Name { get; set; }

        public SpaceTemplateBitmap(string name)
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
