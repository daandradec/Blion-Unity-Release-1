using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class FileBrowserButton : MonoBehaviour, IPointerClickHandler
{

	protected Text buttonText;
    protected string absolutePath;
    protected FileBrowserAndroid fileBrowserAndroid;

    private void Awake()
    {
        buttonText = this.transform.GetChild(0).GetComponent<Text>();
    }

    public void SetText(string text) {
        this.buttonText.text = text;
    }

    public void SetAbsolutePath(string path)
    {
        this.absolutePath = path;
    }

    public void SetFileBrowser(FileBrowserAndroid fileBrowser)
    {
        this.fileBrowserAndroid = fileBrowser;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
