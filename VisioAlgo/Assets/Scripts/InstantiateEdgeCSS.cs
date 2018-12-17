using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEdgeCSS : MonoBehaviour {

    void Start()
    {

    }

    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        gameObject.transform.position = curPosition;
    }   
}