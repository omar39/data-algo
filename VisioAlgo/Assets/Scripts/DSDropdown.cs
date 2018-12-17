using UnityEngine;
using MaterialUI;
using UnityEngine.SceneManagement;
public class DSDropdown : MonoBehaviour {
    public void OnItemSelected()
    {
        int index = this.GetComponent<MaterialDropdown>().currentlySelected;
        if(index == 0)
        {
            Debug.Log("going to the DSMenu");
        }
        else if( index == 1)
        {
            Debug.Log("going to the DSProblemSet");
            SceneManager.LoadScene("DSProblemSet");
        }
    }
}
