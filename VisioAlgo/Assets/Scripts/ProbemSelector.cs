using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class ProbemSelector : MonoBehaviour {

    public GameObject[] prblems;
	public void OnItemSelected()
    {
        try
        {
            int selctor = this.GetComponent<MaterialDropdown>().currentlySelected;
            prblems[selctor].SetActive(true);
        }
        catch(System.Exception exc)
        {
            Debug.Log("unhandled Exception thrown");
            return;
        }
     }

}
