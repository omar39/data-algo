using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text;
using System.IO;

class Vertex
{
    internal int id;
    //private int value;
    internal Vertex[] links;

    public Vertex(int inc_id, int inc_value)
    {
        this.id = inc_id;
        //this.value = inc_value;
        links = new Vertex[Program.random_generator.Next(0, 4)];
    }
}

class Program
{
    private static List<Vertex> vertices;
    public static System.Random random_generator;

    public static void Initiate_Graph(int number_of_vertices)
    {
        random_generator = new System.Random();
        vertices = new List<Vertex>(number_of_vertices);

        for (int i = 0; i < vertices.Capacity; i++)
        {
            vertices.Add(new Vertex(i, random_generator.Next(100, 255) * i + random_generator.Next(0, 32)));
        }
    }

    public static void Intiate_Vertices(int number_of_vertices)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Vertex vertex = vertices[i] as Vertex;
            vertex.links = new Vertex[random_generator.Next(1, 4)];

            for (int j = 0; j < vertex.links.Length; j++)
            {
                vertex.links[j] = vertices[random_generator.Next(0, number_of_vertices)];
            }

            vertices[i] = vertex;
        }
    }

    public static bool[,] UnDierectedWeightlessMatrix()
    {
        bool[,] matrix = new bool[vertices.Count, vertices.Count];

        foreach (Vertex vertex in vertices)
        {
            int[] links = new int[vertex.links.Length];
            
            for (int i = 0; i < links.Length; i++)
            {
                links[i] = vertex.links[i].id;
                matrix[vertex.id, links[i]] = matrix[links[i], vertex.id] = true;
            }
        }

        return matrix;
    }

    public static bool[,] DierectedWeightlessMatrix()
    {
        bool[,] matrix = new bool[vertices.Count, vertices.Count];

        foreach (Vertex vertex in vertices)
        {
            int[] links = new int[vertex.links.Length];

            for (int i = 0; i < links.Length; i++)
            {
                links[i] = vertex.links[i].id;
                matrix[vertex.id, links[i]] = true;
            }
        }

        return matrix;
    }

    public static int[,] UnDierectedWeightedMatrix()
    {
        int[,] matrix = new int[vertices.Count, vertices.Count];

        foreach (Vertex vertex in vertices)
        {
            int[] links = new int[vertex.links.Length];

            for (int i = 0; i < links.Length; i++)
            {
                links[i] = vertex.links[i].id;
                matrix[vertex.id, links[i]] = matrix[links[i], vertex.id] = random_generator.Next(1, 12);
            }
        }

        return matrix;
    }

    public static int[,] DierectedWeightedMatrix()
    {
        int[,] matrix = new int[vertices.Count, vertices.Count];

        foreach (Vertex vertex in vertices)
        {
            int[] links = new int[vertex.links.Length];

            for (int i = 0; i < links.Length; i++)
            {
                links[i] = vertex.links[i].id;
                matrix[vertex.id, links[i]] = random_generator.Next(1, 12);
            }
        }

        return matrix;
    }
}

//namespace KoderDojo.Examples
//{
//    public static class Generic<T> where T : class 
//    {
//        public static class Tuple
//        {
//            /// <summary>
//            /// Creates a new tuple value with the specified elements. The method
//            /// can be used without specifying the generic parameters, because C#
//            /// compiler can usually infer the actual types.
//            /// </summary>
//            /// <param name="item1">First element of the tuple</param>
//            /// <param name="second">Second element of the tuple</param>
//            /// <returns>A newly created tuple</returns>
//            private static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 second)
//            {
//                return new Tuple<T1, T2>(item1, second);
//            }

//            /// <summary>
//            /// Creates a new tuple value with the specified elements. The method
//            /// can be used without specifying the generic parameters, because C#
//            /// compiler can usually infer the actual types.
//            /// </summary>
//            /// <param name="item1">First element of the tuple</param>
//            /// <param name="second">Second element of the tuple</param>
//            /// <param name="third">Third element of the tuple</param>
//            /// <returns>A newly created tuple</returns>
//            public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 second, T3 third)
//            {
//                return new Tuple<T1, T2, T3>(item1, second, third);
//            }

//            /// <summary>
//            /// Creates a new tuple value with the specified elements. The method
//            /// can be used without specifying the generic parameters, because C#
//            /// compiler can usually infer the actual types.
//            /// </summary>
//            /// <param name="item1">First element of the tuple</param>
//            /// <param name="second">Second element of the tuple</param>
//            /// <param name="third">Third element of the tuple</param>
//            /// <param name="fourth">Fourth element of the tuple</param>
//            /// <returns>A newly created tuple</returns>
//            public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 second, T3 third, T4 fourth)
//            {
//                return new Tuple<T1, T2, T3, T4>(item1, second, third, fourth);
//            }

//            /// <summary>
//            /// Extension method that provides a concise utility for unpacking
//            /// tuple components into specific out parameters.
//            /// </summary>
//            /// <param name="tuple">the tuple to unpack from</param>
//            /// <param name="ref1">the out parameter that will be assigned tuple.Item1</param>
//            /// <param name="ref2">the out parameter that will be assigned tuple.Item2</param>
//            public static void Unpack<T1, T2>(this Tuple<T1, T2> tuple, out T1 ref1, out T2 ref2)
//            {
//                ref1 = tuple.Item1;
//                ref2 = tuple.Item2;
//            }

//            /// <summary>
//            /// Extension method that provides a concise utility for unpacking
//            /// tuple components into specific out parameters.
//            /// </summary>
//            /// <param name="tuple">the tuple to unpack from</param>
//            /// <param name="ref1">the out parameter that will be assigned tuple.Item1</param>
//            /// <param name="ref2">the out parameter that will be assigned tuple.Item2</param>
//            /// <param name="ref3">the out parameter that will be assigned tuple.Item3</param>
//            public static void Unpack<T1, T2, T3>(this Tuple<T1, T2, T3> tuple, out T1 ref1, out T2 ref2, T3 ref3)
//            {
//                ref1 = tuple.Item1;
//                ref2 = tuple.Item2;
//                ref3 = tuple.Item3;
//            }

//            /// <summary>
//            /// Extension method that provides a concise utility for unpacking
//            /// tuple components into specific out parameters.
//            /// </summary>
//            /// <param name="tuple">the tuple to unpack from</param>
//            /// <param name="ref1">the out parameter that will be assigned tuple.Item1</param>
//            /// <param name="ref2">the out parameter that will be assigned tuple.Item2</param>
//            /// <param name="ref3">the out parameter that will be assigned tuple.Item3</param>
//            /// <param name="ref4">the out parameter that will be assigned tuple.Item4</param>
//            public static void Unpack<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple, out T1 ref1, out T2 ref2, T3 ref3, T4 ref4)
//            {
//                ref1 = tuple.Item1;
//                ref2 = tuple.Item2;
//                ref3 = tuple.Item3;
//                ref4 = tuple.Item4;
//            }
//        }
//    }

//    public class Graph<T>
//    {
//        public Graph() { }
//        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
//        {
//            foreach (var vertex in vertices)
//                AddVertex(vertex);

//            foreach (var edge in edges)
//                AddEdge(edge);
//        }

//        public Dictionary<T, HashSet<T>> AdjacencyList  = new Dictionary<T, HashSet<T>>();

//        public void AddVertex(T vertex)
//        {
//            AdjacencyList[vertex] = new HashSet<T>();
//        }

//        public void AddEdge(Tuple<T, T> edge)
//        {
//            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
//            {
//                AdjacencyList[edge.Item1].Add(edge.Item2);
//                AdjacencyList[edge.Item2].Add(edge.Item1);
//            }
//        }
//    }

//    public class Algorithms
//    {
//        public HashSet<T> DFS<T>(Graph<T> graph, T start)
//        {
//            var visited = new HashSet<T>();

//            if (!graph.AdjacencyList.ContainsKey(start))
//                return visited;

//            var stack = new Stack<T>();
//            stack.Push(start);

//            while (stack.Count > 0)
//            {
//                var vertex = stack.Pop();

//                if (visited.Contains(vertex))
//                    continue;

//                visited.Add(vertex);


//                foreach (var neighbor in graph.AdjacencyList[vertex])
//                    if (!visited.Contains(neighbor))
//                        stack.Push(neighbor);
//            }

//            return visited;
//        }

//        public HashSet<T> BFS<T>(Graph<T> graph, T start)
//        {
//            var visited = new HashSet<T>();

//            if (!graph.AdjacencyList.ContainsKey(start))
//                return visited;

//            var queue = new Queue<T>();
//            queue.Enqueue(start);

//            while (queue.Count > 0)
//            {
//                var vertex = queue.Dequeue();

//                if (visited.Contains(vertex))
//                    continue;

//                visited.Add(vertex);

//                foreach (var neighbor in graph.AdjacencyList[vertex])
//                    if (!visited.Contains(neighbor))
//                        queue.Enqueue(neighbor);
//            }

//            return visited;
//        }
//    }

//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//            var edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
//                Tuple.Create(2,4), Tuple.Create(3,5), Tuple.Create(3,6),
//                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
//                Tuple.Create(5,6), Tuple.Create(8,9), Tuple.Create(9,10)};

//            var graph = new Graph<int>(vertices, edges);
//            var algorithms = new Algorithms();

//            Console.WriteLine("DFS: " + string.Join(", ", algorithms.DFS(graph, 1)));
//            Console.WriteLine("BFS: " + string.Join(", ", algorithms.BFS(graph, 1)));
//            Console.ReadLine();
//        }
//    }
//}

//namespace DijkstraAlgorithm
//{
//    class Dijkstra
//    {
//        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
//        {
//            int min = int.MaxValue;
//            int minIndex = 0;

//            for (int v = 0; v < verticesCount; ++v)
//            {
//                if (shortestPathTreeSet[v] == false && distance[v] <= min)
//                {
//                    min = distance[v];
//                    minIndex = v;
//                }
//            }

//            return minIndex;
//        }

//        private static void Print(int[] distance, int verticesCount)
//        {
//            Console.WriteLine("Vertex    Distance from source");

//            for (int i = 0; i < verticesCount; ++i)
//                Console.WriteLine("{0}\t  {1}", i, distance[i]);
//        }

//        public static void DijkstraAlgo(int[,] graph, int source, int verticesCount)
//        {
//            int[] distance = new int[verticesCount];
//            bool[] shortestPathTreeSet = new bool[verticesCount];

//            for (int i = 0; i < verticesCount; ++i)
//            {
//                distance[i] = int.MaxValue;
//                shortestPathTreeSet[i] = false;
//            }

//            distance[source] = 0;

//            for (int count = 0; count < verticesCount - 1; ++count)
//            {
//                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
//                shortestPathTreeSet[u] = true;

//                for (int v = 0; v < verticesCount; ++v)
//                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
//                        distance[v] = distance[u] + graph[u, v];
//            }

//            Print(distance, verticesCount);
//        }

//        static void Main(string[] args)
//        {
//            int[,] graph =  {
//                            { 0, 6, 0, 0, 0, 0, 0, 9, 0 },
//                            { 6, 0, 9, 0, 0, 0, 0, 11, 0 },
//                            { 0, 9, 0, 5, 0, 6, 0, 0, 2 },
//                            { 0, 0, 5, 0, 9, 16, 0, 0, 0 },
//                            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
//                            { 0, 0, 6, 0, 10, 0, 2, 0, 0 },
//                            { 0, 0, 0, 16, 0, 2, 0, 1, 6 },
//                            { 9, 11, 0, 0, 0, 0, 1, 0, 5 },
//                            { 0, 0, 2, 0, 0, 0, 6, 5, 0 }
//                            };

//            DijkstraAlgo(graph, 0, 9);
//            Console.ReadLine();
//        }
//    }
//}

namespace FloydWarshallAlgorithm
{
    class FloydWarshallAlgo
    {

        public const int cst = 9999;

        private static void Print(int[,] distance, int verticesCount)
        {
            Console.WriteLine("Shortest distances between every pair of vertices:");

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (distance[i, j] == cst)
                        Console.Write("cst".PadLeft(7));
                    else
                        Console.Write(distance[i, j].ToString().PadLeft(7));
                }

                Console.WriteLine();
            }
        }

        public static void FloydWarshall(int[,] graph, int verticesCount)
        {
            int[,] distance = new int[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; ++i)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < verticesCount; ++k)
            {
                for (int i = 0; i < verticesCount; ++i)
                {
                    for (int j = 0; j < verticesCount; ++j)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                            distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }

            Print(distance, verticesCount);
        }

        static void Main(string[] args)
        {
            int[,] graph = {
                         { 0,   6,  cst, 11 },
                         { cst, 0,   4, cst },
                         { cst, cst, 0,   2 },
                         { cst, cst, cst, 0 }
                           };

            FloydWarshall(graph, 4);
        }
    }
}

public class _GraphCSS : MonoBehaviour {

    public GameObject Vertex;
    public GameObject Edge;
    public GameObject Adjagency_Matrix_Start_point;
    public GameObject Adjagency_List_Start_point;
    public GameObject Matrix_Cell;
    public GameObject List_Node;
    private int N_Vertices;
    private List<GameObject> Vertices;
    private List<GameObject>[] Adjagency_Matrix;
    private List<GameObject>[] Adjagency_List;
    private GameObject Matrix_Parent;
    private GameObject List_Parent;
    private GameObject Vertices_Parent;
    private Vector3 Graph_Start_Point;
    private Vector3 screenPoint;
    private Vector3 offset;

    void Start () {
        Graph_Start_Point = gameObject.transform.GetChild(0).transform.localPosition;
        Vertices = new List<GameObject>();
    }
	
	void Update () {

        // dosen't work
        //gameObject.transform.rotation = new Quaternion(0, 0, i * 45, 0);
        for (int i = 0; i < N_Vertices; i++)
        {
            for (int j = 0; j < N_Vertices; j++)
            {
                Adjagency_Matrix[i][j].transform.position = Adjagency_Matrix_Start_point.transform.position +
                    new Vector3(j * (Matrix_Cell.transform.localScale.x) + 1, -(i * Matrix_Cell.transform.localScale.y) - 1, 0);
            }
        }

        var Direction = Input.GetAxis("Mouse ScrollWheel");

        //if(Input.GetKey(KeyCode.Mouse2))
        //{
        //    print("Yes");

        //    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        //    Camera.main.transform.position = curPosition;
        //}

        if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
        {
            if (Direction < 0)
            {
                Camera.main.transform.position += new Vector3(1, 0, 0);
                gameObject.transform.position += new Vector3(1, 0, 0);
            }
            else
            if (Direction > 0)
            {
                Camera.main.transform.position += new Vector3(-1, 0, 0);
                gameObject.transform.position += new Vector3(-1, 0, 0);
            }
            //doesn't work
            //gameObject.transform.position = Camera.main.transform.position;
        }
        else
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            if (Direction < 0)
            {
                Camera.main.transform.position += new Vector3(0, -1, 0);
                gameObject.transform.position += new Vector3(0, -1, 0);
            }
            else
            if (Direction > 0)
            {
                Camera.main.transform.position += new Vector3(0, 1, 0);
                gameObject.transform.position += new Vector3(0, 1, 0);
            }
            //doesn't work
            //gameObject.transform.position = Camera.main.transform.position;
        }
        else
        if(Direction != 0)
        {
            Camera.main.orthographicSize += (Direction > 0)? -1: 1;
        }
    }

    public void Draw_Graph()
    {
        if (N_Vertices < 2)
            return;

        Destroy(Matrix_Parent);
        Destroy(List_Parent);
        Destroy(Vertices_Parent);
        Matrix_Parent = new GameObject("Matrix_Parent");
        Vertices_Parent = new GameObject("Vertices_Parent");
        Vertices = new List<GameObject>();

        Program.Initiate_Graph(N_Vertices);
        Program.Intiate_Vertices(N_Vertices);
        bool[,] Undirected_Matrix = Program.UnDierectedWeightlessMatrix();
        bool[,] Directed_Matrix = Program.DierectedWeightlessMatrix();

        for (int i = 0; i != N_Vertices; i++)
        {
            Undirected_Matrix[i, i] = false;
            Directed_Matrix[i, i] = false;
        }

        int levels = N_Vertices / 6;

        if (N_Vertices < 11)
        {
            levels = 5;
        }

        int node = 0;
        for (int j = 1; j <= levels + 1; j++)
        {
            for (int i = 1; i <= 6; i++)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(60 * i, Vector3.forward);
                GameObject obj = Instantiate(Vertex, gameObject.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
                obj.transform.parent = Vertices_Parent.transform;
                obj.name = "Vetex" + node.ToString();
                obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = node.ToString();
                obj.GetComponent<_VertexCSS>().Set_Edge(Edge);
                Vertices.Add(obj);
                node++;

                if (node == N_Vertices)
                    break;
            }

            if (node == N_Vertices)
                break;

            gameObject.transform.GetChild(0).gameObject.transform.position += new Vector3(
                2, 2, 0);
            gameObject.transform.rotation = Quaternion.AngleAxis(30f * j, Vector3.forward);
        }

        gameObject.transform.GetChild(0).transform.localPosition = Graph_Start_Point;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i != N_Vertices; i++)
        {
            for (int j = 0; j != N_Vertices; j++)
            {
                if (Directed_Matrix[i, j] == true)
                {
                    GameObject New_Edge = Instantiate(Edge, Vertices[i].transform);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(0, Vertices[i].transform.position);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(1, Vertices[j].transform.position);
                    if (Directed_Matrix[j, i] == true)
                        if (i < j)
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(true);
                        else
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(false);
                    Vertices[i].GetComponent<_VertexCSS>().Edges.Add(New_Edge);
                    Vertices[i].GetComponent<_VertexCSS>().Connected_Vertices.Add(Vertices[j]);

                    // don't call a function 
                    //Vertices[i].GetComponent<_VertexCSS>().Connect_Edge(Vertices[j]); 
                }
            }
        }

        for (int i = 0; i < N_Vertices; i++)
        {
            Adjagency_Matrix[i] = new List<GameObject>();

            for (int j = 0; j < N_Vertices; j++)
            {
                GameObject New_Cell = Instantiate(Matrix_Cell, Adjagency_Matrix_Start_point.transform.position +
                    new Vector3(j * (Matrix_Cell.transform.localScale.x) + 1, -(i * Matrix_Cell.transform.localScale.y) - 1, 0), Quaternion.identity);

                New_Cell.transform.parent = Matrix_Parent.transform;

                New_Cell.GetComponent<SpriteRenderer>().color = new Color32(
                    (byte)Program.random_generator.Next(0, 255),
                    (byte)Program.random_generator.Next(0, 255),
                    (byte)Program.random_generator.Next(0, 255), (byte)255);

                New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color
                    = new Color32(
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.r),
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.g),
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.b), (byte)255);

                if (Directed_Matrix[i, j] == true)
                    New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "1";
                else
                    New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "0";

                Adjagency_Matrix[i].Add(New_Cell);
            }
        }

        //for (int i = 0; i != N_Vertices; i++)
        //{
        //    GameObject New_Head = Instantiate(List_Node, Adjagency_Matrix_Start_point.transform.position + new Vector3(0, 3, 0), Quaternion.identity);

        //    for (int j = 0; j != Vertices[i].GetComponent<_VertexCSS>().Connected_Vertices.Count; j++)
        //    {
        //        GameObject New_Node = Instantiate(List_Node, New_Head.transform.position + new Vector3(3, 0, 0), Quaternion.identity);


        //    }
        //}
    }    

    public void Set_N_Vertieces(Text n_vertices)
    {      
        N_Vertices = Convert.ToInt32(n_vertices.text);
        Adjagency_Matrix = new List<GameObject>[N_Vertices];
        Adjagency_List = new List<GameObject>[N_Vertices];
    }
}