using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController03 : MonoBehaviour {

    private NetworkController networkController;

    private void Awake()
    {
        this.networkController = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkController>();
        RequestAllRemainingBlion();
    }

    private void RequestAllRemainingBlion()
    {
        string[] mediaContentsURLS = this.networkController.GetPersistentObjects().GetUser().mediaContentsURLS;
        var netURLS = this.networkController.GetUrls();
        string[] image_extensions = { ".png", ".jpg", ".jpeg" };
        foreach (string url in mediaContentsURLS)
        {
            if (image_extensions.Contains(Path.GetExtension(url)))
                this.networkController.GetRequestTextureAlpha(RequireMediaContentImage, netURLS.GetMainDomain() + netURLS.GET_USER_MEDIA_CONTENTS + "?path=" + url, url);
            else
                this.networkController.GetPersistentObjects().SetMediaContentsVideo(url);
        }
        //suponer que despues de hacer todas las peticiones por ser asincronas, funcionaran aun cambiando la escena
        StartCoroutine(ChangeScene());
    }

    public int RequireMediaContentImage(Texture2D texture, string pathMediaContent)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        this.networkController.GetPersistentObjects().SetNewMediaContentsImage(sprite);
        this.networkController.GetPersistentObjects().SetMediaContentsImagePathOrder(pathMediaContent);

        return 1;
    }

    IEnumerator ChangeScene() // una opcion podria ser llevar un contador que haga esto si la cuenta es igual al numero de recursos, o en la ultima iteracion de video hacer el cambio de escena
    {
        yield return new WaitForSeconds(1.75f);
        LoadSceneByName("04_User");
    }

    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
