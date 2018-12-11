using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yes_btn : MonoBehaviour {

	public void Quit()
    {
        
        Application.Quit();
    }
    public void Logout()
    {
        SceneManager.LoadScene("LoginTheme");
    }
}
