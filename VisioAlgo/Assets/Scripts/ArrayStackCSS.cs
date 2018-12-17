using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrayStackCSS : MonoBehaviour {

    public GameObject Instantiate_Point;
    public GameObject Start_Position;
    public GameObject Top;
    public GameObject Value;
    public GameObject Trail;
    public float Value_Speed;
    float Value_Size;
    Stack<GameObject> Cells_Objects;
    Button Push_Button;
    float Offset;

    GameObject value;

	void Start () {
        Cells_Objects = new Stack<GameObject>();
        Offset = Trail.GetComponent<TrailCSS>().Cell_Height / 2;
        Top.transform.GetChild(1).gameObject.SetActive(false);
        Top.transform.GetChild(1).position = Top.transform.position;

        value = Top.transform.GetChild(0).gameObject;

        value.SetActive(false);
    }

    void Update () {
		
	}

    public void Push_Value(Text Pushed_Value)
    {
        if (Pushed_Value.text == "" || Cells_Objects.Count == Trail.GetComponent<TrailCSS>().Get_Number_Of_Cells())
            return;

        Push_Button.enabled = false;

        GameObject New_Node = Instantiate(Value, Instantiate_Point.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        New_Node.GetComponent<ValueCSS>().Set_Value(Pushed_Value.text);
        New_Node.GetComponent<ValueCSS>().Set_Value_Size(Trail.GetComponent<TrailCSS>().Cell_Number_size);
        New_Node.GetComponent<ValueCSS>().Set_Node_Speed(Value_Speed);

        Cells_Objects.Push(New_Node);

        Top.transform.GetChild(1).gameObject.SetActive(true);
        Push_Sequence(New_Node);
    }

    public void Set_Button_State(Button push_button)
    {
        Push_Button = push_button;
    }

    public void Set_Animation_Speed(Slider Speed)
    {
        Value_Speed = Speed.value;
    }

    private void Push_Sequence(GameObject New_Node)
    {
        StartCoroutine(Push_Seq(New_Node));
    }

    private IEnumerator Push_Seq(GameObject New_Node)
    {


        yield return StartCoroutine(Move_Circle(Top.transform.GetChild(1)));
        Set_Top_Value();
        yield return StartCoroutine(Move_Value(New_Node));

        Top.transform.GetChild(1).gameObject.SetActive(false);
        Top.transform.GetChild(1).position = Top.transform.position;
        Push_Button.enabled = true;
    }

    private IEnumerator Move_Circle(Transform Circle)
    {
        yield return Trail.GetComponent<TrailCSS>().Move_Circle(Circle.gameObject, Cells_Objects.Count - 1, Value_Speed);
    }

    private void Set_Top_Value()
    {     
        Top.transform.GetChild(0).GetComponent<TextMeshPro>().text = Cells_Objects.Count.ToString();
    }

    private IEnumerator Move_Value(GameObject New_Node)
    {
        yield return New_Node.GetComponent<ValueCSS>().Move_Value(
            Start_Position.transform.position +
            new Vector3(Trail.GetComponent<TrailCSS>().Cell_Height * (Cells_Objects.Count) - (Trail.GetComponent<TrailCSS>().Cell_Height / 2),
            Offset, 0));
    }

    public void Pop_Value(Button Pop_Button)
    {
        if (Cells_Objects.Count == 0)
            return;

        Pop_Button.enabled = false;
        Destroy(Cells_Objects.Peek());
        Cells_Objects.Pop();
        Set_Top_Value();
        Pop_Button.enabled = true;
    }
}