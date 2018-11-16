using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController03 : MonoBehaviour {

    private NetworkController networkController;
    private FilePanel filePanel;

    public Image userImage;
    public TextMeshProUGUI textField;

    private void Awake()
    {
        this.networkController = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkController>();
        this.filePanel = this.GetComponent<FilePanel>();       
    }

    private void Start()
    {
        textField.text = "email: " + networkController.GetPersistentObjects().GetUser().email;
        userImage.sprite = networkController.GetPersistentObjects().GetUser().image;
        userImage.preserveAspect = true;
    }

    public NetworkController GetNetworkController()
    {
        return this.networkController;
    }

    public FilePanel GetFilePanel()
    {
        return this.filePanel;
    }

    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
