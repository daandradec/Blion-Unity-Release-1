using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderButton : FileBrowserButton
{
    public Sprite secondaryImage;
    public Image spriteImage;

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.fileBrowserAndroid.ReBuildFilesAndDirectories(absolutePath);
    }

    public void ModifyImageIfContainsExtension(string[] availableExtensions)
    {
        string[] directories = Directory.GetDirectories(absolutePath);
        string[] files = Directory.GetFiles(absolutePath);

        if(CheckFiles(availableExtensions, files) || CheckFilesInDirectories(availableExtensions, directories))
        {
            spriteImage.sprite = secondaryImage;
        }
    }

    private bool CheckFiles(string[] availableExtensions, string[] files)
    {
        foreach (string file in files)
        {
            if (availableExtensions.Contains(Path.GetExtension(file)))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckFilesInDirectories(string[] availableExtensions, string[] directories)
    {
        foreach (string directory in directories)
        {
            if (CheckFiles(availableExtensions, Directory.GetFiles(directory)))
            {
                return true;
            }
        }
        return false;
    }
}
