using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class SelectionModifier : MonoBehaviour {

    public MaterialDropdown topicMenu, problemsMenu;
    private List<OptionData> DsOptions, AlgoOptions;
    private OptionData option1, option2;
    public void Modify () {
        DsOptions = new List<OptionData>();
        AlgoOptions = new List<OptionData>();
        option1 = new OptionData();
        option2 = new OptionData();
        option1.text = "Borei and Marei";
        AlgoOptions.Add(option1);
        option1 = new OptionData();
        option1.text = "Gold Hasher ";
        AlgoOptions.Add(option1);
        option1 = new OptionData();
        option1.text = "Hashing and The Guitar";
        AlgoOptions.Add(option1);
        option1 = new OptionData();
        option1.text = "Playing with Cubes";
        AlgoOptions.Add(option1);
        option1 = new OptionData();
        option1.text = "Sum of Primes";
        AlgoOptions.Add(option1);
        option1 = new OptionData();
        option1.text = "Collector";
        AlgoOptions.Add(option1);
        option2 = new OptionData();
        option2.text = "Balanced Brackets";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Cinema";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Counting Letters";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Decimal and Binary";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Dexetr GCD";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Help Hashing";
        DsOptions.Add(option2);
        option2 = new OptionData();
        option2.text = "Playing with Cards";
        DsOptions.Add(option2);

        if (topicMenu.currentlySelected == 1)
        {
            problemsMenu.ClearData();
            foreach (var item in DsOptions)
                problemsMenu.AddData(item);
            Debug.Log(topicMenu.currentlySelected.ToString());
        }
        else if(topicMenu.currentlySelected == 0)
        {
            problemsMenu.ClearData();
            foreach (var item in AlgoOptions)
                problemsMenu.AddData(item);
            Debug.Log(topicMenu.currentlySelected.ToString());
        }
	}
    public void select()
    {
        Debug.Log(problemsMenu.currentlySelected.ToString());
    }
    public void OnItemSelected()
    {
     //   Start();
        Modify();
    }
}
