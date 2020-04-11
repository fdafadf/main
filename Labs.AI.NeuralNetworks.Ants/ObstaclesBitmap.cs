using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public unsafe class ObstaclesBitmap : IDisposable
    {
        Bitmap Bitmap;
        BitmapData Data;
        byte* Scan0;
        byte BytesPerPixel;

        public ObstaclesBitmap(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Bitmap = bitmap;
            Data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Scan0 = (byte*)Data.Scan0.ToPointer();
            BytesPerPixel = 4;
        }

        public double GetSignal(AntSensor sensor)
        {
            return GetSignal(sensor.Position, sensor.Sensor.Size);
        }

        public double GetSignal(Vector2 position, int size)
        {
            double signal = 0;
            int cx = (int)position.X - size;
            int cy = (int)position.Y - size;

            for (int y = 0; y < size * 2; y++)
            {
                for (int x = 0; x < size * 2; x++)
                {
                    int px = cx + x;

                    if (px < 0 || px >= Data.Width)
                    {
                        signal += 1.0 / 400;
                    }
                    else
                    {
                        int py = cy + y;

                        if (py < 0 || py >= Data.Height)
                        {
                            signal += 1.0 / 400;
                        }
                        else
                        {
                            byte* data = Scan0 + py * Data.Stride + px * BytesPerPixel;
                            //Console.WriteLine(data[0]);

                            if (data[0] == Color.Yellow.B && data[1] == Color.Yellow.G)
                            {
                                signal += 1.0 / 400;
                            }
                        }
                    }
                }
            }

            return signal;
        }

        public bool CollideRectangle(Vector2 position, int size)
        {
            double signal = GetSignal(position, size);
            //Console.WriteLine($"{signal}");
            return signal > 0;
        }

        public void Dispose()
        {
            Bitmap.UnlockBits(Data);
        }
    }
}
