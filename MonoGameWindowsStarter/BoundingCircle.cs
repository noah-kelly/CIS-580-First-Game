using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct BoundingCircle
    {
        public float X;
        public float Y;
        public float Radius;


        // Casts circle into a rectangle
        public static implicit operator Rectangle(BoundingCircle c)
        {
            return new Rectangle(
                (int)(c.X - c.Radius),
                (int)(c.Y - c.Radius),
                (int)(2 * c.Radius),
                (int)(2 * c.Radius)
                );
        }
    }
}
