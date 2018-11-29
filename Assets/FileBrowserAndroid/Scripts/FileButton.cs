using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileButton : FileBrowserButton
{
    public Sprite[] images;
    public Image spriteImage;

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.fileBrowserAndroid.GetFileManager().SetFinalPath(absolutePath);
        Destroy(this.fileBrowserAndroid.gameObject);
    }

    public void SetImage(string extension)
    {
        switch (extension)
        {
            case ".png":
                spriteImage.sprite = images[0];
                break;
            case ".jpeg":
                spriteImage.sprite = images[0];
                break;
            case ".jpg":
                spriteImage.sprite = images[0];
                break;
            case ".mp4":
                spriteImage.sprite = images[1];
                break;
        }
    }
}
