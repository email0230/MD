using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MD_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas canvas;
        Vertex[] verts;
        Ellipse ellipse;
        Line line;
        public MainWindow()
        {
            InitializeComponent();

            CreateCanvas();
        }

        private void CreateCanvas()
        {
            canvas = new Canvas();
            canvas.Width = 600;
            canvas.Height = 400;
            canvas.Background = Brushes.Bisque;
            canvas.Margin = new Thickness(175, 0, 0, 0);

            grid.Children.Add(canvas);
        }

        private void Draw(object sender, RoutedEventArgs e)
        {
            CreateCanvas();

            Random random = new Random();

            // getting input number
            if (!int.TryParse(vertices.Text, out var numOfVertices))
            {
                return;
            }

            if (!double.TryParse(chance.Text, out var lineChance))
            {
                return;
            }

            // creating vertex array
            verts = new Vertex[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            { 
                verts[i] = new Vertex();
            }

            // drawing vertices
            foreach (var vertex in verts)
            {
                ellipse = new Ellipse()
                { 
                    Width = 10,
                    Height = 10
                };

                ellipse.Stroke = System.Windows.Media.Brushes.Black;
                ellipse.Fill = System.Windows.Media.Brushes.DarkBlue;

                ellipse.Margin = new Thickness(vertex.X, vertex.Y, 0, 0);
                
                canvas.Children.Add(ellipse);
            }

            // drawing lines
            for (int i = 0; i < verts.Length; i++)
            {
                for (int j = i + 1; j < verts.Length; j++)
                {
                    if (random.NextDouble() < lineChance) 
                    {
                        line = new Line()
                        {
                            X1 = verts[i].X + 5, // +5 dla wyśrodkowania okręgu wierzchołka
                            Y1 = verts[i].Y + 5,
                            X2 = verts[j].X + 5,
                            Y2 = verts[j].Y + 5,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        };
                        canvas.Children.Add(line);
                    }
                }
            }
        }
    }
}
