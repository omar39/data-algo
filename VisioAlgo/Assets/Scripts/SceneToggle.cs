using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneToggle : MonoBehaviour
{
    public Animator Anim;
    public Image Img;
    public void Toggle()
    {
        if (SceneManager.GetActiveScene().name == "LoginTheme")
        {

            StartCoroutine(Fade());
           
        }
        
           
    }
  
    IEnumerator Fade()
    {
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneManager.LoadScene("Main Menu");
    }
    
    public void ToggleRegister()
    {
        if (SceneManager.GetActiveScene().name == "LoginTheme")
        {
            SceneManager.LoadScene("Register");
        }
        
    }

  
}
