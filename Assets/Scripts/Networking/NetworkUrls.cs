using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUrls : MonoBehaviour {

    private string DOMAIN = "http://aqueous-tor-84195.herokuapp.com";

    /* APIS URLS */
    public string GET_USER = "/api/users/";
    public string GET_USER_IMAGE = "/image";
    public string POST_USER = "/api/users/";
    public string POST_USER_IMAGE = "/image";

    public string POST_LOGIN_API = "/api/login";
    public string POST_REGISTER_API = "/api/register";

    public string GetMainDomain()
    {
        return this.DOMAIN;
    }
}
