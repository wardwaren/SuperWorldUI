using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string email {get;}
    public string username {get;}
    public string password {get; set;}
    public string fullName {get;}
    
    public User(string email, string username, string password, string fullName)
    {
        this.email = email;
        this.username = username;
        this.password = password;
        this.fullName = fullName;
    }
}
