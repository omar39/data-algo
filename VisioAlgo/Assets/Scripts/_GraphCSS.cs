using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    private const int graphs_count = 12;
    private static List<Vertex> vertices;
    public static System.Random random_generator;

    public static void Init(int number_of_vertices)
    {
        random_generator = new System.Random();
        vertices = new List<Vertex>(number_of_vertices);

        for (int i = 0; i < vertices.Capacity; i++)
        {
            vertices.Add(new Vertex(i, random_generator.Next(100, 255) * i + random_generator.Next(0, 32)));
        }
    }

    public static void InitGraphs()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Vertex vertex = vertices[i] as Vertex;
            vertex.links = new Vertex[random_generator.Next(1, 4)];

            for (int j = 0; j < vertex.links.Length; j++)
            {
                vertex.links[j] = vertices[random_generator.Next(0, 10)];
            }

            vertices[i] = vertex;
        }
    }

    public static bool[,] ParseUndirectedMatrix()
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

    public static bool[,] ParseDirectedMatrix()
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

    private static void PrintMatrix(ref bool[,] matrix)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Console.Write("{0} | [ ", i);

            for (int j = 0; j < vertices.Count; j++)
            {
                Console.Write(" {0},", Convert.ToInt32(matrix[i, j]));
            }

            Console.Write(" ]\r\n");
        }

        Console.Write("{0}", new string(' ', 7));

        for (int i = 0; i < vertices.Count; i++)
        {
            Console.Write("---");
        }

        Console.Write("\r\n{0}", new string(' ', 7));

        for (int i = 0; i < vertices.Count; i++)
        {
            Console.Write("{0}  ", i);
        }

        Console.Write("\r\n");
    }

    private static void PrintGraphs()
    {
        foreach (Vertex graph in vertices)
        {
            Console.Write("\r\nGraph id: {0}. It references to the graphs: ", graph.id);

            for (int i = 0; i < graph.links.Length; i++)
            {
                Console.Write(" {0}", graph.links[i].id);
            }
        }
    }

    [STAThread]
    static void Main()
    {
        try
        {
            Init(graphs_count);
            InitGraphs();
            bool[,] matrix = ParseUndirectedMatrix();
            PrintMatrix(ref matrix);
            PrintGraphs();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }

        Console.Write("\r\n\r\nPress enter to exit this program...");
        Console.ReadLine();
    }
}

public class _GraphCSS : MonoBehaviour {

    public GameObject Vertex;
    public GameObject Edge;
    public GameObject Adjagency_Matrix_Start_point;
    public GameObject Adjagency_List_Start_point;
    public GameObject Matrix_Cell;
    public GameObject List_Node;
    public int N_Vertices;
    private List<GameObject> Vertices;
    private List<GameObject>[] Adjagency_Matrix;
    private List<GameObject>[] Adjagency_List;
    private GameObject Matrix_Parent;
    private GameObject List_Parent;
    private GameObject Vertices_Parent;

    void Start () {
        Matrix_Parent = Instantiate(new GameObject());
        List_Parent = Instantiate(new GameObject());
        Vertices_Parent = Instantiate(new GameObject());
        Vertices = new List<GameObject>();
        Adjagency_Matrix = new List<GameObject>[N_Vertices];
        Adjagency_List = new List<GameObject>[N_Vertices];

        //Draw_Graph();
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
        Destroy(Matrix_Parent);
        Destroy(List_Parent);
        Destroy(Vertices_Parent);
        Matrix_Parent = Instantiate(new GameObject());
        List_Parent = Instantiate(new GameObject());
        Vertices_Parent = Instantiate(Vertices_Parent);
        Program.Init(N_Vertices);
        Program.InitGraphs();
        bool[,] Undirected_Matrix = Program.ParseUndirectedMatrix();
        bool[,] Directed_Matrix = Program.ParseDirectedMatrix();

        for (int i = 0; i != N_Vertices; i++)
        {
            Undirected_Matrix[i, i] = false;
            Directed_Matrix[i, i] = false;
        }

        int levels = N_Vertices / 6;

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

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i != N_Vertices; i++)
        {
            for (int j = 0; j != N_Vertices; j++)
            {
                if (Undirected_Matrix[i, j] == true)
                {
                    GameObject New_Edge = Instantiate(Edge, Vertices[i].transform);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(0, Vertices[i].transform.position);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(1, Vertices[j].transform.position);
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

                New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().fontMaterial.color
                    = new Color32(
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.r),
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.g),
                    (byte)(255 - New_Cell.GetComponent<SpriteRenderer>().color.b), (byte)255);

                if (Undirected_Matrix[i, j] == true)
                    New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "1";
                else
                    New_Cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "0";

                Adjagency_Matrix[i].Add(New_Cell);
            }
        }
    }    
}