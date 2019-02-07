using Assets.Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AddContentButton : MonoBehaviour {

    public GameController05 gameController;

    public void AddNewContent()
    {
        if (Application.isEditor)
        {
            string path = gameController.GetFilePanel().SearchImage();
            BuildMedia(path);
        }
        else
        {
            gameController.GetFilePanel().SearchImageWithFileManager(BuildMedia);

        }
    }

    private int BuildMedia(string path)
    {
        if (path.Length > 0)
        {
            // si la extension es una imagen, si es un video haga otra cosa

            byte[] img = File.ReadAllBytes(path);
            var netController = gameController.GetNetworkController();
            var netURLS = netController.GetUrls();

            string[] image_extensions = { ".png", ".jpg", ".jpeg" };

            if ( image_extensions.Contains(Path.GetExtension(path)))
            {
                Texture2D texture = GenerateImage(img);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);

                GameObject mediaImage = gameController.InstantianteMediaContentComponent(gameController.GetPrefabImageMediaContent(), gameController.GetMedConOffsetX(), gameController.GetMedConOffsetY());
                gameController.ConfigureMediaContentImage(mediaImage, sprite);
                gameController.UpdateMediaContentCoordinates(mediaImage);

                netController.GetPersistentObjects().SetNewMediaContentsImage(sprite);

                netController.PostRequestImageBytesMethodAlpha(SetMediaContentPathToGameObject, img,
                    netURLS.GetMainDomain() + netURLS.POST_USER + netController.GetPersistentObjects().GetUser().id + netURLS.POST_USER_MEDIA_CONTENTS,
                    mediaImage, Path.GetExtension(path).Substring(1));
            }
            else
            {
                GameObject mediaVideo = gameController.InstantianteMediaContentComponent(gameController.GetPrefabVideoMediaContent(), gameController.GetMedConOffsetX(), gameController.GetMedConOffsetY());
                gameController.UpdateMediaContentCoordinates(mediaVideo);

                netController.PostRequestVideoBytesMethodAlpha(SetMediaContentVideoPathToGameObject, img,
                    netURLS.GetMainDomain() + netURLS.POST_USER + netController.GetPersistentObjects().GetUser().id + netURLS.POST_USER_MEDIA_CONTENTS,
                    mediaVideo, Path.GetExtension(path).Substring(1));
            }
        }
        return 0;
    }

    private int SetMediaContentPathToGameObject(string answer, GameObject mediaImage)
    {
        Response response = JsonUtility.FromJson<Response>(answer);
        if (response.success)
        {            
            mediaImage.GetComponent<MediaContent>().SetAsociatedPath(response.message);
            this.gameController.GetNetworkController().GetPersistentObjects().SetMediaContentsImagePathOrder(response.message);
        }
            
        return 1;
    }

    private int SetMediaContentVideoPathToGameObject(string answer, GameObject mediaVideo)
    {
        Response response = JsonUtility.FromJson<Response>(answer);
        if (response.success)
        {
            var netController = this.gameController.GetNetworkController();
            var netURLS = netController.GetUrls();
            netController.GetPersistentObjects().SetMediaContentsVideo(response.message);
            mediaVideo.GetComponent<MediaContent>().SetAsociatedPath(response.message);
            mediaVideo.GetComponent<VideoMediaContent>().ConfigureVideoPlayer(netURLS.GetMainDomain() + netURLS.GET_USER_MEDIA_CONTENTS + "?path=" + response.message);
        }

        return 1;
    }

    private Texture2D GenerateImage(byte[] img)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(img);
        return texture;
    }
}
