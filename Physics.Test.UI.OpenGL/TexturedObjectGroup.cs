using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WindowsFormsApp1;

namespace Basics.Physics.Test.UI
{
    class TexturedObjectGroup : ObjectGroup
    {
        public int ShaderLayout_Position;
        public int ShaderLayout_Texture;

        public TexturedObjectGroup(TexturedObjectGroupDefinition definition) : base(definition)
        {
            ShaderLayout_Position = GL.GetAttribLocation(ShaderProgram.Handle, definition.ShaderLayoutName_Position);
            ShaderLayout_Texture = GL.GetAttribLocation(ShaderProgram.Handle, definition.ShaderLayoutName_Texture);
        }

        public TexturedModel Add(TexturedVertex[] tvs, uint[] tis)
        {
            TexturedModel texturedModel = new TexturedModel(tvs, tis, ShaderLayout_Position, ShaderLayout_Texture);
            this.Disposables.Add(texturedModel);
            return texturedModel;
        }

        public void Add(TexturedModel texturedModel, int textureId, Vector3 position)
        {
            SceneObject texturedObject = new SceneObject(texturedModel, position);
            //_texture = InitTextures(@"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Basics.Physics.Test.UI\Textures\wooden.bmp");
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            this.Renderables.Add(texturedObject);
        }

        public TexturedModel Add(Triangle triangle)
        {
            TexturedVertex[] tvs = {
                new TexturedVertex(triangle.A.ToVector3(), new Vector2(0, 0)),
                new TexturedVertex(triangle.B.ToVector3(), new Vector2(127, 255)),
                new TexturedVertex(triangle.C.ToVector3(), new Vector2(255, 0)),
            };
            uint[] tis = { 0, 1, 2 };
            return Add(tvs, tis);
        }

        public Texture LoadTexture(FileInfo file)
        {
            Texture texture = new Texture(file);
            this.Disposables.Add(texture);
            return texture;
        }
    }

    class Texture : IDisposable
    {
        public readonly int Handle;

        public Texture(FileInfo file)
        {
            using (Bitmap bitmap = new Bitmap(file.FullName))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Handle = GL.GenTexture();
                //GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, Handle);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
                //GL.TextureStorage2D(texture, 1, SizedInternalFormat.Rgba32f, width, height);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
                bitmap.UnlockBits(data);
            }
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }
    }
}
