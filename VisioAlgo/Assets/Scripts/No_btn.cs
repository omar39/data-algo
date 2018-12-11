using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class No_btn : MonoBehaviour {
    public GameObject panel;
    public GameObject Shadow;

    public void CloseWindow()
    {
        Shadow.SetActive(false);
        panel.SetActive(false);
    }
}
