using System;
using System.Collections.Generic;
using System.Text;

namespace Monkeys
{
    public class Boom
    {
        public int Id { get; set; }
        public int X { get; set;}
        public int Y { get; set; }

        public Boom(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            Boom other = obj as Boom;
            double afstand = Math.Sqrt(Math.Pow(this.X-other.X,2)+ Math.Pow(this.Y-other.Y,2));

            return other != null && afstand <12;
        }
        public override int GetHashCode()
        {
            if (Id == 0)
            {
                return 0;
            }
            else
            {
                return Id.GetHashCode();
            }
        }
    }
}