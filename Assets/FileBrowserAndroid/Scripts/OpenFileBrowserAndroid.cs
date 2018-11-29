using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFileBrowserAndroid : MonoBehaviour {

	public GameObject fileBrowserAndroid;

    public void OpenFileBrowser()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Instantiate(fileBrowserAndroid,canvas.transform,false);
    }
}
