using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    const string levelNo = "levelNo";
    const string curLevelNo = "curLevelNo";

    public static int LevelNo
    {
        get => PlayerPrefs.GetInt(levelNo);
        set => PlayerPrefs.SetInt(levelNo, value);
    }

    public static int CurLevelNo
    {
        get => PlayerPrefs.GetInt(curLevelNo);
        set => PlayerPrefs.SetInt(curLevelNo, value);
    }

    public static bool IsWin;
    public static bool IsLoss;
    public static bool IsTouched;
    public static bool IsPaused;
    public static bool IsAdShown = false;

    private void Start()
    {
        IsWin = false;
        IsLoss = false;
        IsTouched = false;
        IsPaused = false;
    }
}
