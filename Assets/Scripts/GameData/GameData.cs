using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[CreateAssetMenu(fileName ="GameData" , menuName ="Custom Items/GameData")]
public class GameData : ScriptableObject
{
    #region FIELD DECLERATION

    [Header("PROPERTIES")]
    public bool resetOnExit;

    [Header("LEVEL DATA")]
    public int maxLevelsCap;
    public int currentLevel;
    public int levelWinCount;
    public int levelFailCount;

    [Header("PLAYER PROGRESSION")]
    public bool membership;
    public int playerLives;
    public int playerScore;

    private PlayerSaveData _playerData;
    private const string _playerProgression = "PLAYER PROGRESSION";

    #endregion

    #region PLAYER DATA HANDLING

    /// <summary>
    /// Store Data to Object and return Json string
    /// </summary>
    public void SavePlayerProgression()
    {
        _playerData = new PlayerSaveData();
        _playerData.membership = this.membership;
        _playerData.playerScore = this.playerScore;
        _playerData.currentLevel = this.currentLevel;
        _playerData.levelFailCount = this.levelFailCount;
        _playerData.levelWinCount = this.levelWinCount;

        PlayerPrefs.SetString(_playerProgression, JsonUtility.ToJson(_playerData));
    }

    public void LoadPlayerProgression()
    {
        // Restore Player Data 
        _playerData = new PlayerSaveData();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(_playerProgression), _playerData);

        this.membership = _playerData.membership;
        this.playerScore = _playerData.playerScore;
        this.currentLevel = _playerData.currentLevel;
        this.levelFailCount = _playerData.levelFailCount;
        this.levelWinCount = _playerData.levelWinCount;

        playerLives = membership? 5 : 3;
    }

    #endregion

    private void OnDisable()
    {
        // used for reseting values
        #if UNITY_EDITOR
        if (resetOnExit)
        {
            playerScore = 0;
            levelWinCount = 0;
            levelFailCount = 0;
            currentLevel = 0;
        }
        #endif
    }

}

[Serializable]
public class PlayerSaveData{
    public bool membership;
    public int playerScore;
    public int currentLevel;
    public int levelWinCount;
    public int levelFailCount;
}