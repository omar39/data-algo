using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
   public void backToggle()
    {
        if(SceneManager.GetActiveScene().name==("Main Menu"))
        {
            SceneManager.LoadScene("LoginTheme");
        }
        else if(SceneManager.GetActiveScene().name=="Register")
        {
            SceneManager.LoadScene("LoginTheme");

        }
    }

	
}
