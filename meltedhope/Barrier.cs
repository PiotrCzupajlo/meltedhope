using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Barrier
    {
        public short topleftx { get; set; }
        public short toprightx { get; set; }

        public short toplefty { get; set; }
        public short bottomlefty { get; set; }

        public Barrier(short toplx, short toply, short toprx, short botly)
        { 
        topleftx = toplx;
            toplefty = toply;
            toprightx = toprx;
            bottomlefty = botly;



        }
        public bool isColliding(float x, float y)
        {
            if ((x<toprightx&& x>topleftx)&& (y<bottomlefty&& y>toplefty))
                return true;
            return false;
        }
    }
}
