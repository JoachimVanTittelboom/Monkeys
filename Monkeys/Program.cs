using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Monkeys
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGames();

        }
        static void RunGames()
        {
            string path = "E:/Graduaat Programmeren/Semester 2/Programmeren4/Monkeys/Monkeys/bin/Debug/netcoreapp3.1";
            Map map = new Map(1, 0, 500, 0, 500);

            Bitmap bitmap = new Bitmap((map.MaxX - map.MinX), (map.MaxY - map.MinY));
            Random r = new Random();
            int aantal = r.Next(1, 6);
            map.MaakBos(bitmap);
            map.MaakApen(aantal, bitmap);
            
            foreach(Aap aap in map.Apen)
            {
                Spel(bitmap, map, aap);
            }
          /*  for (int i = 0; i < map.Apen.Count; i++)
            {
                Spel(bitmap, map, map.Apen[i]);
                i--;
            }*/

            bitmap.Save(Path.Combine(path, $"{1}Escape.jpg"), ImageFormat.Jpeg);
        }
        static void Spel(Bitmap bitmap, Map map, Aap aap)
        {
            for(int i=0; i<200; i++)
            {
                map.DichtsteBoom(bitmap, aap);
            }
           /* while (true)
            {
                map.DichtsteBoom(bitmap, aap);
                if(!map.Apen.Contains(aap))
                {
                    break;
                }
            }*/
        }
    }
}
