using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreData : MonoBehaviour
{
    public GameData gameData;
    private void Start()
    {
        gameData.LoadPlayerProgression();
    }

}
