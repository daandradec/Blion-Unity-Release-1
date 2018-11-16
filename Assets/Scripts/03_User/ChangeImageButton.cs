using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChangeImageButton : MonoBehaviour {

    public GameController03 gameController;

    public void ModifyCurrentImageUser()
    {
        string path = gameController.GetFilePanel().SearchImage();
        if(path.Length > 0)
        {
            byte[] img = File.ReadAllBytes(path);
            var netController = gameController.GetNetworkController();
            //var netURLS = netController.GetUrls();

            Texture2D texture = GenerateImage(img);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
            netController.GetPersistentObjects().SetImageToCurrentUser(sprite);                    
            gameController.userImage.sprite = sprite;            

            //netController.PostRequestImage(img, 
            //    netURLS.GetMainDomain() + netURLS.GET_USER + netController.GetPersistentObjects().GetUser().id + netURLS.POST_USER_IMAGE);
        }
    }

    private Texture2D GenerateImage(byte[] img)
    {        
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(img);
        return texture;
    }
}
