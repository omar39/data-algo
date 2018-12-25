using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrailCSS : MonoBehaviour {

    private TrailRenderer Trail;
    private List<GameObject> Cell_Numbers;
    private int Array_Width;
    private int Array_Height;
    private int Cell_Width;
    private int Number_Of_Cells;
    public Material Trail_Matrial;
    public GameObject Start_Position;
    public GameObject Cell_Number;
    public int Cell_Height;
    public float Trail_Speed;
    public float Cell_Number_size;

    void Start () {
        gameObject.transform.position = Start_Position.transform.position;
        gameObject.AddComponent<TrailRenderer>();
        Trail = gameObject.GetComponent<TrailRenderer>();
        Trail.material = Trail_Matrial;
        Trail.numCornerVertices = 90;
        Trail.numCapVertices = 90;
        Trail.widthMultiplier = .1f;
        Cell_Numbers = new List<GameObject>();
        Array_Width = -(int)Start_Position.transform.position.x * 2;
        Cell_Width = Cell_Height;
        Array_Height = Cell_Height;
        Number_Of_Cells = Array_Width / Cell_Width;
        StartCoroutine(Draw_Array());
    }
	
	void Update () {
        Trail.time++;
	}

    //public void Draw_Array_Now()
    //{
    //    for (int cell = 0; cell != Number_Of_Cells; cell++)
    //    {
    //        StartCoroutine(Draw_Cell(Start_Position.transform.position + new Vector3(Cell_Width * cell, 0, 0)));
    //    }
    //}

    public int Get_Number_Of_Cells()
    {
        return Number_Of_Cells;
    }

    public Vector3 Get_Cell_Position(int index)
    {
        return (Cell_Numbers[index].transform.position);
    }

    public IEnumerator Move_Circle(GameObject Circle, int Index, float Circle_Speed)
    {
        while(Circle.transform.position != Cell_Numbers[Index].transform.position)
        {
            Circle.transform.position = Vector3.MoveTowards(Circle.transform.position, Cell_Numbers[Index].transform.position, Circle_Speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Draw_Array()
    {       
        for(int cell = 0; cell != Number_Of_Cells; cell++)
        {
            //StartCoroutine(Draw_Cell(gameObject.transform.position));                        
            yield return StartCoroutine(Draw_Cell(Start_Position.transform.position + new Vector3(Cell_Width * cell, 0, 0)));

            GameObject New_Node = Instantiate(Cell_Number, Start_Position.transform.position + new Vector3(Cell_Width * (cell + 1) - (Cell_Width / 2), -Cell_Height, 0), Quaternion.identity);

            New_Node.transform.parent = Start_Position.transform;

            New_Node.GetComponent<ValueCSS>().Set_Value_Size(Cell_Number_size);
            New_Node.GetComponent<ValueCSS>().Set_Value(cell.ToString());
            New_Node.GetComponent<ValueCSS>().Set_Node_Speed(0);

            Cell_Numbers.Add(New_Node);
        }
    }

    public IEnumerator Draw_Cell(Vector3 Cell_Position)
    {
        yield return StartCoroutine(Draw_Edge(Cell_Position + new Vector3(Cell_Width, 0, 0))); // down edge from left to right
        yield return StartCoroutine(Draw_Edge(Cell_Position + new Vector3(Cell_Width, Cell_Height, 0))); // right edge from down to up
        //yield return StartCoroutine(Draw_Edge(Cell_Position + new Vector3(Cell_Width, 0, 0))); // right edge from up to down
        yield return StartCoroutine(Draw_Edge(Cell_Position + new Vector3(0, Cell_Height, 0))); // up edge from right to left 
        yield return StartCoroutine(Draw_Edge(Cell_Position)); // left edge from up to down
        yield return StartCoroutine(Draw_Edge(Cell_Position + new Vector3(Cell_Width, 0, 0))); // down edge from left to right
    }

    public IEnumerator Draw_Edge(Vector3 End_Position)
    {
        while(gameObject.transform.position != End_Position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, End_Position, Trail_Speed * Time.deltaTime);
            yield return null;
        }
    }
}