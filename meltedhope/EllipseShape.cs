using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class EllipseShape : CircleShape
    {

        public EllipseShape(float radius, Vector2f scale, uint pointCount = 50)
            : base(radius, pointCount)
        {
            this.Scale = scale;
        }
    }

}
