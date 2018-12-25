using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class DijkstraCSS : MonoBehaviour {

    public GameObject _Vertex;
    public GameObject Known;
    public GameObject Cost;
    public GameObject Compare_Cost;
    public float Offset;
    //public GameObject Path;
    public GameObject TableCell;
    private List<GameObject> VertexList;
    private List<GameObject> KnownList;
    private List<GameObject> CostList;
    //private List<GameObject> PathList;
    public GameObject Vertex;
    public GameObject Edge;
    private int N_Vertices;
    private List<GameObject> Vertices;
    private GameObject Vertices_Parent;
    private Vector3 Graph_Start_Point;

    private bool stop;

    void Start () {
        Graph_Start_Point = gameObject.transform.GetChild(0).transform.localPosition;
    }

    void Update () {
		
	}

    private IEnumerator Change_Color(GameObject cell)
    {
        int r = 255;
        int change = -1;

        while(!stop)
        {
            cell.GetComponent<SpriteRenderer>().color = new Color32((byte)r, (byte)0, (byte)0, (byte)255);
            r += change;
            if (r <= 0)
                change = 1;
            if (r > 255)
                change = -1;
            yield return null;
        }

        //print("Here");

        cell.GetComponent<SpriteRenderer>().color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
        StopCoroutine(Change_Color(cell));

        //print("also Here");
    }

    private IEnumerator Search_Cheapest_Known()
    {
        for(int i = 0; i != CostList.Count; i++)
        {
            if (KnownList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text == "F") 
                StartCoroutine(Change_Color(CostList[i]));
        }        

        //print("Before");
        yield return new WaitForSeconds(2.5f);
        //print("After");

        stop = true;
    }

    private IEnumerator Set_Known(int index)
    {
        StartCoroutine(Change_Color(KnownList[index]));

        //print("Before");
        yield return new WaitForSeconds(2.5f);
        //print("After");

        stop = true;

        KnownList[index].GetComponentInChildren<TextMeshPro>().text = "T";
        yield return null;
    }

    private IEnumerator Set_Cost(int index, int cost)
    {
        StartCoroutine(Change_Color(CostList[index]));

        //print("Before");
        yield return new WaitForSeconds(2.5f);
        //print("After");

        stop = true;

        CostList[index].GetComponentInChildren<TextMeshPro>().text = cost.ToString();
        yield return null;
    }

    private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
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

    public IEnumerator DijkstraAlgo(int[,] graph, int source, int verticesCount)
    {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }

        //0
        distance[source] = 0;
        stop = false;
        yield return StartCoroutine(Set_Cost(source, 0));

        for (int count = 0; count < verticesCount; ++count)
        {
            //1
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);

            print(count + "Finding Cheapest Unknown Vertex...");
            stop = false;
            yield return StartCoroutine(Search_Cheapest_Known());
            print("Cheapest Unknown Vertex : " + u);

            yield return new WaitForSeconds(5);

            //2
            shortestPathTreeSet[u] = true;

            print(count + "Setting Known Field To True");
            stop = false;
            yield return StartCoroutine(Set_Known(u));

            print("Unpdating Neighbours of Vertex " + u);
            yield return new WaitForSeconds(5);

            //3
            for (int v = 0; v < verticesCount; v++)
            {               
                Compare_Cost.transform.position = CostList[v].transform.position + new Vector3(Offset, 0, 0);
                Compare_Cost.GetComponent<TextMeshPro>().text = distance[v].ToString() + " > " + (distance[u] + " + " + graph[u, v]).ToString();

                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                {
                    distance[v] = distance[u] + graph[u, v];

                    stop = false;
                    yield return StartCoroutine(Set_Cost(v, distance[u] + graph[u, v]));
                    yield return new WaitForSeconds(2.5f);
                }
            }
            Compare_Cost.GetComponent<TextMeshPro>().text = "";
        }
        print("Finished");
    }

    void Draw_Graph(int [,]matrix)
    {
        if (N_Vertices < 2)
            return;

        //print("hey");

        Destroy(Vertices_Parent);
        Vertices_Parent = new GameObject("Vertices_Parent");
        Vertices = new List<GameObject>();

        for (int i = 0; i != N_Vertices; i++)
        {
            matrix[i, i] = 0;
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
                obj.name = "Vetex " + node.ToString();
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
                if (matrix[i, j] != 0)
                {
                    GameObject New_Edge = Instantiate(Edge, Vertices[i].transform);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(0, Vertices[i].transform.position);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(1, Vertices[j].transform.position);
                    New_Edge.transform.GetChild(0).GetComponent<TextMeshPro>().text = matrix[i, j].ToString();
                    if (matrix[j, i] != 0 && matrix[j, i] != matrix[i, j])
                        if (i < j)
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(true);
                        else
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(false);
                    Vertices[i].GetComponent<_VertexCSS>().Edges.Add(New_Edge);
                    Vertices[i].GetComponent<_VertexCSS>().Connected_Vertices.Add(Vertices[j]);                 
                }
            }
        }
    }

    void Set_Dijkstra_Table()
    {
        //Destroy(_Vertex);
        //Destroy(Known);
        //Destroy(Cost);
        //_Vertex = new GameObject("v");
        //Known = new GameObject("k");
        //Cost = new GameObject("c");
        //_Vertex.transform.position = new Vector3(-16, 9, 0);
        //Known.transform.position = new Vector3(-14, 9, 0);
        //Cost.transform.position = new Vector3(-12, 9, 0);

        VertexList = new List<GameObject>();
        for (int i = 1; i <= N_Vertices; i++)
        {
            GameObject new_vertex = Instantiate(TableCell, _Vertex.transform.position + i * new Vector3(0, -2, 0), Quaternion.identity);
            new_vertex.transform.GetChild(0).GetComponent<TextMeshPro>().text = (i - 1).ToString();
            new_vertex.transform.parent = _Vertex.transform;
            VertexList.Add(new_vertex);
        }
        KnownList = new List<GameObject>();
        for (int i = 1; i <= N_Vertices; i++)
        {
            GameObject new_known = Instantiate(TableCell, Known.transform.position + i * new Vector3(0, -2, 0), Quaternion.identity);
            new_known.transform.GetChild(0).GetComponent<TextMeshPro>().text = "F";
            new_known.transform.parent = Known.transform;
            KnownList.Add(new_known);
        }
        CostList = new List<GameObject>();
        for (int i = 1; i <= N_Vertices; i++)
        {
            GameObject new_cost = Instantiate(TableCell, Cost.transform.position + i * new Vector3(0, -2, 0), Quaternion.identity);
            new_cost.transform.GetChild(0).GetComponent<TextMeshPro>().text = "inf";
            new_cost.transform.parent = Cost.transform;
            CostList.Add(new_cost);
        }
        //PathList = new List<GameObject>();
        //for (int i = 1; i <= 10; i++)
        //{
        //    GameObject new_path = Instantiate(TableCell, Path.transform.position + i * new Vector3(0, -2, 0), Quaternion.identity);
        //    new_path.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-1";
        //    new_path.transform.parent = Path.transform;
        //    PathList.Add(new_path);
        //}
    }

    public void Run_Dijkstra()
    {
        N_Vertices = 6;

        Program.Initiate_Graph(N_Vertices);
        Program.Intiate_Vertices(N_Vertices);
        int[,] Undirected_Matrix = Program.UnDierectedWeightedMatrix();
        int[,] Directed_Matrix = Program.DierectedWeightedMatrix();

        Set_Dijkstra_Table();
        Draw_Graph(Undirected_Matrix);

        for(int i = 0; i != N_Vertices; i++)
        {
            for (int j = 0; j != N_Vertices; j++)
            {
                print(Undirected_Matrix[i,j]);
            }
        }
     
        StartCoroutine(DijkstraAlgo(Undirected_Matrix, 0, N_Vertices));
    }
}