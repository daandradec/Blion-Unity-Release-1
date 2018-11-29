using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetRequestTexture : MonoBehaviour {


    public void MakeGetRequestMethod(Func<Texture2D, int> Method, string url) { StartCoroutine(GetRequestMethod(Method, url)); }
    public void MakeGetRequestMethodAlpha(Func<Texture2D, string, int> Method, string url, string pathMediaContent) { StartCoroutine(GetRequestMethod(Method, url, pathMediaContent)); }

    IEnumerator GetRequestMethod(Func<Texture2D, int> Method, string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            UnityWebRequestAsyncOperation request = www.SendWebRequest();
            while (!request.isDone)
            {
                //Debug.Log("www img progress: "+www.downloadProgress);
                yield return null;
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Method(((DownloadHandlerTexture)www.downloadHandler).texture);
            }
        }
    }

    IEnumerator GetRequestMethod(Func<Texture2D, string, int> Method, string url, string pathMediaContent)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            UnityWebRequestAsyncOperation request = www.SendWebRequest();
            while (!request.isDone)
            {
                //Debug.Log("www img progress: "+www.downloadProgress);
                yield return null;
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Method(((DownloadHandlerTexture)www.downloadHandler).texture, pathMediaContent);
            }
        }
    }
}
