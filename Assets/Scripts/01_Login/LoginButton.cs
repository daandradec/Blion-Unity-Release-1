using Assets.Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour {

    public GameController01 gameController;


    public void Login()
    {
        string email = GameObject.Find("InputField Email").GetComponent<InputField>().text;
        string password = GameObject.Find("InputField Password").GetComponent<InputField>().text;

        var netController = gameController.GetNetworkController();
        var netUrls = netController.GetUrls();

        string url = netUrls.GetMainDomain() + netUrls.POST_LOGIN_API;
        string[] keys = {"email","password"};
        string[] values = {email,password};

        netController.PostRequestMethod(LoginUser, keys, values,  url);
    }

    private int LoginUser(string answer)
    {
        Response response = JsonUtility.FromJson<Response>(answer);
        if (response.success)
        {
            UserResponse user = JsonUtility.FromJson<UserResponse>(response.message);
            gameController.GetNetworkController().GetPersistentObjects().SetCurrentUser(user);
            gameController.LoadSceneByName("03_User");
        }
        else
            Debug.Log("Post Request Fallido en LoginButton"); 
        
        return 1;
    }
}
