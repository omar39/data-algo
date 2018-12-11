using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class FixRatio : MonoBehaviour {

    public MaterialButton btn;
    public Canvas canvas;
	void Start () {
        float aspectRatio = canvas.scaleFactor;
        btn.transform.position = new Vector3(btn.transform.position.x * aspectRatio, btn.transform.position.y);
        Debug.Log("aspect ratio : " + aspectRatio.ToString());
	}
	
}
