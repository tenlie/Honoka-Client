using UnityEngine;
using GooglePlayGames;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GooglePlayMgr : MonoBehaviour {

    private static GooglePlayMgr _instance;
    public static GooglePlayMgr Instance
    {
        get
        {
            if (_instance==null) _instance = new GooglePlayMgr();
            return _instance;
        }
    }
    
    private bool _authenticating = false;
    public bool _authenticated { get { return Social.Active.localUser.authenticated; } }

    //list of achievements we know we have unlocked (to avoid making repeated calls to the API)
    private Dictionary<string, bool> _unlockedAchievements = new Dictionary<string, bool>();
    //achievement increments we are accumulating locally, waiting to send to the games API
    private Dictionary<string, int> _pendingIncrements = new Dictionary<string, int>();
    
    //구글플레이 초기화
    public void Initialize()
    {
        // Enable/disable logs on the PlayGamesPlatform
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);

        // Activate the Play Games platform. This will make it the default implementation of Social.Active
        PlayGamesPlatform.Activate();
    }

    //구글플레이 로그인
    public void SignInToGooglePlay()
    {
        if (_authenticated || _authenticating)
        {
            Debug.LogWarning("Ignoring repeated call to Authenticate().");
            return;
        }

        _authenticating = true;
        Social.localUser.Authenticate((bool success) =>
        {
            _authenticating = false;
            if (success)
            {
                Debug.Log("Sign in successful!");
            }
            else
            {
                Debug.LogWarning("Failed to sign in with Google Play");
            }
        });
    }

    //구글플레이 로그아웃
    public void SignOutFromGooglePlay()
    {
        GooglePlayGames.PlayGamesPlatform.Instance.SignOut();
    }

    //구글플레이 업적 조회
    public void ShowGooglePlayAchievements()
    {
        if (_authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    //구글플레이 리더보드 조회
    public void ShowLeaderboardUI()
    {
        if (_authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
}
