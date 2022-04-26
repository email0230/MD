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

        public Vertex(int i)
        {
            vertexNumber = i;

            Random random = new Random();

            // 25px margin
            X = 25 + random.Next(550); 
            Y = 25 + random.Next(350);

            textX = X - 15;
            textY = Y - 5;

            text = new TextBlock();

            char ch = (char)(65 + i);
            text.Text = ch.ToString();
        }
    }
}
