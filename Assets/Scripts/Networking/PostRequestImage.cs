using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostRequestImage : MonoBehaviour {

    public void MakePostRequest(byte[] image,string url) {
        string base64encoded = Convert.ToBase64String(image);
        base64encoded = base64encoded.Replace("+", @"%2B");
        base64encoded = base64encoded.Replace("/", @"%2F");
        base64encoded = base64encoded.Replace("=", @"%3D");

        StartCoroutine(PostRequestAnImage(base64encoded, url));
    }

    IEnumerator PostRequestAnImage(string base64, string url)
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
