using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpColorGraph
{
    public class Program
    {
        // Create colors
        public static string[]  COLORS      = { "Blue", "Green", "Purple" };
        public static int       NODE_COUNT  = 6;

        public static void Main(string[] pArguments)
        {
            // 1. Construct the edges
            // 2. Create graph using the edges
            // 3. Color graph using the graph method
            // 4. Display color assigned to each vertix

            // Create edges to add to new graph object
            List<Edge>  edges = new List<Edge> { new Edge(0, 1), new Edge(0,4), new Edge(0, 5), new Edge(4, 5), new Edge(1, 4),
                                    new Edge(1,3), new Edge(2,3), new Edge(2,4)};

            // Create graph object from graph class
            Graph graph = new Graph(edges);

            // Color the graph and return the results
            Dictionary<int, int> coloredGraphResults = graph.ColorGraph();

            // Print out results
            for (int i = 0; i < Program.NODE_COUNT; i++)
                Console.WriteLine("Node: " + i + " Color: " + Program.COLORS[coloredGraphResults[i]]);
        }
    }

    // Basic edge class to use with the graph class
    public class Edge
    {
        private int m_source;
        private int m_destination;

        public Edge(int source, int destination)
        {
            this.m_source       = source;
            this.m_destination  = destination;
        }

        public int Source
        {
            get { return this.m_source; }
            set { this.m_source = value; }
        }

        public int Destination
        {
            get { return this.m_destination; }
            set { this.m_destination = value; }
        }
    }

    // Class to create and color graph
    public class Graph
    {
        private List<List<int>> m_adjacentList;

        // Construct the graph using all the edges in the constructor
        public Graph(List<Edge> edges)
        {
            this.m_adjacentList = new List<List<int>>();

            for (int i = 0; i < Program.NODE_COUNT; i++)
                this.m_adjacentList.Add(new List<int>());

            // This loop creates the list of all adjacencies
            foreach (Edge edge in edges)
            {
                int source      = edge.Source;
                int destination = edge.Destination;

                this.m_adjacentList[source].Add(destination);
                this.m_adjacentList[destination].Add(source);
            }
        }

        // Greedy method to color the internal graph and reteurn
        public Dictionary<int, int> ColorGraph()
        {
            // Store colors for each vertex
            Dictionary<int, int> result = new Dictionary<int, int>();

            // Do this for each node
            for (int nodeNum = 0; nodeNum < Program.NODE_COUNT; nodeNum++)
            {
                int             colorToCheck    = 0;
                SortedSet<int>  assignments     = new SortedSet<int>();

                // Check colors of adjacent nodes and store
                foreach (int i in this.m_adjacentList[nodeNum])
                {
                    if (result.ContainsKey(i))
                        assignments.Add(result[i]);
                }

                // Get first free color
                foreach (int assignment in assignments)
                {
                    if (colorToCheck != assignment)
                        break;
                    colorToCheck++;
                }

                result.Add(nodeNum, colorToCheck);
            }

            return result;
        }
    }
}
