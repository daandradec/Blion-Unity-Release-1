using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler
{
    public FileBrowserAndroid filebrowserAndroid;

    public void OnPointerClick(PointerEventData eventData)
    {
        string path = filebrowserAndroid.GetBeforeDirectoryVisisted();
        string root_directory = filebrowserAndroid.GetRootDirectory();
        if (path == root_directory && filebrowserAndroid.GetCurrentPath() == root_directory)
        {
            Destroy(filebrowserAndroid.gameObject);
            return;
        }
        filebrowserAndroid.ReBuildFilesAndDirectoriesWithoutMemory(path);
    }

}
