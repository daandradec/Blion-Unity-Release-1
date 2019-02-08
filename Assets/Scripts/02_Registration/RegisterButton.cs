using Assets.Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterButton : MonoBehaviour {

    public GameController02 gameController;

	public void Register()
    {
        string name = GameObject.Find("InputField Name").GetComponent<InputField>().text;
        string email = GameObject.Find("InputField Email").GetComponent<InputField>().text;
        string password = GameObject.Find("InputField Password").GetComponent<InputField>().text;
        string password_confirmation = GameObject.Find("InputField PasswordConfirm").GetComponent<InputField>().text;

        var netController = gameController.GetNetworkController();
        var netURLS = netController.GetUrls();

        string url = netURLS.GetMainDomain() + netURLS.POST_REGISTER_API;
        string[] keys = { "name","email", "password" , "password_confirmation" };
        string[] values = { name, email, password, password_confirmation };

        netController.PostRequestMethod(RegisterUser, keys, values, url);
    }

    public int RegisterUser(string answer)
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
