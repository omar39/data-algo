using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCSS : MonoBehaviour {

    public GameObject cell;

//    GameObject cell;
    GameObject[] line;
    int width, height;

    // Use this for initialization
    void Start()
    {
        width = 1;
        height = 1;

        //      cell = Instantiate(new GameObject(), new Vector3(-5, -5, 0), Quaternion.identity);
        //    cell.name = "cell";

        line = new GameObject[4];
        for (int i = 0; i != 4; i++)
        {
            line[i] = Instantiate(new GameObject(), new Vector3(-5, -5, 0), Quaternion.identity);
            line[i].name = "Line" + i.ToString();
            line[i].AddComponent<LineRenderer>();
            line[i].GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);
        }

        float x = cell.transform.position.x;
        float y = cell.transform.position.y; 

        line[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x, y, 0));
        line[0].GetComponent<LineRenderer>().SetPosition(1, new Vector3(x + width, y, 0));

        line[1].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x + width, y, 0));
        line[1].GetComponent<LineRenderer>().SetPosition(1, new Vector3(x + width, y + height, 0));

        line[2].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x + width, y + height, 0));
        line[2].GetComponent<LineRenderer>().SetPosition(1, new Vector3(x, y + height, 0));

        line[3].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x, y + height, 0));
        line[3].GetComponent<LineRenderer>().SetPosition(1, new Vector3(x, y, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
