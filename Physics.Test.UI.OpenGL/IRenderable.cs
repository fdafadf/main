namespace Basics.Physics.Test.UI
{
    interface IRenderable
    {
        void Render(int modelMatrixUniformIndex);
    }

    interface IUpdatable
    {
        void Update();
    }
}
