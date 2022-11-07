using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Lab7
{
    internal class Lab7
    {
        //implementation of the Kruskal's algorithm
        public static int Kruskal(int[,] graph)
        {
            int n = graph.GetLength(0);
            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
            int[] rank = new int[n];
            int cost = 0;
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (graph[i, j] != 0)
                    {
                        edges.Add(new Edge(i, j, graph[i, j]));
                    }
                }
            }
            edges.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            foreach (Edge e in edges)
            {
                int a = e.A;
                int b = e.B;
                int c = e.Cost;
                if (Find(a, parent) != Find(b, parent))
                {
                    cost += c;
                    Union(a, b, parent, rank);
                }
            }
            return cost;
        }

        private static void Union(int a, int b, int[] parent, int[] rank)
        {
            a = Find(a, parent);
            b = Find(b, parent);
            if (a != b)
            {
                if (rank[a] < rank[b])
                {
                    parent[a] = b;
                }
                else
                {
                    parent[b] = a;
                    if (rank[a] == rank[b])
                    {
                        rank[a]++;
                    }
                }
            }
        }

        private static int Find(int a, int[] parent)
        {
            if (a != parent[a])
            {
                parent[a] = Find(parent[a], parent);
            }
            return parent[a];
        }

        private class Edge
        {
            public int A { get; set; }
            public int B { get; set; }
            public int Cost { get; set; }

            public Edge(int a, int b, int cost)
            {
                A = a;
                B = b;
                Cost = cost;
            }
        }
    }
}
