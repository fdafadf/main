using AI.NeuralNetworks;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.AI.NeuralNetworks.Car
{
    public partial class Form1 : Form
    {
        Color SandColor = Color.Yellow;
        Car Car;
        Bitmap Bitmap;
        Vector2 Goal;
        QLearning Brain;
        float[] Actions = { 0, 20, -20 };

        public Form1()
        {
            InitializeComponent();
            Bitmap = new Bitmap(800, 460);
            Goal = new Vector2(20, Bitmap.Width - 20);
            Car = new Car(Bitmap.Width / 2, Bitmap.Height / 2, 6, 0);
            Brain = new QLearning();
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Bitmap, 0, 0);
            e.Graphics.DrawRectangle(Pens.Blue, Car.Position.X - 5, Car.Position.Y - 5, 10, 10);
        }

        private float lastDistance;
        private float lastReward;

        private void timer1_Tick(object sender, EventArgs e)
        {
            float dx = Goal.X - Car.Position.X;
            float dy = Goal.Y - Car.Position.Y;
            float orientation = 0; // Math.Atan2(b.Y - a.Y, b.X - a.X); // Vector.An Car.Velocity.Angle //Vector2(*self.car.velocity).angle((xx, yy)) / 180.
            int action = Brain.Update(lastReward, Car.Signal1, Car.Signal2, Car.Signal3, orientation);
            Car.Move(Actions[action], Bitmap);
            float distance = Car.Position.Distance(Goal);
            lastReward = GetReward(distance, lastDistance);
            lastDistance = distance;
            Refresh();
        }

        private float GetReward(float distance, float lastDistance)
        {
            Color pixel = Bitmap.GetPixel((int)Car.Position.X, (int)Car.Position.Y);

            if (Car.Position.X < 10)
            {
                Car.Position.X = 10;
                return -1;
            }

            if (Car.Position.X > Bitmap.Width - 10)
            {
                Car.Position.X = Bitmap.Width - 10;
                return -1;
            }

            if (Car.Position.Y < 10)
            {
                Car.Position.Y = 10;
                return -1;
            }

            if (Car.Position.Y > Bitmap.Height - 10)
            {
                Car.Position.Y = Bitmap.Height - 10;
                return -1;
            }

            if (pixel == SandColor)
            {
                return -1;
            }
            else
            {
                return distance < lastDistance ? 0.1f : -0.2f;
            }
        }
    }

    public class MemoryItem
    {
        public double[] FromState;
        public double[] ToState;
        public int Action;
        public double Reward;

        public MemoryItem(double[] fromState, double[] toState, int action, double reward)
        {
            FromState = fromState;
            ToState = toState;
            Action = action;
            Reward = reward;
        }
    }

    public class History
    {
        public LinkedList<MemoryItem> Items = new LinkedList<MemoryItem>();

        public void Add(double[] fromState, double[] toState, int action, double reward)
        {
            Items.AddFirst(new MemoryItem(fromState, toState, action, reward));
        }
    }

    public class QLearning
    {
        Network Network;
        History History;
        double[] lastState;
        int LastAction;
        double LastReward;
        SGDMomentum Optimizer;
        Random random;

        public QLearning()
        {
            Network = new Network(Function.ReLU, 4, 3, 30, 60, 30);
            History = new History();
            random = new Random(0);
        }

        public int Update(double reward, params double[] state)
        {
            History.Add(lastState, state, LastAction, reward);
            int action = GetAction(state);

            if (History.Items.Count > 100)
            {
                Learn();
            }

            LastAction = action;
            lastState = state;
            LastReward = reward;
            return 0;
        }

        public void Learn()
        {
            IEnumerable<MemoryItem> items = History.Items.Subset(100, random);
            IEnumerable<double[]> outputs = items.Select(item => Optimizer.Network.Evaluate(item.FromState));
            IEnumerable<double[]> outputsNext = items.Select(item => Optimizer.Network.Evaluate(item.ToState));
            double[] target = outputsNext.Select(next => );
            double tdLoss = outputs.SmoothL1Loss(target);
            var data = outputs.Select(output => new Projection(output, output));
            var trainer = new Trainer(Optimizer, random);
            trainer.Train(data, 1, 32);
            //Optimizer.Evaluate(outputs, )
            //Optimizer.Evaluate()
        }

        private int GetAction(double[] state)
        {
            return Network.Evaluate(state).Softmax().RandomFromDistribution(random);
        }
    }

    public class Car
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle;
        public float Rotation;
        public Vector2 Sensor1;
        public Vector2 Sensor2;
        public Vector2 Sensor3;
        public float Signal1;
        public float Signal2;
        public float Signal3;

        public Car(float x, float y, float dx, float dy)
        {
            Position = new Vector2(x, y);
            Velocity = new Vector2(dx, dy);
        }

        public void Move(float rotation, Bitmap bitmap)
        {
            Position += Velocity;
            Angle += Rotation = rotation;
            Sensor1 = new Vector2(30, 0).Rotate(Angle) + Position;
            Sensor2 = new Vector2(30, 0).Rotate((Angle - 30) % 360) + Position;
            Sensor3 = new Vector2(30, 0).Rotate((Angle + 30) % 360) + Position;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Signal1 = Signal(Sensor1, bitmapData);
            Signal2 = Signal(Sensor2, bitmapData);
            Signal3 = Signal(Sensor3, bitmapData);
            bitmap.UnlockBits(bitmapData);
        }

        private float Signal(Vector2 sensor, BitmapData bitmapData)
        {
            return 0;
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> Subset<T>(this IEnumerable<T> self, int k, Random random)
        {
            var enumerator = self.GetEnumerator();
            double available = self.Count();
            int selected = 0;
            int needed = k;

            while (selected < k)
            {
                enumerator.MoveNext();

                if (random.NextDouble() < needed / available)
                {
                    yield return enumerator.Current;
                    needed--;
                    selected++;
                }

                available--;
            }
        }

        public static double SmoothL1Loss(this double[] self, double[] y)
        {
            double sum = 0;

            for (int i = 0; i < self.Length; i++)
            {
                double diff = Math.Abs(self[i] - y[i]);

                if (diff < 1)
                {
                    sum += 0.5 * (diff * diff);
                }
                else
                {
                    sum += diff - 0.5;
                }
            }

            return sum / self.Length;
        }

        public static int RandomFromDistribution(this double[] self, Random random)
        {
            double t = random.NextDouble();
            double k = 0;

            for (int i = 0; i < self.Length; i++)
            {
                if (k >= t)
                {
                    return i;
                }
                else
                {
                    k += self[i];
                }
            }

            return self.Length - 1;
        }

        public static double[] Softmax(this double[] self)
        {
            double[] result = self.Select(Math.Exp).ToArray();
            double sum = result.Sum();

            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= sum; 
            }

            return result;
        }

        public static Vector2 Rotate(this Vector2 self, float degrees)
        {
            float radians = ConvertDegreesToRadians(degrees);
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            float tx = self.X;
            float ty = self.Y;
            self.X = (cos * tx) - (sin * ty);
            self.Y = (sin * tx) + (cos * ty);
            return self;
        }

        public static float ConvertDegreesToRadians(float degrees)
        {
            return (float)(Math.PI / 180.0f) * degrees;
        }

        public static float Distance(this Vector2 self, Vector2 v)
        {
            return (v - self).Length();
        }
    }
}
