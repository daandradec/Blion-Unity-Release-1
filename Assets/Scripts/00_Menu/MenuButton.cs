using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public GameController gameController;


    public void ButtonEventLogin()
    {
        gameController.playerShip_1.GetComponent<PlayerMovement>().MakeAnimation("01_Login");
    }

    public void ButtonEventRegister()
    {
        gameController.playerShip_2.GetComponent<PlayerMovement>().MakeAnimation("02_Registration");
    }
}
