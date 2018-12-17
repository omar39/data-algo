using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class SelectionModifier : MonoBehaviour {

    public MaterialDropdown topicMenu, problemsMenu;
    private List<OptionData> DsOptions, AlgoOptions;
    
	public void Modify () {
        DsOptions = new List<OptionData>();
        AlgoOptions = new List<OptionData>();
        OptionData option = new OptionData();
        OptionData option2 = new OptionData();
        OptionData option3 = new OptionData();
        OptionData option4 = new OptionData();
        option.text = "Balanced Brackets";
        DsOptions.Add(option);
        option2.text = "Balanced ";
        DsOptions.Add(option2);
        option3.text = "Seive or Sieve ?";
        AlgoOptions.Add(option3);
        option4.text = "Points Collection";
        AlgoOptions.Add(option4);

        if (topicMenu.currentlySelected == 0)
        {
            problemsMenu.ClearData();
            foreach (var item in DsOptions)
                problemsMenu.AddData(item);
            Debug.Log("you chose ds");
        }
        else if(topicMenu.currentlySelected == 1)
        {
            problemsMenu.ClearData();
            foreach (var item in AlgoOptions)
                problemsMenu.AddData(item);
            Debug.Log("you chose algo");
        }
	}
    public void OnItemSelected()
    {
     //   Start();
        Modify();
    }
}
