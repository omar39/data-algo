using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class GenerateGraphCSS : MonoBehaviour {

    public GameObject MyCursor;
    public float Speed;
    public GameObject Vertex;
    public GameObject Edge;
    public GameObject Adjagency_Matrix_Start_point;
    public GameObject Adjagency_List_Start_point;
    public GameObject Matrix_Cell;
    public GameObject List_Node;
    private int[,] AdjagencyMatrix;
    private int N_Vertices;
    private List<GameObject>[] Adjagency_List;
    private List<GameObject> Vertices;
    private GameObject Matrix_Parent;
    private GameObject List_Parent;
    private GameObject Vertices_Parent;
    private Vector3 Graph_Start_Point;
    List<bool> Visited;

    void Start () {
        Graph_Start_Point = gameObject.transform.GetChild(0).transform.localPosition;
        Vertices = new List<GameObject>();
        Visited = new List<bool>();
    }
	
	void Update () {

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
        }
        else
        if (Direction != 0)
        {
            Camera.main.orthographicSize += (Direction > 0) ? -1 : 1;
        }
    }

    public void Set_Adjagency_Matrix(InputField inputField)
    {
        if (!inputField.multiLine)
            return;

        string input = inputField.textComponent.text;
        string []lines = input.Split('\n');
        string[] line;
        AdjagencyMatrix = new int[lines.Length, lines.Length];
        Adjagency_List = new List<GameObject>[lines.Length];
        N_Vertices = lines.Length;
        for (int i = 0; i < N_Vertices; i++)
        {
            Visited.Add(false);
        }

        for(int i = 0; i != lines.Length; i++)
        {
            line = lines[i].Split(' ');
            if (line.Length != lines.Length)
                return;

            for(int j = 0; j != line.Length; j++)
            {
                AdjagencyMatrix[i, j] = int.Parse(line[j]);

                if (AdjagencyMatrix[i, j] != 0 && i == j)
                    return;
            }
        }

        StartCoroutine(Draw_Graph());
    }

    private IEnumerator Draw_Graph()
    {
        if (N_Vertices < 2)
            yield return null;

        Destroy(Matrix_Parent);
        Destroy(List_Parent);
        Destroy(Vertices_Parent);
        Matrix_Parent = new GameObject("Matrix_Parent");
        Vertices_Parent = new GameObject("Vertices_Parent");
        Vertices = new List<GameObject>();

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

                yield return null;
                if (node == N_Vertices)
                    break;
            }

            if (node == N_Vertices)
                break;

            gameObject.transform.GetChild(0).gameObject.transform.position += new Vector3(
                2, 2, 0);
            gameObject.transform.rotation = Quaternion.AngleAxis(30f * j, Vector3.forward);
            yield return null;
        }

        gameObject.transform.GetChild(0).transform.localPosition = Graph_Start_Point;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i != N_Vertices; i++)
        {
            Adjagency_List[i] = new List<GameObject>();

            for (int j = 0; j != N_Vertices; j++)
            {

                if (AdjagencyMatrix[i, j] != 0)
                {

                    Adjagency_List[i].Add(Vertices[j]);

                    GameObject New_Edge = Instantiate(Edge, Vertices[i].transform);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(0, Vertices[i].transform.position);
                    New_Edge.GetComponent<LineRenderer>().SetPosition(1, Vertices[j].transform.position);
                    New_Edge.transform.GetChild(0).GetComponent<TextMeshPro>().text = AdjagencyMatrix[i, j].ToString();
                    if(AdjagencyMatrix[j,i] != 0)                    
                        if (i < j)
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(true);
                        else
                            New_Edge.GetComponent<EdgeCSS>().Set_Direction(false);  
                    //else
                        //set arrow direction

                    //null refrence exception don't know why 
                    //New_Edge.GetComponent<EdgeCSS>().Set_Weigth(AdjagencyMatrix[i, j]);
                    Vertices[i].GetComponent<_VertexCSS>().Edges.Add(New_Edge);
                    Vertices[i].GetComponent<_VertexCSS>().Connected_Vertices.Add(Vertices[j]);
                    // don't call a function 
                    //Vertices[i].GetComponent<_VertexCSS>().Connect_Edge(Vertices[j]); 
                    yield return null;
                }
            }
        }
    }

    IEnumerator DfsRecursive(int s)
    {
        Visited[s] = true;
        float Distance = 0;
        for (int i = 0; i < Adjagency_List[s].Count(); i++)
        {
            if (Visited[int.Parse(Adjagency_List[s][i].GetComponentInChildren<TextMeshPro>().text)] == false)
            {
                Distance = Vector3.Distance(MyCursor.transform.position, Adjagency_List[s][i].transform.position);
                StartCoroutine(MoveCursor(Adjagency_List[s][i].transform.position));
                yield return new WaitForSeconds(Distance / Speed);
                StartCoroutine(DfsRecursive(int.Parse(Adjagency_List[s][i].GetComponentInChildren<TextMeshPro>().text)));                
            }
        }
    }

    IEnumerator DFS(int Start)
    {
        // Initially mark all verices as not visited 
        float Distance;
        List<bool> Visited = new List<bool>();
        for (int i = 0; i < N_Vertices; i++)
        {
            Visited.Add(false);
        }

        // Create a stack for DFS 
        Stack<int> MyStack = new Stack<int>(); 
        // Push the current source node. 
        MyStack.Push(Start);

        while (MyStack.Count != 0)
        {
            // Pop a vertex from stack and print it 
            Start = MyStack.Peek();
            MyStack.Pop();

            // Stack may contain same vertex twice. So 
            // we need to print the popped item only 
            // if it is not visited. 
            
            if (!Visited[Start])
            {
                Visited[Start] = true;
            }

            // Get all adjacent vertices of the popped vertex s 
            // If a adjacent has not been visited, then puah it 
            // to the stack.           
            for (int i = 0; i < Adjagency_List[Start].Count; i++)
            {
                if (!Visited[i])
                {
                    MyStack.Push(i);
                    Distance = Vector3.Distance(MyCursor.transform.position, Adjagency_List[Start][i].transform.position);

                    StartCoroutine(MoveCursor(Adjagency_List[Start][i].transform.position));
                    yield return new WaitForSeconds(Distance / Speed);
                }
            }              
        }
    }
    
    IEnumerator MoveCursor(Vector3 Target)
    {
        while (MyCursor.transform.position != Target)
        {
            MyCursor.transform.position = Vector3.MoveTowards(MyCursor.transform.position, Target, Speed * Time.deltaTime);
            yield return null;
        }
    }

    public void StartDFS()
    {
        MyCursor.transform.position = Vertices[0].transform.position;
        StartCoroutine(DFS(0));
    }
}