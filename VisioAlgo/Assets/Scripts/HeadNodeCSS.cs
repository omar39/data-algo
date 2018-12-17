using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeadNodeCSS : MonoBehaviour {

    private Transform Next_Pointer;
    private float Node_Speed;
    private GameObject Next_Node;
    private Vector3 screenPoint; 
    private Vector3 offset;
    private bool Interact;

    void Awake()
    {
        Next_Pointer = gameObject.transform.GetChild(0);
        Interact = false;
    }

    void Update()
    {
        if (!Interact)
            return;

        gameObject.GetComponent<LineRenderer>().SetPosition(
            0,
            gameObject.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(
            gameObject.GetComponent<LineRenderer>().positionCount - 1,
            Next_Node.transform.position);
    }

    void OnMouseDown()
    {
        if (!Interact)
            return;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!Interact)
            return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    public void Set_Interact(bool interact)
    {
        Interact = interact;
    }

    public void Set_Node_Speed(float Speed)
    {
        Node_Speed = Speed;
    }

    public void Set_Next_pointer_Position(GameObject next_Node)
    {
        Next_Node = next_Node;
    }

    public IEnumerator Move_Head(Vector3 Next_Position, float Horizontal_Offset ,float Vertical_Offset)
    {
        //while (gameObject.transform.position - Next_Position != new Vector3(Horizontal_Offset, Vertical_Offset, 0)) 
        while (gameObject.transform.position != Next_Position + new Vector3(Horizontal_Offset, Vertical_Offset, 0))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                Next_Position + new Vector3(Horizontal_Offset, Vertical_Offset, 0), Node_Speed * Time.deltaTime);
            gameObject.GetComponent<LineRenderer>().SetPosition(
                0,
                gameObject.transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(
                gameObject.GetComponent<LineRenderer>().positionCount - 1,
                Next_Position);
            yield return null;
        }
    }

    public IEnumerator Connect_Next_Pointer(Vector3 Next_Position)
    {
        while (Next_Pointer.transform.position != Next_Position)
        {
            Next_Pointer.transform.position = Vector3.MoveTowards(Next_Pointer.transform.position, Next_Position, Node_Speed * Time.deltaTime);
            gameObject.GetComponent<LineRenderer>().SetPosition(
                0,
                gameObject.transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(
                gameObject.GetComponent<LineRenderer>().positionCount - 1,
                Next_Position);
            yield return null;
        }
    }
}