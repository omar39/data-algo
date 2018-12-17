using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphCSS : MonoBehaviour {

    public GameObject Vertex;
    public GameObject Edge;
    public GameObject Start_Point;
    public GameObject obj;
    public float Horizontal_Offset;
    public int Number_Of_Vertices;
    public int Number_Of_Sides;
    private List<GameObject> Vertices;
    private int [,] Matrix;
    private GameObject Instantiate_Point;
    private int Levels;
    private int Last_Level;

    bool s;
    int i;
    int j;

    //void Start () {
    //       Vertices = new List<GameObject>();
    //       Matrix = new int[4, 4] { { 0, 1, 1, 1 },
    //                                { 1, 0, 1, 1 },
    //                                { 1, 1, 0, 1 },
    //                                { 1, 1, 1, 0 }};
    //       //StartCoroutine(Draw_Graph());
    //       Draw_Graph();
    //   }

    //   void Update () {

    //   }

    //   public void Set_Number_Of_Vertices(int number_of_vertices)
    //   {
    //       Number_Of_Vertices = number_of_vertices;
    //       Matrix = new int[3, 3] { { 0, 1, 1 },
    //                                { 1, 0, 1 },
    //                                { 1, 1, 0 } };
    //   }

    //   public void Set_Vertex(int i, int j, int vertex_number)
    //   {
    //       GameObject New_Vertex = Instantiate(Vertex,
    //                 Start_Point.transform.position + new Vector3(Offset * j, - Offset * i, 0),
    //                 Quaternion.identity);
    //       New_Vertex.GetComponent<VertexCSS>().Set_Vertex_Number((vertex_number).ToString());
    //       New_Vertex.GetComponent<VertexCSS>().Set_Number_Of_Edges(Number_Of_Vertices);
    //       New_Vertex.GetComponent<VertexCSS>().Set_Edge(Edge);
    //       //for (int k = 0; k != Number_Of_Vertices * 2; k++)
    //       //    New_Vertex.GetComponent<VertexCSS>().Add_Edge(New_Vertex);
    //       StartCoroutine(New_Vertex.GetComponent<VertexCSS>().Draw_Circle());
    //      // yield return null;
    //       Vertices.Add(New_Vertex);
    //   }

    //   public void Set_Vertices()
    //   {
    //       int Vertex_Number = 0;
    //       for (int i = 1; i <= Number_Of_Vertices/2; i++)
    //       {
    //           for (int j = 1; j <= Number_Of_Vertices/2; j++)
    //           {
    //               //yield return StartCoroutine(Set_Vertex(i, j, Vertex_Number));
    //               Set_Vertex(i, j, Vertex_Number);
    //               Vertex_Number++;                
    //           }
    //       }
    //   }

    //   public void Set_Edges()
    //   {
    //       for(int i = 0; i != Number_Of_Vertices; i++)
    //       {
    //           for (int j = 0; j != Number_Of_Vertices; j++)
    //           {
    //               if(Matrix[i,j] == 1)
    //               {
    //                   Vertices[i].GetComponent<VertexCSS>().Add_Edge(Vertices[j]);
    //                   //yield return null;
    //               }
    //           }
    //       }
    //   }

    //   public void Draw_Graph()
    //   {
    //       //yield return StartCoroutine(Set_Vertices());
    //       Set_Vertices();
    //       //yield return StartCoroutine(Set_Edges());
    //       Set_Edges();
    //   }

    void Start()
    {
        Application.targetFrameRate = 300;
        Levels = Number_Of_Vertices / Number_Of_Sides;
        Last_Level = Number_Of_Vertices - Levels;
        Instantiate_Point = Start_Point.transform.GetChild(0).gameObject;

        //for (int i = 1; i <= Levels; i++)
        //{
        //    for (int j = 0; j != Number_Of_Sides; j++)
        //    {
        //        Start_Point.transform.rotation = Quaternion.AngleAxis((360 / Number_Of_Sides) * j, Vector3.forward);
        //        GameObject New_Vertex = Instantiate(obj, Instantiate_Point.transform.position, Quaternion.identity);
        //        Vertices.Add(New_Vertex);
        //    }
        //    Start_Point.transform.rotation = Quaternion.AngleAxis((360 / Number_Of_Sides) * i, Vector3.forward);
        //}


        s = false;
        i = 1;
        j = 0;
    }

    void Update()
    {
        j++;

        print(j);

        //if (s == false)
        //{
        //    Start_Point.transform.rotation = Quaternion.AngleAxis((360 / Number_Of_Sides) * j, Vector3.forward);
        //    GameObject New_Vertex = Instantiate(obj, Instantiate_Point.transform.position, Quaternion.identity);
        //    Vertices.Add(New_Vertex);
        //    j++;

        //    if (j == Number_Of_Sides)
        //    {
        //        Start_Point.transform.rotation = Quaternion.AngleAxis((360 / Number_Of_Sides) * i, Vector3.forward);
        //        //Instantiate_Point.transform.position += new Vector3(Horizontal_Offset, Instantiate_Point.transform.position.y, 0);
        //        j = 0;
        //        i++;
        //        if (i > Levels)
        //        {
        //            s = true;
        //        }
        //    }

        //    print(s);
        //    print(i);
        //    print(j);
        //}
    }

    //public void OnMouseDown()
    //{
    //    StartCoroutine(Make_N_Sided_Shape(Number_Of_Sides));
    //}

    //void Draw_Graph(int sides)
    //{
    //    StartCoroutine(Make_Levels(sides));
    //}

    //IEnumerator Make_Levels(int sides)
    //{
    //    for (int i = 0; i != 3; i++)
    //    {
    //        print("y1");
    //        yield return StartCoroutine(Make_Level(sides));
    //        yield return StartCoroutine(Rotate(Instantiate_Point, Start_Point, sides));
    //    }
    //}

    //IEnumerator Make_Level(int sides)
    //{
    //    for (int j = 0; j != 8; j++)
    //    {
    //        print("y2");
    //        StartCoroutine(Rotate(Instantiate_Point, Start_Point, sides));
    //        yield return null;
    //        GameObject New_Vertex = Instantiate(@object, Instantiate_Point.transform.position, Quaternion.identity);
    //        Vertices.Add(New_Vertex);
    //    }
    //}

    //public IEnumerator Rotate(GameObject rotator, GameObject rotation_point, int sides)
    //{
    //    // 50 is a random number that i found that it does the trick DKY
    //    //rotator.transform.RotateAround(rotation_point.transform.position, Vector3.forward, 50 * (360 / sides) * Time.deltaTime);

    //    print("y3");

    //    Vector3 Rotation = rotator.transform.rotation.eulerAngles;
    //    Rotation += new Vector3(0, 0, (360 / sides));

    //    while (rotator.transform.rotation.eulerAngles.z < Rotation.z)
    //    {
    //        // Time.fixedDeltaTime better that Time.deltaTime
    //        rotator.transform.RotateAround(rotation_point.transform.position, Vector3.forward, (360 / sides) * Time.smoothDeltaTime);
    //        yield return null;
    //    }
    //}

    //public void Add_Offset(GameObject obj, float horizontal_offset)
    //{
    //    print("y4");
    //    obj.transform.position += new Vector3(horizontal_offset, obj.transform.position.y, 0);
    //}

    //public IEnumerator Make_N_Sided_Shape(int sides)
    //{
    //    for (int j = 0; j != Levels; j++)
    //    {
    //        for (int i = 0; i != sides; i++)
    //        {
    //            Instantiate_Point.transform.RotateAround(Start_Point.transform.position, Vector3.forward, 50 * (360 / sides) * Time.smoothDeltaTime);
    //            GameObject New_Vertex = Instantiate(@object, Instantiate_Point.transform.position, Quaternion.identity);
    //            Vertices.Add(New_Vertex);
    //        }
    //        Instantiate_Point.transform.RotateAround(Start_Point.transform.position, Vector3.forward, 50 * (360 / sides) * Time.smoothDeltaTime);
    //        Instantiate_Point.transform.position += new Vector3(Horizontal_Offset, Instantiate_Point.transform.position.y, 0);
    //    }

    //    yield return null;
    //}

    //public IEnumerator MoveCursor(GameObject MyCursor, List<Vector3> Positions, int Index)
    //{
    //    print("Moveing Towards :" + Positions[Index]);
    //    while (MyCursor.transform.position != Positions[Index])
    //    {
    //        MyCursor.transform.position = Vector3.MoveTowards(MyCursor.transform.position, Positions[Index], Speed * Time.deltaTime);
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(0.7f);
    //    if (Index < Positions.Count - 1)
    //    {
    //        Debug.Log("Next postion no :" + Index);
    //        Index++;
    //        StartCoroutine(MoveCursor(MyCursor, Positions, Index));
    //    }
    //    else
    //    {
    //        print("Reset");
    //        MyCursor.transform.position = new Vector3(-7, -1, 0);
    //        StartCoroutine(MoveNode(NewNode, Target));
    //        for (int i = 0; i < ToShift.Count; i++)
    //        {
    //            StartCoroutine(MoveNode(ToShift[i].Key, ToShift[i].Value));
    //        }
    //        Index = 0;
    //    }

    //}
}