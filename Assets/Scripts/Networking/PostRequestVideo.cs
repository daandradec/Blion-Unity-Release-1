using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostRequestVideo : MonoBehaviour {

    public void MakePostRequestBytesMethodAlpha(Func<string, GameObject, int> Method, byte[] video, string url, GameObject gameObject, string extension)
    {
        StartCoroutine(PostRequestAnImageBytesMethodAlpha(Method, video, url, gameObject, extension));
    }

    IEnumerator PostRequestAnImageBytesMethodAlpha(Func<string, GameObject, int> Method, byte[] video, string url, GameObject gameObject, string extension)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("mediafile", video, null, "video/" + extension);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("{'success' : 'enviado'}");
                Method(www.downloadHandler.text, gameObject);
            }
        }
    }
}
