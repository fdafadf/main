using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsFormsApp1;

namespace Basics.Physics.Test.UI
{
    class MainWindow : GameWindow
    {
        private List<IDisposable> disposables = new List<IDisposable>();
        private List<ICamera> cameras = new List<ICamera>();
        VertexObjectGroup vertexGroup;
        TexturedObjectGroup texturedGroup;
        SceneSphere playerSceneSphere;
        Race race;

        public MainWindow(int width, int height) : base(width, height, GraphicsMode.Default, "Physics", GameWindowFlags.Default, DisplayDevice.Default, 4, 5, GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.CadetBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            VertexObjectGroupDefinition vertexGroupDefinition = new VertexObjectGroupDefinition(Properties.Resources.VertexShader, Properties.Resources.FragmentShader);
            vertexGroup = new VertexObjectGroup(vertexGroupDefinition);
            this.disposables.Add(vertexGroup);

            #region Models

            WavefrontModel model = new WavefrontModelLoader().Load(new FileInfo(@"C:\Users\pstepnowski\Documents\untitled.obj"));
            RenderableModel modelSphere = vertexGroup.Add(model);

            #endregion
            #region Scene Objects

            race = new Race(200);
            vertexGroup.Add(modelSphere, race.Spheres);
            playerSceneSphere = vertexGroup.Renderables.OfType<SceneSphere>().First(s => s.Sphere == race.PlayerSphere);
            
            #endregion

            Camera camera1 = new Camera(Width, Height);
            camera1.position = new Vector3(5, 2, 5);
            CameraFollow camera2 = new CameraFollow(Width, Height, playerSceneSphere);
            camera2.position = new Vector3(5, 2, 5);
            this.cameras.Add(camera2);

            TexturedObjectGroupDefinition texturedGroupDefinition = new TexturedObjectGroupDefinition(Properties.Resources.VertexShaderTextured, Properties.Resources.FragmentShaderTextured);
            texturedGroup = new TexturedObjectGroup(texturedGroupDefinition);
            this.disposables.Add(texturedGroup);

            FileInfo textureFile = new FileInfo(@"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Basics.Physics.Test.UI\Textures\concrete.png");
            Texture texture = texturedGroup.LoadTexture(textureFile);

            TestModelLoader modelLoader = new TestModelLoader();
            modelLoader.LoadTrack(texturedGroup, texture);

            //TexturedModel triangleModel = texturedGroup.Add(race.Ground);
            //texturedGroup.Add(triangleModel, texture.Handle, new Vector3());
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //this.camera.Update();
            ICamera selectedCamera = this.cameras.First();
            vertexGroup.Render(selectedCamera);
            texturedGroup.Render(selectedCamera);

            GL.BindVertexArray(0);
            this.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = Keyboard.GetState();
            race.Input.TurningRight = input.IsKeyDown(Key.D);
            race.Input.TurningLeft = input.IsKeyDown(Key.A);
            race.Input.Accelerate = input.IsKeyDown(Key.W);
            race.Input.Braking = input.IsKeyDown(Key.S);
            race.UpdateInput();
            race.UpdateDirections();
            race.UpdatePositions(0.1f);
            playerSceneSphere.UpdateRotation();

            this.cameras.First().Update();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.BindVertexArray(0);
            GL.UseProgram(0);

            foreach (IDisposable disposable in this.disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
