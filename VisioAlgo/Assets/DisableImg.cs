using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisableImg : MonoBehaviour {

   public Image Img;
	
	void LateUpdate () {

 
        if (Img.color.a == 0)
        {
            Img.enabled = (false);
            
            return;
        }
        
    }
}
