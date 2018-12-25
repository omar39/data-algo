using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

public class TabViewerManager : MonoBehaviour {

    public TabView tabs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(tabs.currentPage == 2)
        {
            Debug.Log("We are here");
            this.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        }
        else
        {
            this.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }
	}
}
