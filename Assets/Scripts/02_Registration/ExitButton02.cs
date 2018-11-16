using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton02 : MonoBehaviour {

    public GameController02 gameController;

    public void Exit()
    {
        gameController.LoadSceneByName("00_Menu");
    }
}
