using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    private static SaveData _instance;
    public static SaveData Instance { get { return _instance; } }
    const float _saveDataVersion = 0.1f;
    public static string _saveDate = "(non)";

    // Init UserInfo
    public static string _userID = "";
    public static string _pwd = "";

    // Init Option
    public static string _sound = "ON";
    public static string _autoTimeCheck = "ON";
    public static string _pushAlarm = "ON";

    static void SaveDataHeader(string dataGroupName)
    {
        PlayerPrefs.SetFloat("SaveDataVersion", _saveDataVersion);
        _saveDate = System.DateTime.Now.ToString("G");
        PlayerPrefs.SetString("SaveDataDate", _saveDate);
        PlayerPrefs.SetString(dataGroupName, "on");
    }

    static bool CheckSaveDataHeader(string dataGroupName)
    {
        if (!PlayerPrefs.HasKey("SaveDataVersion"))
        {
            Debug.Log("SaveData.CheckData : No Save Data");
            return false;
        }
        if (PlayerPrefs.GetFloat("SaveDataVersion") != _saveDataVersion)
        {
            Debug.Log("SaveData.CheckData : Version Error");
            return false;
        }
        if (!PlayerPrefs.HasKey(dataGroupName))
        {
            Debug.Log("SaveData.CheckData : No Group Data");
            return false;
        }
        _saveDate = PlayerPrefs.GetString("SaveDataDate");
        return true;
    }

    public static bool SaveUserInfo()
    {
        try
        {
            Debug.Log("SaveData.SaveUserInfo : Start");

            // UserID Data
            SaveDataHeader("SDG_UserInfo");
            PlayerPrefs.SetString("UserID", _userID);
            PlayerPrefs.SetString("Pwd", _pwd);

            // Save
            PlayerPrefs.Save();
            Debug.Log("SaveData.SaveUserInfo : End");
            return true;
        }
        catch(System.Exception e)
        {
            Debug.LogWarning("SaveData.SaveUserInfo : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static bool SaveOption()
    {
        try
        {
            Debug.Log("SaveData.SaveOption : Start");
            
            // Option Data
            SaveDataHeader("SDG_Option");
            PlayerPrefs.SetString("Sound", _sound);
            PlayerPrefs.SetString("AutoTimeCheck", _autoTimeCheck);
            PlayerPrefs.SetString("AutoTimeCheck", _pushAlarm);

            // Save
            PlayerPrefs.Save();
            Debug.Log("SaveData.SaveOption : End");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.SaveOption : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static bool LoadOption()
    {
        try
        {
            if (CheckSaveDataHeader("SDG_Option"))
            {
                Debug.Log("SaveData.LoadOption : Start");

                _sound = PlayerPrefs.GetString("Sound");
                _autoTimeCheck = PlayerPrefs.GetString("AutoTimeCheck");

                Debug.Log("SaveData.LoadOption : End");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.LoadOption : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static void DeleteAndInit(bool init)
    {
        Debug.Log("SaveData.DeleteAndInit : DeleteAll");
        PlayerPrefs.DeleteAll();

        if (init)
        {
            Debug.Log("SaveData.DeleteAndInit : Init");
            _saveDate = "(non)";
        }
    }
}


