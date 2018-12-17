using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinkedStackCSS : MonoBehaviour
{
    public GameObject Node;
    public GameObject Instantiate_Point;
    public GameObject Start_Position;
    public float Node_Speed;
    public float Horizontal_Offset;
    public float Vertical_Offset;
    List<GameObject> Nodes_Objects;
    List<Vector3> Node_Positions;
    GameObject Head;
    bool Hints;
   
    void Start()
    {
        Nodes_Objects = new List<GameObject>();
        Node_Positions = new List<Vector3>();
        Head = GameObject.Find("HeadNode");
        Head.GetComponent<HeadNodeCSS>().Set_Node_Speed(Node_Speed);
        Head.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Set_Hints(Toggle Switch)
    {
        Hints = Switch.isOn;
    }

    public void Push_Value(Text Pushed_Value)
    {
        if (Pushed_Value.text == "")
            return;
        
        // instatiate a new node and give it the entered value
        //GameObject NewNode = Instantiate(Node, InstantiatePoint);  //instatates the gameobject as a child of the instatiatepoint game object may cause problomes 
        GameObject New_Node = Instantiate(Node, Instantiate_Point.transform.position + new Vector3(1, 0, 0), Quaternion.identity);  //solution
        New_Node.GetComponent<NodeCSS>().Set_Node_Value(Pushed_Value.text);
        New_Node.GetComponent<NodeCSS>().Set_Node_Speed(Node_Speed);

        // set the first distnation point for the first node only if the stack is empty 
        if (Nodes_Objects.Count == 0)
        {
            Node_Positions.Add(Start_Position.transform.position);
            Head.SetActive(true);
        }

        // a new desired position is specified and add to the stack of positions and the new node is added to the stack of objects
        Node_Positions.Add(Node_Positions[Node_Positions.Count - 1] + new Vector3(Horizontal_Offset, Vertical_Offset, 0));
        Nodes_Objects.Add(New_Node);

        // start  the push sequence of operations
        Push_Sequence(New_Node);
    }

    private void Push_Sequence(GameObject New_Node)
    {
        StartCoroutine(Push_Seq(New_Node));
    }

    private IEnumerator Push_Seq(GameObject New_Node)
    {
        yield return StartCoroutine(Move_Node(New_Node));
        yield return StartCoroutine(Connect_Temp_Pointer(New_Node));
        yield return StartCoroutine(Move_Head());
        yield return StartCoroutine(Connect_Head_Pointer());

        New_Node.GetComponent<NodeCSS>().Set_Interact(true);
        if(Nodes_Objects.Count > 1)
            New_Node.GetComponent<NodeCSS>().Set_Next_pointer_Position(Nodes_Objects[Nodes_Objects.Count - 2]);
        else
            New_Node.GetComponent<NodeCSS>().Set_Next_pointer_Position(Start_Position);

        Head.GetComponent<HeadNodeCSS>().Set_Interact(true);
        Head.GetComponent<HeadNodeCSS>().Set_Next_pointer_Position(Nodes_Objects[Nodes_Objects.Count - 1]);
    }

    private IEnumerator Move_Node(GameObject New_Node)
    {
        yield return New_Node.GetComponent<NodeCSS>().Move_Node(Node_Positions[Node_Positions.Count - 1]);
    }

    private IEnumerator Connect_Temp_Pointer(GameObject New_Node)
    {
        //yield return New_Node.GetComponent<NodeCSS>().Connect_Next_Pointer(Nodes_Objects.Peek().transform.position);
        //yield return New_Node.GetComponent<NodeCSS>().Connect_Next_Pointer(
        //Node_Positions[Node_Positions.Count - 1] - new Vector3(Horizontal_Offset, Vertical_Offset, 0)); // works
        yield return New_Node.GetComponent<NodeCSS>().Connect_Next_Pointer(Node_Positions[Node_Positions.Count - 2]); // works but smarter !
    }

    private IEnumerator Move_Head()
    {
        //yield return Head.GetComponent<HeadNodeCSS>().Move_Head(Nodes_Objects.Peek().transform.position, Horizontal_Offset, Vertical_Offset);
        yield return Head.GetComponent<HeadNodeCSS>().Move_Head(Node_Positions[Node_Positions.Count - 1], Horizontal_Offset, Vertical_Offset);
    }

    private IEnumerator Connect_Head_Pointer()
    {
        yield return Head.GetComponent<HeadNodeCSS>().Connect_Next_Pointer(Nodes_Objects[Nodes_Objects.Count - 1].transform.position);
    }

    public void Pop_Value(Button Pop_Button)
    {
        if (Nodes_Objects.Count == 0)
            return;

        Pop_Button.enabled = false;

        Destroy(Nodes_Objects[Nodes_Objects.Count - 1]);
        Nodes_Objects.RemoveAt(Nodes_Objects.Count - 1);
        Node_Positions.RemoveAt(Node_Positions.Count - 1);

        Head.GetComponent<HeadNodeCSS>().Set_Next_pointer_Position(Nodes_Objects[Nodes_Objects.Count - 1]);

        Pop_Sequence(Pop_Button);
    }

    private void Pop_Sequence(Button Pop_Button)
    {
        StartCoroutine(Pop_Seq(Pop_Button));
    }

    private IEnumerator Pop_Seq(Button Pop_Button)
    {              
        yield return StartCoroutine(Connect_Head_Pointer());
        yield return StartCoroutine(Move_Head());

        // must be excuted after the corutines have finished 
        if (Nodes_Objects.Count == 0)
        {
            Head.transform.position = new Vector3(-6f, 4, 0);
            Head.GetComponent<LineRenderer>().SetPosition(0, Head.transform.position);
            Head.GetComponent<LineRenderer>().SetPosition(1, Head.transform.position);
            Head.SetActive(false);
        }

        Pop_Button.enabled = true;
    }
}