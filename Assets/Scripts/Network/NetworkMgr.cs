using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkMgr : MonoBehaviour
{
    public static string ServerURL = "localhost:9090/Honoka/";

    private static NetworkMgr _instance;

    public static NetworkMgr GetInstance()
    {
        return _instance;
    }

    static bool get(string dataGroupName)
    {
        return false;
    }

    public static bool SaveUserInfo()
    {

        return false;
    }

    public static bool SaveOption()
    {
       
        return false;
    }

    public static bool LoadOption()
    {
        
        return false;
    }

    public static void DeleteAndInit(bool init)
    {

    }
}


