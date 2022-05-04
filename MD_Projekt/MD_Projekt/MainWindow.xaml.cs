﻿using System;
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
        int[,] matrix;
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

            if (!int.TryParse(minWeightText.Text, out var minWeight))
            {
                return;
            }

            if (!int.TryParse(maxWeightText.Text, out var maxWeight))
            {
                return;
            }

            // creating vertex array
            verts = new Vertex[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            { 
                verts[i] = new Vertex(i);
            }

            //creating matrix of weights
            matrix = new int[numOfVertices,numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = 0; j < numOfVertices; j++)
                {
                    matrix[i, j] = 0;
                }
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

                Canvas.SetLeft(vertex.text, vertex.textX);
                Canvas.SetTop(vertex.text, vertex.textY);

                canvas.Children.Add(vertex.text);
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
                        // drawing line
                        LineContainer lineCon = new LineContainer(verts[i], verts[j], line, minWeight, maxWeight, matrix);

                        canvas.Children.Add(line);

                        //drawing weight
                        Canvas.SetLeft(lineCon.text, lineCon.weightX);
                        Canvas.SetTop(lineCon.text, lineCon.weightY);

                        canvas.Children.Add(lineCon.text);
                    }
                }
            }

            //robocze wyświetlanie macierzy wag z lewej strony ekranu
            StringBuilder sb = new StringBuilder();

            sb.Append("Macierz wag: \n\n");

            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = 0; j < numOfVertices; j++)
                {
                    sb.Append(matrix[i, j]);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }

            matrixBlock.Text = sb.ToString();


            //wypisywanie trójkątów
            List<string> triangles = new List<string>();

            for (int i = 0; i < numOfVertices; i++)
            {
                for (int j = i + 1; j < numOfVertices; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        for (int k = j + 1; k < numOfVertices; k++)
                        {
                            if (matrix[j, k] > 0 && matrix[k, i] > 0)
                            {
                                char c1 = (char)(i + 65);
                                char c2 = (char)(j + 65);
                                char c3 = (char)(k + 65);
                                triangles.Add(c1.ToString() + c2.ToString() + c3.ToString());
                            } 
                        }
                    } 
                }
            }

            sb = new StringBuilder();

            sb.Append("Trójkąty: \n\n");

            foreach (string tri in triangles) sb.AppendLine(tri);
            
            trianglesBlock.Text = sb.ToString();
        }

        //to część algorytmu Dijkstry do wyliczania najkrótszej drogi, wzięte z neta
        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public static int[] Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                        distance[v] = distance[u] + graph[u, v];
            }

            return distance;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            // getting input number
            int begin = start.Text[0] - 65;

            int numOfVertices = (int)Math.Sqrt(matrix.Length);

            if (matrix.Length == 0) return;

            int[] distance = Dijkstra(matrix, begin, numOfVertices);

            StringBuilder sb = new StringBuilder();

            sb.Append("Odległości od wierzchołka: \n\n");

            for (int i = 0; i < numOfVertices; ++i)
            {
                if(distance[i] == 0 || distance[i] == int.MaxValue) continue;

                sb.Append("|");
                sb.Append((char)(begin + 65));
                sb.Append((char)(i + 65));
                sb.Append("| = ");
                sb.Append(distance[i]);
                sb.Append("\n");
            }

            distanceBlock.Text = sb.ToString();
        }
    }
}
