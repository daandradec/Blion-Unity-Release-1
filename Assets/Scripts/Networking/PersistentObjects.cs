using Assets.Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour {

    private UserResponse user;
    
    public void SetCurrentUser(string user)
    {
        this.user = JsonUtility.FromJson<UserResponse>(user);
        this.user.mediaContentsImages = new List<Sprite>();
    }

    public void SetCurrentUser(UserResponse user)
    {
        this.user = user;
    }


    public void SetImageToCurrentUser(Sprite image)
    {
        this.user.image = image;
    }


    public void SetMediaContentsUserURLS(string[] urls)
    {
        this.user.mediaContentsURLS = urls;
    }



    public void SetNewMediaContentsImage(Sprite image)
    {
        this.user.mediaContentsImages.Add(image);
    }

    public void SetMediaContentsImagePathOrder(string path)
    {
        this.user.mediaContentsImagesPathsOrder.Add(path);
    }

    public string GetMediaContentsImagePathOrder(int index)
    {
        return this.user.mediaContentsImagesPathsOrder[index];
    }

    public int GetIndexMediaContentImage(string path)
    {
        return this.user.mediaContentsImagesPathsOrder.IndexOf(path);
    }

    public void RemoveItemMediaContentsImage(int index)
    {
        this.user.mediaContentsImages.RemoveAt(index);
    }

    public void RemoveItemMediaContentsImagePathOrder(int index)
    {
        this.user.mediaContentsImagesPathsOrder.RemoveAt(index);
    }



    public void SetMediaContentsVideo(string path)
    {
        this.user.mediaContentsVideosURLS.Add(path);
    }
    public void RemoveItemMediaContentsVideo(string path)
    {
        this.user.mediaContentsVideosURLS.Remove(path);
    }

    public UserResponse GetUser()
    {
        return user;
    }
}

