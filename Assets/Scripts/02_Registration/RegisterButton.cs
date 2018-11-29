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


            StartCoroutine(CallRequest(netController, user));
        }
        else
            gameController.GetNetworkController().LogRequestErrorMessage(response.message);

        return 1;
    }

    IEnumerator CallRequest(NetworkController netController, UserResponse user)
    {
        var netURLS = netController.GetUrls();
        gameController.GetNetworkController().GetRequest(RequireMediaContentsUser, netURLS.GetMainDomain() + netURLS.GET_USER + user.id + netURLS.GET_USER_CONTENTS);
        yield return new WaitForSeconds(0.5f);
        gameController.GetNetworkController().GetRequestTexture(RequireImageAndLogin, netURLS.GetMainDomain() + netURLS.GET_USER + user.id + netURLS.GET_USER_IMAGE);
    }

    private int RequireMediaContentsUser(string answer)
    {
        ResponseList response = JsonUtility.FromJson<ResponseList>(answer);
        if (response.success)
        {
            gameController.GetNetworkController().GetPersistentObjects().SetMediaContentsUserURLS(response.message);
        }

        return 1;
    }

    private int RequireImageAndLogin(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        gameController.GetNetworkController().GetPersistentObjects().SetImageToCurrentUser(sprite);

        gameController.LoadSceneByName("03_Loading");
        return 1;
    }
}
