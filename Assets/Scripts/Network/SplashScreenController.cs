using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public bool loadGameLobby;

	void Start ()
    {
        StartCoroutine(LoadGameLobby());
	}
	
	IEnumerator LoadGameLobby()
    {
        yield return new WaitForSeconds(5f);
        if (loadGameLobby)
        {
            SceneManager.LoadScene("GameLobby");
        }
	}
}
