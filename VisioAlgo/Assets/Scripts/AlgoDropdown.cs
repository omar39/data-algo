using UnityEngine;
using MaterialUI;
using UnityEngine.SceneManagement;

public class AlgoDropdown : MonoBehaviour {

	public void OnItemSelected()
    {
        int index = this.GetComponent<MaterialDropdown>().currentlySelected;
        if (index == 0)
        {
            Debug.Log("going to the AlgoMenu");
        }
        else if (index == 1)
        {
            Debug.Log("going to the AlgoProblemSet");
        }
    }
}
