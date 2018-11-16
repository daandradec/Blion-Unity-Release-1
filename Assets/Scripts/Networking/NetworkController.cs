using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviour {

    private GetRequest getRequest;
    private GetRequestTexture getRequestTexture;
    private PostRequest postRequest;
    private PostRequestImage postRequestImage;

    private PersistentObjects persistentObjects;
    private NetworkUrls urls;
    
    /*DEBUG*/
    public PauseManager pauseManager;

    private void Awake()
    {
        getRequest = this.GetComponent<GetRequest>();
        getRequestTexture = this.GetComponent<GetRequestTexture>();
        postRequest = this.GetComponent<PostRequest>();
        postRequestImage = this.GetComponent<PostRequestImage>();
        persistentObjects = this.GetComponent<PersistentObjects>();
        urls = this.GetComponent<NetworkUrls>();
        RequireCanvasPauseManager();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GetRequest(Func<string, int> Method, string url)
    {
        this.getRequest.MakeGetRequestMethod(Method, url);
    }

    public void GetRequestTexture(Func<Texture2D, int> Method, string url)
    {
        this.getRequestTexture.MakeGetRequestMethod(Method, url);
    }

    public void PostRequest(string[] keys, string[] values, string url)
    {
        this.postRequest.MakePostRequest(keys, values, url);
    }

    public void PostRequestMethod(Func<string, int> Method, string[] keys, string[] values, string url)
    {
        this.postRequest.MakePostRequestMethod(Method, keys, values, url);
    }

    public void PostRequestImage(byte[] image, string url)
    {
        this.postRequestImage.MakePostRequest(image, url);
    }

    public void LogRequestErrorMessage(string text)
    {
        pauseManager.PauseWithTextParameter(text);
    }


    /* GETS */

    public NetworkUrls GetUrls()
    {
        return this.urls;
    }
    public PersistentObjects GetPersistentObjects()
    {
        return this.persistentObjects;
    }

    /* SCENES EVENTS */

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "01_Login":
                this.RequireCanvasPauseManager();
                break;
            case "02_Registration":
                this.RequireCanvasPauseManager();
                break;
        }
    }

    private void RequireCanvasPauseManager()
    {
        this.pauseManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseManager>();
    }
}