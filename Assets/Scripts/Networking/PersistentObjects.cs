using Assets.Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour {

    private UserResponse user;
    
    public void SetCurrentUser(string user)
    {
        this.user = JsonUtility.FromJson<UserResponse>(user);
    }

    public UserResponse GetUser()
    {
        return user;
    }
}

