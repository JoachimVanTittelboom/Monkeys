using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

namespace Monkeys
{
    public class Map
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public List<Boom> Bomen { get; set; }
        public List<Aap> Apen { get; set; }
        public int MapId { get; set; }

        public Map(int id, int minx, int maxX, int minY, int maxY)
        {
            MinX = minx;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            Bomen = new List<Boom>();
            Apen = new List<Aap>();
            MapId = id;
        }

        public void MaakBos(Bitmap bitmap)
        {
            Random r = new Random();

            for (int i = 0; i < 300; i++)
            {
                Boom boom = new Boom(i, r.Next(MinX + 10, MaxX - 10), r.Next(MinY + 10, MaxY - 10));

                if(!Bomen.Contains(boom))
                {
                    Bomen.Add(boom);
                }
                else
                {
                    i--;
                }
            }

            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Green);

            foreach (Boom boom in Bomen)
            {
                graphics.DrawEllipse(pen, boom.X, boom.Y, 10, 10);
            }
        }

        public void MaakApen(int aantal, Bitmap bitmap)
        {
            List<Boom> ingenomenBomen = new List<Boom>();
            Random r = new Random();
            List<string> namen = new List<string>()
            {
                "Jens",
                "Jo",
                "Marnick",
                "Toon",
                "Robin",
                "Arne",
                "Steve",
                "Vlad",
                "Gregory",
                "Kevy"
            };


           for (int i = 0; i < aantal; i++)
            {
                int step = r.Next(0, 256);
                Color color = GetRainbowColor(step, 255, 128);
                int naam = r.Next(1, namen.Count);
                Aap aap = new Aap(i + 1, namen[naam], color);
                if (!Apen.Contains(aap))
                {
                    Apen.Add(aap);
                }
                else
                {
                    i--;
                }
            }
            for (int i = 0; i < Apen.Count; i++)
            {
                Boom rb = Bomen[r.Next(1, Bomen.Count)];
                if (!ingenomenBomen.Contains(rb))
                {
                    Apen[i].Bomen.Add(rb);
                    ingenomenBomen.Add(rb);
                }
                else
                {
                    i--;
                }
            }
            Graphics graphics = Graphics.FromImage(bitmap);

            foreach (Aap aap in Apen)
            {
                Brush brush = new SolidBrush(aap.Color);
                graphics.FillEllipse(brush, aap.Bomen[0].X, aap.Bomen[0].Y, 12, 12);
            }
        }

        public void DichtsteBoom(Bitmap bitmap, Aap aap)
        {
            Boom laatsteBoom = aap.Bomen[^1];

            List<Boom> georderdeBomen = Bomen.Select(x => x).OrderBy(teZoekenBoom => (Math.Abs(teZoekenBoom.X - laatsteBoom.X) + Math.Abs(teZoekenBoom.Y - laatsteBoom.Y))).ToList();
            
            foreach (Boom boom in georderdeBomen)
            {
                if (!aap.Bomen.Any(x => x.Id == boom.Id))
                {
                    double vrijheid = AfstandVrijheid(laatsteBoom);

                    double verschilx = (laatsteBoom.X + 5) - (boom.X + 5);
                    double verschilY = (laatsteBoom.Y + 5) - (boom.Y + 5);
                    double kwadraatX = Math.Pow(verschilx, 2);
                    double kwadraatY = Math.Pow(verschilY, 2);
                    double totaal = kwadraatX + kwadraatY;
                    double result = Math.Sqrt(totaal);

                    if (result < vrijheid)
                    {
                        TekenRoute(bitmap, aap, laatsteBoom, boom);
                        Console.WriteLine(aap.Naam + " " + boom.X + " " + boom.Y);                       
                        aap.Bomen.Add(boom);
                        return;
                    }
                    /*else
                    {
                        Apen.Remove(aap);
                        return;
                    }*/
                }
            }
        }

        private void TekenRoute(Bitmap bitmap, Aap aap, Boom laatsteBoom, Boom boom)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(aap.Color);

            graphics.DrawLine(pen, new Point(laatsteBoom.X + 5, laatsteBoom.Y + 5), new Point(boom.X + 5, boom.Y + 5));

        }

        private double AfstandVrijheid(Boom laatsteBoom)
        {
            double afstandRand = (new List<double>() { MaxY - laatsteBoom.Y, MaxX-laatsteBoom.X, laatsteBoom.Y-MinY,laatsteBoom.X-MinX}).Min();
            return afstandRand;

        }

        static public Color GetRainbowColor(int step, int numOfSteps, byte alpha)
        {
            float r = 0, g = 0, b = 0;
            float h = (float)step / numOfSteps;
            float i = (int)(h * 6f);
            float f = h * 6f - i;
            float q = 1f - f;
            switch ((int)i % 6)
            {
                case 0: r = 1; g = f; b = 0; break;
                case 1: r = q; g = 1; b = 0; break;
                case 2: r = 0; g = 1; b = f; break;
                case 3: r = 0; g = q; b = 1; break;
                case 4: r = f; g = 0; b = 1; break;
                case 5: r = 1; g = 0; b = q; break;
            }
            Color result = Color.FromArgb(alpha, (int)(r * 255), (int)(g * 255), (int)(b * 255));
            return result;
        }
    }
}
