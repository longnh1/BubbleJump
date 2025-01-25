using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level = 0;
}

public static class KEY
{
    public static string GAME_DATA = "gamedata";
}

public class DataManager : Singleton<DataManager>
{
    public PlayerData playerData = new PlayerData();

    public bool isLoad = false;

    void Start()
    {
        GameLoad();
    }

    private void OnApplicationPause(bool pause)
    {
        GameSave();
    }
    private void OnApplicationQuit()
    {
        GameSave();
    }

    public void GameSave()
    {
        if (!isLoad) return;

        string _data = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(KEY.GAME_DATA, _data);
        PlayerPrefs.Save();
    }

    public void GameLoad()
    {
        if (PlayerPrefs.HasKey(KEY.GAME_DATA))
        {
            string _data = PlayerPrefs.GetString(KEY.GAME_DATA);
            playerData = JsonUtility.FromJson<PlayerData>(_data);

        }
        else
        {
            playerData.level = 0;
        }

        isLoad = true;
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ChangeLevel(int level = 1)
    {
        playerData.level += level;

    }
}
