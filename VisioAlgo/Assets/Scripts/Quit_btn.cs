using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit_btn : MonoBehaviour {
    public GameObject panel;
    public GameObject Shadow;
	public void ActivateMenu()
    {
        panel.SetActive(true);
        Shadow.SetActive(true);
    }
}
