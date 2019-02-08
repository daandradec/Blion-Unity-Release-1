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
        var netURLS = netController.GetUrls();

        string url = netURLS.GetMainDomain() + netURLS.POST_LOGIN_API;
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
            var netController = gameController.GetNetworkController();
            netController.GetPersistentObjects().SetCurrentUser(user);

            gameController.LoadSceneByName("03_Loading");
        }
        else
            gameController.GetNetworkController().LogRequestErrorMessage(response.message);    
            
        
        return 1;
    }
}
