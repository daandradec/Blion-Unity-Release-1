using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton01 : MonoBehaviour {

    public GameController01 gameController;

    public void Exit()
    {
        gameController.LoadSceneByName("00_Menu");
    }
}
