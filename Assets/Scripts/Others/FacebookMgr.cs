using UnityEngine;
using Facebook.Unity;
using System.Collections;

public class FacebookMgr
{
    //Singleton Pattern
    private static FacebookMgr _instance;
    public static FacebookMgr Instance { get { return _instance; } }

    public static bool IsLoggedIn
    {
        get { return FB.IsLoggedIn; }
    }

    public static void Initialize()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public static void SignIn()
    {
        FB.LogInWithReadPermissions();
    }

    public static void SignOut()
    {
        FB.LogOut();
    }
}
