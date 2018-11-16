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

    public void SetCurrentUser(UserResponse user)
    {
        this.user = user;
    }

    public void SetImageToCurrentUser(Sprite image)
    {
        this.user.image = image;
    }

    public UserResponse GetUser()
    {
        return user;
    }
}

