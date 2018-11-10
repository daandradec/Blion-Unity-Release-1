using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetRequestTexture : MonoBehaviour {


    public void MakeGetRequestMethod(Func<Texture2D, int> Method, string url) { StartCoroutine(GetRequestMethod(Method, url)); }

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
}
