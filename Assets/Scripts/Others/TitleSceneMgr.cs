using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class TitleSceneMgr : MonoBehaviour {

    //salt key for password encryption
    private string _salt = "z}/`fPl+Uos<|sK^vBf32`[@H8, 8.shA | P | ZP3V434SQ9n;Kh2fB_SE}~|pSH::2";

    public float duration;
    public GameObject Logo;
    public GameObject Title;
    public GameObject Pnl_Login;
    public GameObject Pnl_SignUp;
    public GameObject Pnl_SearchAccount;
    public UILabel InputID;
    public UILabel InputPW;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(InitCo());
	}
	
    IEnumerator InitCo()
    {
        //FacebookMgr.Initialize();
        //FacebookMgr.SignIn();
        GooglePlayMgr.Initialize();
        GooglePlayMgr.SignInToGooglePlay();
        yield return new WaitForSeconds(duration);

        NGUITools.SetActive(Logo, false);
        yield return new WaitForSeconds(duration);

        if (SaveData._autoLogin.Equals("ON"))
        {
            Login(SaveData._userID, SaveData._userPW);
        }
        else
        {
            NGUITools.SetActive(Pnl_Login, true);
            TweenAlpha.Begin(Pnl_Login, duration, 1.0f);
        }
    }

    public void OpenSignUpPopup()
    {
        NGUITools.SetActive(Pnl_SignUp, true);
    }

    public void CloseSignUpPopup()
    {
        NGUITools.SetActive(Pnl_SignUp, false);
    }

    public void OpenSearchAccountPopup()
    {
        NGUITools.SetActive(Pnl_SearchAccount, true);
    }

    public void CloseSearchAccountPopup()
    {
        NGUITools.SetActive(Pnl_SearchAccount, false);
    }

    public void CheckAutoLogin(bool isChecked)
    {
        if (isChecked)
        {
            SaveData._autoLogin = "ON";
        }
        else
        {
            SaveData._autoLogin = "OFF";
        }
    }

    public void Login(string id, string pw)
    {
        StartCoroutine(LoginCo(id, pw));
    }

    IEnumerator LoginCo(string id, string pw)
    {
        Debug.Log("Login()");
        Debug.Log("Encrypting Password...");
        string encryptedPW = EncryptString(pw, _salt);
        Debug.Log("ID:" + id + "\nPW:" + encryptedPW);
        string url = NetworkMgr.ServerURL + "getUser.do";

        WWWForm form = new WWWForm();
        form.AddField("UserID", "admin1");
        form.AddField("CrunchTime", "100");

        WWW www = new WWW(url, form);
        yield return www;

        //if (www.error==null)
        //{
            string result = www.text;
            Debug.Log("result: " + result);

            if (result!="")
            {
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.Log("UserID does not exist");
            }
        //}
    }

    private string EncryptString(string InputText, string Salt)
    {
        // Rihndael class를 선언하고, 초기화
        RijndaelManaged rijndaelCipher = new RijndaelManaged();

        // 입력받은 문자열을 바이트 배열로 변환
        byte[] plainText = System.Text.Encoding.Unicode.GetBytes(InputText);

        // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서 
        // Salt를 사용한다.
        byte[] salt = Encoding.UTF8.GetBytes(Salt.Length.ToString());

        // PasswordDeriveBytes 클래스를 사용해서 SecretKey를 얻는다.
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(salt, salt);

        // Create a encryptor from the existing SecretKey bytes.
        // encryptor 객체를 SecretKey로부터 만든다.
        // Secret Key에는 32바이트
        // (Rijndael의 디폴트인 256bit가 바로 32바이트입니다)를 사용하고, 
        // Initialization Vector로 16바이트
        // (역시 디폴트인 128비트가 바로 16바이트입니다)를 사용한다.
        ICryptoTransform Encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

        // 메모리스트림 객체를 선언,초기화 
        MemoryStream memoryStream = new MemoryStream();

        // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

        // 암호화 프로세스가 진행된다.
        cryptoStream.Write(plainText, 0, plainText.Length);

        // 암호화 종료
        cryptoStream.FlushFinalBlock();

        // 암호화된 데이터를 바이트 배열로 담는다.
        byte[] cipherBytes = memoryStream.ToArray();

        // 스트림 해제
        memoryStream.Close();
        cryptoStream.Close();

        // 암호화된 데이터를 Base64 인코딩된 문자열로 변환한다.
        string encryptedData = Convert.ToBase64String(cipherBytes);

        // 최종 결과를 리턴
        return encryptedData;
    }

    private string DecryptString(string InputText, string Password)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();

        byte[] encryptedData = Convert.FromBase64String(InputText);
        byte[] salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, salt);

        // Decryptor 객체를 만든다.
        ICryptoTransform Decryptor = rijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

        MemoryStream memoryStream = new MemoryStream(encryptedData);

        // 데이터 읽기(복호화이므로) 용도로 cryptoStream객체를 선언, 초기화
        CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

        // 복호화된 데이터를 담을 바이트 배열을 선언한다.
        // 길이는 알 수 없지만, 일단 복호화되기 전의 데이터의 길이보다는
        // 길지 않을 것이기 때문에 그 길이로 선언한다.
        byte[] plainText = new byte[encryptedData.Length];

        // 복호화 시작
        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);

        memoryStream.Close();
        cryptoStream.Close();

        // 복호화된 데이터를 문자열로 바꾼다.
        string decryptedData = Encoding.Unicode.GetString(plainText, 0, decryptedCount);

        // 최종 결과 리턴
        return decryptedData;
    }
}
