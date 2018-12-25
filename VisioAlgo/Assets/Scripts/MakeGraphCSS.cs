using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MakeGraphCSS : MonoBehaviour {

    public GameObject Vertex_Instantiate_Point;
    public GameObject Edge_Instantiate_Point;
    public GameObject Vertex;
    public GameObject Edge;
    private List<GameObject> Vertices;
    private List<GameObject> Edges;
    private bool Instantiate_Vertex;
    private bool Instantiate_Edge;
    private bool Remove_Vertex;
    private bool First_Connected;
    private bool Second_Connected;
    private int Current_Vertex;

    void Start () {
        Vertices = new List<GameObject>();
        Edges = new List<GameObject>();
        Instantiate_Vertex = false;
        Instantiate_Edge = false;
        Remove_Vertex = false;
        First_Connected = false;
        Second_Connected = false;
    }
	
	void Update () {
        if((Instantiate_Edge || First_Connected || Second_Connected) && Edges.Count > 0)
        {
            Edges[Edges.Count - 1].transform.position = Edge_Instantiate_Point.transform.position;
            Edges[Edges.Count - 1].GetComponent<LineRenderer>().SetPosition(1, Edge_Instantiate_Point.transform.position);
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

    void OnMouseDown()
    {
        if(Instantiate_Vertex)
        {
            GameObject obj = Instantiate(Vertex, Vertex_Instantiate_Point.transform.position, Quaternion.identity);
            obj.name = "Vetex" + Vertices.Count.ToString();
            obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = Vertices.Count.ToString();
            Vertices.Add(obj);
            Instantiate_Vertex = false;
            Vertex_Instantiate_Point.SetActive(false);
        }else
        if (Instantiate_Edge)
        {
            if (First_Connected)
            {
                for (int i = 0; i != Vertices.Count; i++)
                {
                    if (Vertices[i].transform.position.x + Vertex.transform.localScale.x / 2 >= Edge_Instantiate_Point.transform.position.x
                        && Vertices[i].transform.position.x - Vertex.transform.localScale.x / 2 <= Edge_Instantiate_Point.transform.position.x
                        && Vertices[i].transform.position.y + Vertex.transform.localScale.y / 2 >= Edge_Instantiate_Point.transform.position.y
                        && Vertices[i].transform.position.y - Vertex.transform.localScale.y / 2 <= Edge_Instantiate_Point.transform.position.y)
                    {
                        GameObject obj = Instantiate(Edge, Vertices[i].transform);
                        obj.transform.position = Vertices[i].transform.position;
                        obj.GetComponent<LineRenderer>().SetPosition(0, Vertices[i].transform.position);
                        obj.GetComponent<LineRenderer>().SetPosition(1, Vertices[i].transform.position);
                        obj.name = "Edge" + Edges.Count.ToString();
                        Current_Vertex = i;
                        First_Connected = false;
                        Second_Connected = true;
                        Edges.Add(obj);
                        Vertices[i].GetComponent<_VertexCSS>().Edges.Add(obj);
                        break;
                    }
                }
            }
            else
            if (Second_Connected)
            {
                for (int i = 0; i != Vertices.Count; i++)
                {
                    if (   Vertices[i].transform.position.x + Vertex.transform.localScale.x / 2 >= Edge_Instantiate_Point.transform.position.x
                        && Vertices[i].transform.position.x - Vertex.transform.localScale.x / 2 <= Edge_Instantiate_Point.transform.position.x
                        && Vertices[i].transform.position.y + Vertex.transform.localScale.y / 2 >= Edge_Instantiate_Point.transform.position.y
                        && Vertices[i].transform.position.y - Vertex.transform.localScale.y / 2 <= Edge_Instantiate_Point.transform.position.y)
                    {
                        if (i == Current_Vertex)
                            break;

                        Vertices[Current_Vertex].GetComponent<_VertexCSS>().Connected_Vertices.Add(Vertices[i]);
                        Second_Connected = false;
                        for (int j = 0; j != Vertices.Count; j++)
                        {
                            Vertices[j].GetComponent<CircleCollider2D>().enabled = true;
                        }
                        Edge_Instantiate_Point.SetActive(false);
                        Instantiate_Edge = false;
                        break;
                    }
                }
            }                  
        }
        else       
        if (Remove_Vertex)
        {


            Remove_Vertex = false;
        }
    }

    public void Set_Instantiate_Vertex(bool instantiate_vertex)
    {
        if (Instantiate_Edge)
            return;

        Instantiate_Vertex = instantiate_vertex;
        Vertex_Instantiate_Point.SetActive(instantiate_vertex);
    }

    public void Set_Instantiate_Edge(bool instantiate_edge)
    {
        if (Instantiate_Vertex || Vertices.Count < 2)
            return;

        Instantiate_Edge = instantiate_edge;
        Edge_Instantiate_Point.SetActive(instantiate_edge);

        First_Connected = true;

        for(int i = 0; i != Vertices.Count; i++)
        {
            Vertices[i].GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}