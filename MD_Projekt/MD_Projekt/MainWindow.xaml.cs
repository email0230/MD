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

            // getting input number
            if (!int.TryParse(vertices.Text, out var numOfVertices))
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


            /* example how to draw a line
             * 
            Line lll = new Line()
            {
                X1 = 50,
                Y1 = 50,
                X2 = 190,
                Y2 = 100,
                Stroke = Brushes.Blue,
                StrokeThickness = 2.5
            };
            grid.Children.Add(lll);
            */
        }
    }
}
