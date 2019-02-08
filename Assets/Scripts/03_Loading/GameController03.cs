using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets.Scripts.Networking;

public class GameController03 : MonoBehaviour {

    private NetworkController networkController;
    private int resources;
    private int LEN_RESOURCES;

    private void Awake()
    {
        this.networkController = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkController>();
        this.resources = 0;
        CallRequest();
    }


    private void CallRequest()
    {
        var netURLS = this.networkController.GetUrls();
        UserResponse user = this.networkController.GetPersistentObjects().GetUser();
        this.networkController.GetRequestTexture(RequireImage, netURLS.GetMainDomain() + netURLS.GET_USER + user.id + netURLS.GET_USER_IMAGE);        
    }
    private void CallMainRequest()
    {
        var netURLS = this.networkController.GetUrls();
        UserResponse user = this.networkController.GetPersistentObjects().GetUser();
        this.networkController.GetRequest(RequireMediaContentsUser, netURLS.GetMainDomain() + netURLS.GET_USER + user.id + netURLS.GET_USER_CONTENTS);
    }

    private int RequireImage(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        this.networkController.GetPersistentObjects().SetImageToCurrentUser(sprite);
        CallMainRequest();
        return 1;
    }

    private int RequireMediaContentsUser(string answer)
    {
        ResponseList response = JsonUtility.FromJson<ResponseList>(answer);
        if (response.success)
        {
            this.networkController.GetPersistentObjects().SetMediaContentsUserURLS(response.message);
            RequestAllRemainingBlion();
        }

        return 1;
    }

    private void RequestAllRemainingBlion()
    {
        string[] mediaContentsURLS = this.networkController.GetPersistentObjects().GetUser().mediaContentsURLS;
        var netURLS = this.networkController.GetUrls();
        string[] image_extensions = { ".png", ".jpg", ".jpeg" };
        List<string> images_urls = new List<string>();
        foreach (string url in mediaContentsURLS)
        {
            if (image_extensions.Contains(Path.GetExtension(url)))
                images_urls.Add(url);
            else
                this.networkController.GetPersistentObjects().SetMediaContentsVideo(url);
        }
        this.LEN_RESOURCES = images_urls.Count();                
        foreach (string url_image in images_urls)
        {
            this.networkController.GetRequestTextureAlpha(RequireMediaContentImage, netURLS.GetMainDomain() + netURLS.GET_USER_MEDIA_CONTENTS + "?path=" + url_image, url_image);
        }

        if (this.LEN_RESOURCES == 0)
            LoadSceneByName("04_User");
    }

    public int RequireMediaContentImage(Texture2D texture, string pathMediaContent)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        this.networkController.GetPersistentObjects().SetNewMediaContentsImage(sprite);
        this.networkController.GetPersistentObjects().SetMediaContentsImagePathOrder(pathMediaContent);
        ++this.resources;
        if(this.resources >= this.LEN_RESOURCES)
            LoadSceneByName("04_User");
        
        return 1;
    }
    // una opcion podria ser llevar un contador que haga esto si la cuenta es igual al numero de recursos, o en la ultima iteracion de video hacer el cambio de escena

    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
