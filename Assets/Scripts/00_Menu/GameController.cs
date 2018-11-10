using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject playerShip_1;
    public GameObject playerShip_2;
    
    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
