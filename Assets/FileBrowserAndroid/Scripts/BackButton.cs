using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler
{
    public FileBrowserAndroid filebrowserAndroid;

    public void OnPointerClick(PointerEventData eventData)
    {
        filebrowserAndroid.ReBuildFilesAndDirectoriesWithoutMemory(filebrowserAndroid.GetBeforeDirectoryVisisted());
    }

}
