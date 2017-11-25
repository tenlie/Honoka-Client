using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;

public class NetworkController : MonoBehaviour
{
    public static NetworkController Instance;

    private string _connStr = "Server=honoka.cbj19ew5j4m4.ap-northeast-2.rds.amazonaws.com;" +
                     "Port=3306;" +
                     "Database=Honoka;" +
                     "uid=underdogs;" +
                     "pwd=xkdlxksvhf2;";

    public string connStr { get { return _connStr; } }

    public MySqlConnection conn = null;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            Debug.Log("Connection State: " + conn.State);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.ToString());
            conn.Close();
        }
    }

    //DB조회 예제
    public void selectTestDB()
    {
        try
        {
            DataSet ds = new DataSet();

            string sql = "select * from Test3";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds,"Test3");
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Debug.Log(row["name"].ToString());
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }
}
