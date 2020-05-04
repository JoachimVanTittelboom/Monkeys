using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
namespace Monkeys
{
    public class Aap
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public Color Color { get; set; }
        public List<Boom>Bomen { get; set; }

        public Aap (int id , string naam, Color color)
        {
            Id = id;
            Naam = naam;
            Color = color;
            Bomen = new List<Boom>();
        }

        public override bool Equals(object obj)
        {
            return obj is Aap other && other.Naam == this.Naam;
        }
        public override int GetHashCode()
        {
            if(Id == 0)
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