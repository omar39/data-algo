using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;
using System.Diagnostics;


public class Visualize : MonoBehaviour {
    public List<Button> squares; 
     public struct number
    {
        public int value;
       public bool marked;
    };
    private number[] n;
 private bool isPrime(int n)
    {
        for (int i = 2; i < n; i++)
        {
            if (n % i == 0)
            {
                return false;
            }

        }
        return true;
    }

    // Update is called once per frame
    IEnumerator  sieve () {
        print("Entered");
        //squares = new List<Button>();
        n = new number[101] ;
        squares[0].enabled = false;
        for (int i = 0; i <= 100; i++)
        {
            n[i].value = i + 1;
            n[i].marked = false;
        }
        n[0].marked = true;
       
       
        for (int i = 1; i <= 10; i++)
        {
            if (n[i].marked)
            {
                continue;
            }
            if (isPrime(n[i].value))
            {


                int multiple = n[i].value;

                int j = i;

                int sum = j + multiple;

                while (sum < 100)
                {
                    n[sum].marked = true;
                    squares[sum].enabled = false;
                    yield return new WaitForSeconds(0.5f);     
                    sum = sum + multiple;

                }
            }
        }
        
    }
   
    public void calling()
    {
        StartCoroutine(sieve());
    }
}
