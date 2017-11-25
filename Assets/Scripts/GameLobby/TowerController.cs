using UnityEngine;
using System;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;

public class TowerController : MonoBehaviour
{
    private MySqlConnection _conn;        //SQL커넥터

    public string _workHour { get; set; }     //야근시간
    public string _currHeight { get; set; }   //탑의 현재 높이
    public string _remainHeight { get; set; } //다음 업적까지 남은 높이
    public string _currAchv { get; set; }     //현재업적

    public GameObject lb_CurrAchv;
    public GameObject lb_CurrHeight;
    public GameObject lb_RemainHeight;
    public GameObject lb_workHour;

    void Awake()
    {
        _conn = null;

        _workHour = null;
        _currHeight = null;
        _remainHeight = null;
        _currAchv = null;
    }

    void Start()
    {
        GetUserInfo();
    }

    void Update()
    {

    }

    void OnPress(bool isBtnDown)
    {
        ShowWorkHour(isBtnDown);
    }

    //UserInfo 가져오기
    public void GetUserInfo()
    {
        string query = "";
        MySqlDataAdapter adapter;
        DataSet ds = new DataSet();

        try
        {
            _conn = new MySqlConnection(NetworkController.Instance.connStr);
            _conn.Open();
            query = "select * from userinfo where userid ='" + SaveData._userID + "'";
            adapter = new MySqlDataAdapter(query, _conn);
            adapter.Fill(ds, "userinfo");

            if (ds.Tables.Count == 1)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Debug.Log("workhour: " + row["workhour"].ToString());
                    Debug.Log("currheight: " + row["currheight"].ToString());
                    Debug.Log("remainheight: " + row["remainheight"].ToString());
                    Debug.Log("achvname: " + row["achvname"].ToString());
                    lb_workHour.GetComponent<UILabel>().text = _workHour = row["workhour"].ToString();
                    lb_CurrHeight.GetComponent<UILabel>().text = _currHeight = row["currheight"].ToString();
                    lb_RemainHeight.GetComponent<UILabel>().text = _remainHeight = row["remainheight"].ToString();
                    lb_CurrAchv.GetComponent<UILabel>().text = _currAchv = row["achvname"].ToString();
                }
            }
            else
            {
                Debug.LogWarning("UserID Select Error");
            }

        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.ToString());
        }
        finally
        {
            if (_conn != null)
            {
                _conn.Close();
            }
        }
    }

    public void BuildTower()
    {

    }

    //야근시간 보여주기
    public void ShowWorkHour(bool isBtnDown)
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "ShowWorkTime()");
#endif
        if(isBtnDown)
        {
            NGUITools.SetActive(lb_workHour, true);
        }
        else
        {
            NGUITools.SetActive(lb_workHour, false);
        }
    }

    //배경 설정
    public void SetBackground()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "SetBackground()");
#endif
    }

    //업종에 따른 탑재료 설정
    public void SetTowerMaterial()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "SetTowerMaterial()");
#endif
    }

    //업종에 따른 탑재료 효과음 설정
    public void SetTowerMaterialSFX()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "SetTowerMaterialSFX()");
#endif
    }

    //유저정보 새로고침
    public void RefreshUserInfo()
    {
#if UNITY_EDITOR
        Debug.Log(this.name + " >>> " + "RefreshUserInfo()");
#endif
        GetUserInfo();
        SetBackground();
    }
}
