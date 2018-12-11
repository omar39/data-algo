using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class Trainee {

    public string userName { get; set; }
    public string userPassword { get; set; }
    public Trainee(string name, string password)
    {
        this.userName = name;
        this.userPassword = password;
    }
    public Trainee()
    {
        userName = userPassword = null;
    }
    
}
