using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomeButton : MonoBehaviour, IPointerClickHandler
{
    public FileBrowserAndroid fileBrowserAndroid;

    public void OnPointerClick(PointerEventData eventData)
    {
        fileBrowserAndroid.ReBuildFilesAndDirectories(fileBrowserAndroid.GetRootDirectory());
    }

}
