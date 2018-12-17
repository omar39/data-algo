using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphMatrixCSS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//class Vertex
//{
//    internal int id;
//    //private int value;
//    internal Vertex[] links;

//    public Vertex(int inc_id, int inc_value)
//    {
//        this.id = inc_id;
//        //this.value = inc_value;
//        links = new Vertex[Program.random_generator.Next(0, 4)];
//    }
//}

//class Program
//{
//    private const int graphs_count = 9; // 10
//    private static List<Vertex> vertices;
//    public static System.Random random_generator;

//    private static void Init()
//    {
//        random_generator = new System.Random();
//        vertices = new List<Vertex>(graphs_count);

//        for (int i = 0; i < vertices.Capacity; i++)
//        {
//            vertices.Add(new Vertex(i, random_generator.Next(100, 255) * i + random_generator.Next(0, 32)));
//        }
//    }

//    private static void InitGraphs()
//    {
//        for (int i = 0; i < vertices.Count; i++)
//        {
//            Vertex graph = vertices[i] as Vertex;
//            graph.links = new Vertex[random_generator.Next(1, 4)];

//            for (int j = 0; j < graph.links.Length; j++)
//            {
//                graph.links[j] = vertices[random_generator.Next(0, 10)];
//            }

//            vertices[i] = graph;
//        }
//    }

//    private static bool[,] ParseAdjectiveMatrix()
//    {
//        bool[,] matrix = new bool[vertices.Count, vertices.Count];

//        foreach (Vertex vertex in vertices)
//        {
//            int[] links = new int[vertex.links.Length];

//            for (int i = 0; i < links.Length; i++)
//            {
//                links[i] = vertex.links[i].id;
//                //

//                //
//                matrix[vertex.id, links[i]] = matrix[links[i], vertex.id] = true;
//            }
//        }

//        return matrix;
//    }

//    private static void PrintMatrix(ref bool[,] matrix)
//    {
//        for (int i = 0; i < vertices.Count; i++)
//        {
//            Console.Write("{0} | [ ", i);

//            for (int j = 0; j < vertices.Count; j++)
//            {
//                Console.Write(" {0},", Convert.ToInt32(matrix[i, j]));
//            }

//            Console.Write(" ]\r\n");
//        }

//        Console.Write("{0}", new string(' ', 7));

//        for (int i = 0; i < vertices.Count; i++)
//        {
//            Console.Write("---");
//        }

//        Console.Write("\r\n{0}", new string(' ', 7));

//        for (int i = 0; i < vertices.Count; i++)
//        {
//            Console.Write("{0}  ", i);
//        }

//        Console.Write("\r\n");
//    }

//    private static void PrintGraphs()
//    {
//        foreach (Vertex graph in vertices)
//        {
//            Console.Write("\r\nGraph id: {0}. It references to the graphs: ", graph.id);

//            for (int i = 0; i < graph.links.Length; i++)
//            {
//                Console.Write(" {0}", graph.links[i].id);
//            }
//        }
//    }

//    [STAThread]
//    static void Main()
//    {
//        try
//        {
//            Init();
//            InitGraphs();
//            bool[,] matrix = ParseAdjectiveMatrix();
//            PrintMatrix(ref matrix);
//            PrintGraphs();
//        }
//        catch (Exception exc)
//        {
//            Console.WriteLine(exc.Message);
//        }

//        Console.Write("\r\n\r\nPress enter to exit this program...");
//        Console.ReadLine();
//    }
//}