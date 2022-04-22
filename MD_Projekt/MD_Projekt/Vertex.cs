using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_Projekt
{
    internal class Vertex
    {

        public int X;
        public int Y;

        public Vertex()
        {
            Random random = new Random();

            // 25px margin
            X = 25 + random.Next(550); 
            Y = 25 + random.Next(350);
        }
    }
}
