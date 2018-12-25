using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;
using TMPro;

public class CanvesCSS : MonoBehaviour {

    public GameObject Pen;
    public Image Preview_Color; 
    private Color32 Pen_Color;
    private int Pen_Layer;
    private Vector3 screenPoint;
    private Vector3 offset;
    private List<GameObject> Pens;
    private Stack<GameObject> Undo;
    private Stack<GameObject> Redo;
    private bool _Undo;
    private bool _Redo;
    private float Red;
    private float Green;
    private float Blue;

    void Start () {
        Pen_Color = new Color32();
        Pen_Layer = 0;
        Pens = new List<GameObject>();
        Undo = new Stack<GameObject>();
        Redo = new Stack<GameObject>();
        Red = 0;
        Green = 0;
        Blue = 0;
    }
	
	void Update () {

       // Preview_Color.color = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)255);

        for (int t = 0; t != Pens.Count; t++)
        {
            Pens[t].GetComponent<TrailRenderer>().time++;
            //Pens[t].GetComponent<TrailRenderer>().material.color = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)0);
            //= new Color(Red, Green, Blue);     
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
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        
        GameObject New_Pen = Instantiate(Pen, curPosition, Quaternion.identity);
        New_Pen.transform.parent = gameObject.transform;
        New_Pen.GetComponent<TrailRenderer>().material.color = Pen_Color;
        New_Pen.GetComponent<TrailRenderer>().sortingOrder = Pen_Layer;
        Pen_Layer += 1;
        Pens.Add(New_Pen);

        Undo.Push(New_Pen);
    }

    void OnMouseDrag()
    {       
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        Pens[Pens.Count - 1].transform.position = curPosition;
    }

    public void Set_Erase()
    {
        Pen_Color = new Color32((byte)255, (byte)255, (byte)255, (byte)0);
        Pen_Layer += 1;
    }
    
    public void Set_Draw()
    {
        Pen_Color = new Color32((byte)0, (byte)0, (byte)0, (byte)0);
        Pen_Layer += 1;
    }

    public void Set_Background_Color()
    {
        Camera.main.backgroundColor = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)0);
    }

    public void Set_Red(Slider red)
    {
        Red = red.value;
        Pen_Color = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)0);
    }

    public void Set_Green(Slider green)
    {
        Green = green.value;
        Pen_Color = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)0);
    }

    public void Set_Blue(Slider blue)
    {
        Blue = blue.value;
        Pen_Color = new Color32((byte)Red, (byte)Green, (byte)Blue, (byte)0);
    }

    public void Undo_Last_Opertation()
    {
        if (Pens.Count == 0)
            return;

        Destroy(Pens[Pens.Count - 1]);
        Destroy(Undo.Peek());
        Pens.RemoveAt(Pens.Count - 1);
        Undo.Pop();

        //stupid solution 
        //Redo.Push(Undo.Peek());
        //Destroy(Pens[Pens.Count - 1]);
        //Destroy(Undo.Peek());
        //Pens.RemoveAt(Pens.Count - 1);
        //Undo.Pop();
    }

    public void Redo_Last_Opertation()
    {
        GameObject Old_Pen = Instantiate(Pen, Redo.Peek().transform.position, Quaternion.identity);
        Old_Pen = Redo.Peek();

        Pens.Add(Old_Pen);
        Redo.Pop();
    }

    public void Inverse_Colors(Button button)
    {
        button.GetComponent<MaterialButton>().icon.color = new Color32((byte)(255 - Red), (byte)(255 - Green), (byte)(255 - Blue), (byte)255);
    }
}