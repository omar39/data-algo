using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueCSS : MonoBehaviour {
  
    private float Node_Speed;
    private TextMeshPro Node_Value;

    void Awake()
    {
        //gameObject.AddComponent<TextMeshPro>();
        Node_Value = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
           
    }

    public void Set_Value(string Value)
    {
        Node_Value.text = Value;
    }

    public void Set_Value_Size(float Size)
    {
        Node_Value.fontSize = Size;
    }

    public void Set_Node_Speed(float Speed)
    {
        Node_Speed = Speed;
    }

    public IEnumerator Move_Value(Vector3 Cell_Position)
    {
        // using corutines the node is moved to the desired position 
        while (gameObject.transform.position != Cell_Position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Cell_Position, Node_Speed * Time.deltaTime);
            yield return null;
        }
    }
}