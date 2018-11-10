using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLoginButton : MonoBehaviour {

    public GameController03 gameController;

    public void UnLogin()
    {
        gameController.LoadSceneByName("01_Login");
    }
}
