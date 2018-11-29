using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilePanel : MonoBehaviour {

    public OpenFileBrowserAndroid openFileManager;
    public FileManager fileManager;

	public string SearchImage()
    {
        #if UNITY_EDITOR
        return UnityEditor.EditorUtility.OpenFilePanel("Select an image", "", "png,jpg,jpeg,mp4");
#else
        return "C:\\Users\\USUARIO\\Pictures\\";
#endif
    }

    public void SearchImageWithFileManager(Func<string, int> Method)
    {
        fileManager.SetExecuteFunction(Method);
        openFileManager.OpenFileBrowser();
    }
}
