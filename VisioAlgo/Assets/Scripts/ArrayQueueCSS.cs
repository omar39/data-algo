using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrayQueueCSS : MonoBehaviour {

    public GameObject Instantiate_Point;
    public GameObject Start_Position;
    public GameObject Value;
    public GameObject Front;
    public GameObject Back;
    public GameObject Trail;
    private float Speed;
    private float Size;
    private List<GameObject> Cells_Values;
    private int front;
    private int back;
    private int Elements;
    private int size;

    void Start()
    {
        Cells_Values = new List<GameObject>();
        Speed = 10;
        Elements = 0;
        front = back = -1;
        size = Trail.GetComponent<TrailCSS>().Get_Number_Of_Cells();
    }

    void Update()
    {

    }

    public void Set_Animation_Speed(Slider speed)
    {
        Speed = speed.value;
    }

    public void Enqueue(Text value)
    {
        if (Elements == Trail.GetComponent<TrailCSS>().Get_Number_Of_Cells())
            return;

        if (Elements == 0)
        {
            Front.transform.GetChild(0).GetComponent<TextMeshPro>().text = "0";
            front = 0;
        }

        back = (back + 1) % size;
        Back.transform.GetChild(0).GetComponent<TextMeshPro>().text = back.ToString();

        Elements++;

        GameObject New_Value = Instantiate(Value, Instantiate_Point.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        //New_Value.transform.parent = gameObject.transform;
        New_Value.GetComponent<TextMeshPro>().text = value.text;
        New_Value.GetComponent<ValueCSS>().Set_Node_Speed(Speed);
        Cells_Values.Add(New_Value);
        _start(New_Value);
    }

    void _start(GameObject New_Value)
    {
        StartCoroutine(move(New_Value));
    }

    public IEnumerator move(GameObject New_Value)
    {
        yield return StartCoroutine(New_Value.GetComponent<ValueCSS>().Move_Value(
            Trail.GetComponent<TrailCSS>().Get_Cell_Position(back) + new Vector3(0, 3, 0)));
    }

    public void Dequeue()
    {
        if (Elements == 0)
            return;

        if (Elements == 1)
            Front.transform.GetChild(0).GetComponent<TextMeshPro>().text = Back.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-1";
        else
            Front.transform.GetChild(0).GetComponent<TextMeshPro>().text = (front = (front + 1) % size).ToString();

        Elements--;

        Destroy(Cells_Values[0]);
        Cells_Values.RemoveAt(0);
    }
}