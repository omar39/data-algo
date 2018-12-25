using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class EdgeCSS : MonoBehaviour {

    private GameObject Weight;
    private Vector3 point0, point1, point2, offset;
    private Vector3[] positions;
    private int points;
    private bool direction;

    void Start () {
        points = 50;
        positions = new Vector3[points];
        Weight = gameObject.transform.GetChild(0).gameObject;
    }
	
	void Update () {
        point0 = gameObject.GetComponent<LineRenderer>().GetPosition(0);
        point2 = gameObject.GetComponent<LineRenderer>().GetPosition(1);

        if (direction)
            offset = new Vector3(-1, -1, 0);
        else
            offset = new Vector3(1, 1, 0);

        point1 = ((point0 + point2) / 2) + offset;
        Weight.transform.position =
            ((gameObject.GetComponent<LineRenderer>().GetPosition(0) + gameObject.GetComponent<LineRenderer>().GetPosition(1)) / 2) + offset;

        DrawQuadraticBezierCurve();
    }

    public void Set_Direction(bool die)
    {
        direction = die;
    }

    public void Set_Weigth(int weight)
    {
        Weight.GetComponent<TextMeshPro>().text = Weight.ToString();           
    }

    void DrawQuadraticBezierCurve()
    {      
        for (int i = 1; i <= points; i++)
        {
            float t = i / (float)points;
            positions[i - 1] = calcQuadraticBezierPoint(t, point0, point1, point2);
        }
        gameObject.GetComponent<LineRenderer>().positionCount = points;
        gameObject.GetComponent<LineRenderer>().SetPositions(positions);        
    }

    Vector3 calcQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}