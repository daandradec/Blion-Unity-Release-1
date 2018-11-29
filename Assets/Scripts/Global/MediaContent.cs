using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaContent : MonoBehaviour {

    private string path;

    public string GetAsociatedPath()
    {
        return this.path;
    }

    public void SetAsociatedPath( string path )
    {
        this.path = path;
    }
}
