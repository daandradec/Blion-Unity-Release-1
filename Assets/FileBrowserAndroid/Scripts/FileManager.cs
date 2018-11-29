using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour {

    private string finalPathSelected = "";
    private Func<string, int> Method;

    public void SetFinalPath(string finalPath)
    {
        finalPathSelected = finalPath;
        Method(finalPath);
    }

    public string GetFinalPathSelected()
    {
        return this.finalPathSelected;
    }

    public void SetExecuteFunction(Func<string, int> Method)
    {
        this.Method = Method;
    }
}
