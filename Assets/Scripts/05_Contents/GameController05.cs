using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController05 : MonoBehaviour {


    /* VARIABLES ATRIBUTOS QUE VIENEN DEL INSPECTOR */
    public Canvas canvas;
    public GameObject mediaContentsBody;
    public GameObject imageMediaContent;
    public GameObject videoMediaContent;


    /* VARIABLES ATRIBUTOS DE CALCULOS */
    private float width;
    private float x_count;
    private float y_count;
    private float MAX_LIMIT_WIDTH;


    /* VARIABLES ATRIBUTOS COMPONENTES OBJETOS */
    private NetworkController networkController;
    private FilePanel filePanel;


    /* ################################### INICIALIZACIÓN ################################### */

    private void Awake()
    {
        this.networkController = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkController>();
        this.filePanel = this.GetComponent<FilePanel>();
        this.MAX_LIMIT_WIDTH = canvas.GetComponent<RectTransform>().rect.width;
    }

    private void Start()
    {        
        width = (imageMediaContent.GetComponent<RectTransform>().rect.width + 10f) * canvas.scaleFactor;
        x_count = 0f;
        y_count = 0f;
    
        FillMediaContent();
    }






    /* ########################## ITERAR TODOS LOS CONTENIDOS MEDIA DE IMAGENES Y VIDEO Y AÑADIRLOS A LA ESCENA ########################## */

    public void FillMediaContent()
    {
        int index = 0;

        foreach (Sprite image in this.networkController.GetPersistentObjects().GetUser().mediaContentsImages)
        {
            GameObject mediaImage = InstantianteMediaContentComponent(imageMediaContent, (width * x_count), (width * y_count));

            mediaImage.GetComponent<MediaContent>().SetAssociatedPathAndIndex(this.networkController.GetPersistentObjects().GetMediaContentsImagePathOrder(index),index);

            ConfigureMediaContentImage(mediaImage, image);

            UpdateMediaContentCoordinates(mediaImage);

            ++index;

        }

        var netURLS = this.networkController.GetUrls();
        index = 0;

        foreach (string path in this.networkController.GetPersistentObjects().GetUser().mediaContentsVideosURLS)
        {
            GameObject mediaVideo = InstantianteMediaContentComponent(videoMediaContent, (width * x_count), (width * y_count));

            mediaVideo.GetComponent<MediaContent>().SetAssociatedPathAndIndex(path, index);

            mediaVideo.transform.GetChild(0).GetComponent<VideoMediaContent>().ConfigureVideoPlayer(netURLS.GetMainDomain() + netURLS.GET_USER_MEDIA_CONTENTS + "?path=" + path);

            UpdateMediaContentCoordinates(mediaVideo);

            ++index;
        }
    }





    /* ####################   METODOS PARA INSTANCIAR COMPONENTES MEDIA DE IMAGEN O VIDEO   ####################*/


    public GameObject InstantianteMediaContentComponent(GameObject prefab, float offsetx, float offsety)
    {
        GameObject mediaObject = Instantiate(prefab, mediaContentsBody.transform, false);
        RectTransform imageRect = mediaObject.GetComponent<RectTransform>();
        imageRect.anchoredPosition3D = new Vector3(imageRect.anchoredPosition3D.x + offsetx, imageRect.anchoredPosition3D.y - offsety, imageRect.anchoredPosition3D.z);
        return mediaObject;
    }


    /* ####################  METODOS PARA CONFIGURAR LAS IMAGENES O VIDEOS QUE ESTARAN EN LA ESCENA ####################*/


    public void ConfigureMediaContentImage(GameObject mediaImage, Sprite image)
    {
        Image imageObject = mediaImage.transform.GetChild(0).GetComponent<Image>();
        imageObject.preserveAspect = true;
        imageObject.sprite = image;
    }






    /* #################### ACTUALIZADO Y BORRADO DE LAS VARIABLES QUE MANEJAN LA DISPOSICION DE REJILLA EN LA ESCENA #################### */

    public void UpdateMediaContentCoordinates(GameObject mediaImage)
    {
        ++x_count;

        if (mediaImage.GetComponent<RectTransform>().anchoredPosition3D.x + (width * 2f) > MAX_LIMIT_WIDTH)
        {
            x_count = 0;
            ++y_count;
            mediaContentsBody.GetComponent<RectTransform>().sizeDelta = new Vector2(MAX_LIMIT_WIDTH, (width + 10f)*(y_count+1f) );
        }
    }

    public void ClearMediaContent()
    {
        x_count = 0f;
        y_count = 0f;

        foreach (Transform child in mediaContentsBody.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }






    /* ################################### Metodos exclusivos del GameController05 ################################### */

    public float GetMedConOffsetX()
    {
        return (width * x_count);
    }

    public float GetMedConOffsetY()
    {
        return (width * y_count);
    }

    public GameObject GetPrefabImageMediaContent()
    {
        return this.imageMediaContent;
    }

    public GameObject GetPrefabVideoMediaContent()
    {
        return this.videoMediaContent;
    }

    public FilePanel GetFilePanel()
    {
        return this.filePanel;
    }

    public NetworkController GetNetworkController()
    {
        return this.networkController;
    }

    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
