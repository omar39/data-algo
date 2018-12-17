using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VertexCSS : MonoBehaviour {

    private float Rotation_Speed;
    private float Radius;
    private int Cycle;
    private Transform Rotate_Point;
    private GameObject Circle;
    private GameObject Vertex_Number;
    private GameObject Edge;
    private List<GameObject> Edges;
    private int Number_Of_Edges;

    void Awake() {
        Rotation_Speed = 50;
        Radius = 2;
        Cycle = 500;
        Rotate_Point = gameObject.transform;
        Circle = gameObject.transform.GetChild(0).gameObject;
        Vertex_Number = gameObject.transform.GetChild(1).gameObject;
        Vertex_Number.transform.position = gameObject.transform.position + new Vector3(0, -.75f, 0);
        Edges = new List<GameObject>();
    }

    void Update() {
        Circle.GetComponent<TrailRenderer>().time++;
    }

    public void Set_Vertex_Number(string vertex_number)
    {
        Vertex_Number.GetComponent<TextMeshPro>().text = vertex_number;
    }

    public void Set_Number_Of_Edges(int number_of_edges)
    {
        Number_Of_Edges = number_of_edges;
    }

    public void Set_Edge(GameObject edge)
    {
        Edge = edge;
    }

    public IEnumerator Draw_Circle()
    {
        for(int i = 0; i != Cycle; i++)
        {
            Circle.transform.RotateAround(Rotate_Point.position, Vector3.forward, Time.deltaTime * Rotation_Speed);
            yield return null;
        }
    }

    public void Add_Edge(GameObject vertex)
    {
        GameObject New_Edge = Instantiate(Edge, gameObject.transform);
        New_Edge.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);
        New_Edge.GetComponent<LineRenderer>().SetPosition(1, vertex.transform.position);
        Edges.Add(New_Edge);
    }

    public void Connect_Edge_Line(GameObject edge, GameObject vertex)
    {
        edge.GetComponent<LineRenderer>().SetPosition(1, vertex.transform.position);
    }

    public void Connect_Edge_Curve(int index, GameObject vertex, Vector3 mid)
    {
        DrawQuadraticBezierCurve(index, 50, gameObject.transform.position, mid, vertex.transform.position);
    }

    Vector3 calcQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    void DrawQuadraticBezierCurve(int index,int points, Vector3 point0, Vector3 point1, Vector3 point2)
    {
        Vector3 []positions = new Vector3[points];
        for (int i = 1; i <= points; i++)
        {
            float t = i / (float)points;
            positions[i - 1] = calcQuadraticBezierPoint(t, point0, point1, point2);
        }
        Edges[index].GetComponent<LineRenderer>().SetPositions(positions);        
    }

    //public IEnumerator _Connect_Edge(GameObject edge, GameObject vertex)
    //{
    //    while(Edge.transform.position != vertex.transform.position)
    //    {
    //        Edge.transform.position = Vector3.MoveTowards(Edge.transform.position, vertex.transform.position, 10);
    //        yield return null;
    //    }
    //}
}