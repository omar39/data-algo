using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _VertexCSS : MonoBehaviour {

    public List<GameObject> Edges;
    public List<GameObject> Connected_Vertices;
    private GameObject Edge;
    private Vector3 screenPoint;
    private Vector3 offset;

    void Start () {
        //we must do this don't know why though
        //Edges = new List<GameObject>();
        //Connected_Vertices = new List<GameObject>();
    }
	
	void Update () {        
        for(int i = 0; i != Edges.Count; i++)
        {
            Edges[i].GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);
            Edges[i].GetComponent<LineRenderer>().SetPosition(1, Connected_Vertices[i].transform.position);
        }              
	}

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));       
    }

    void OnMouseDrag()
    {        
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    public void Set_Edge(GameObject edge)
    {
        Edge = edge;
    }

    public void Connect_Edge(GameObject vertex)
    {
        GameObject New_Edge = Instantiate(Edge, gameObject.transform);
        New_Edge.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);
        New_Edge.GetComponent<LineRenderer>().SetPosition(1, vertex.transform.position);
        Edges.Add(New_Edge);
    }
}