using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

    public GameObject SettingsPopup;
    public GameObject FriendsTab;
    public GameObject CompanyTab;
    public GameObject SpecialtyTab;
    public GameObject UserinfoPopup;
    public AudioSource BGM;

    public UIToggle[] toggle_sound;

    private bool _isGameStarted;

    public void OnChangeSoundToggle()
    {
        Debug.Log("sound");
        // 현재 상태가 변한 토글버튼을 가져옵니다.
        UIToggle current = UIToggle.current;
        Debug.Log(current);
        // 우리는 활성화 된 경우만 처리할 예정이므로,
        // 활성화 된것(value가 true)이 아닌경우(false인 경우) return합니다.
        //if (current.value == false) return;
    }

    public void OnChangeTimeCheckToggle()
    {
        Debug.Log("autotime");
        // 현재 상태가 변한 토글버튼을 가져옵니다.
        UIToggle current = UIToggle.current;
        Debug.Log(current);
        // 우리는 활성화 된 경우만 처리할 예정이므로,
        // 활성화 된것(value가 true)이 아닌경우(false인 경우) return합니다.
        //if (current.value == false) return;
    }

    public void OnChangePushToggle()
    {
        Debug.Log("push");
        // 현재 상태가 변한 토글버튼을 가져옵니다.
        UIToggle current = UIToggle.current;
        Debug.Log(current);
        // 우리는 활성화 된 경우만 처리할 예정이므로,
        // 활성화 된것(value가 true)이 아닌경우(false인 경우) return합니다.
        //if (current.value == false) return;
    }

    void Awake()
    {
        _isGameStarted = false;
    }

    void Start () {
        SaveData.LoadOption();
        BGM.Play();
        _isGameStarted = true;
    }
	
	void Update () {
	
	}

    void OnApplicationPause(bool pause)
    {
        if (!_isGameStarted)
            return;

        if (pause)
        {
            BGM.Pause();
        }
        else
        {
            BGM.Play();
        }
    }

    public void ShareOnFacebook()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "ShareOnFacebook()");
#endif
    }

    public void ShareOnInstagram()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "ShareOnInstagram()");
#endif
    }

    public void OpenSettings()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenSettings()");
#endif
        NGUITools.SetActive(SettingsPopup, true);
    }

    public void CloseSettings()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "CloseSettings()");
#endif
        NGUITools.SetActive(SettingsPopup, false);
        SaveData.SaveOption();
    }

    public void ConnectToFacebook()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "ConnectToFacebook()");
#endif
    }

    public void OpenUserinfoPopup()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenUserinfoPopup()");
#endif
        NGUITools.SetActive(UserinfoPopup, true);
    }

    public void CloseUserinfoPopup()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenUserinfoPopup()");
#endif
        NGUITools.SetActive(UserinfoPopup, false);
    }

    public void OpenFriendsTab()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenFriendsTab()");
#endif
        NGUITools.SetActive(FriendsTab, true);
        NGUITools.SetActive(CompanyTab, false);
        NGUITools.SetActive(SpecialtyTab, false);
    }

    public void OpenCompanyTab()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenCompanyTab()");
#endif
        NGUITools.SetActive(FriendsTab, false);
        NGUITools.SetActive(CompanyTab, true);
        NGUITools.SetActive(SpecialtyTab, false);
    }

    public void OpenSpecialtyTab()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "OpenSpecialtyTab()");
#endif
        NGUITools.SetActive(FriendsTab, false);
        NGUITools.SetActive(CompanyTab, false);
        NGUITools.SetActive(SpecialtyTab, true);
    }

}
