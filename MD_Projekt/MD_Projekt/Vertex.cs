using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MD_Projekt
{
    internal class Vertex
    {
        public int vertexNumber;
        public int X;
        public int Y;
        public int textX;
        public int textY;
        public TextBlock text;

        public Vertex(int i, bool circle, int n)
        {
            vertexNumber = i;
            int r = 200;

            if (circle)
            {
                X = (int)(r * Math.Sin(2 * Math.PI / n * (i + 1)) + 300);
                Y = (int)(r * Math.Cos(2 * Math.PI / n * (i + 1)) + 300);
            }
            else
            {
                Random random = new Random();

                // 25px margin
                X = 25 + random.Next(550);
                Y = 25 + random.Next(550);
            }

            textX = X - 15;
            textY = Y - 5;

            text = new TextBlock();

            char ch = (char)(65 + i);
            text.Text = ch.ToString();
        }
    }
}
