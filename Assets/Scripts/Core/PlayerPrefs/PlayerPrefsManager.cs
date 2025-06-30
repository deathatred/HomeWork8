using UnityEngine;

public static class PlayerPrefsManager
{
    private const string GAME_STARTED_KEY = "GameStarted";
    private const string RECORD_DISTANCE_KEY = "Distance";


    public static bool isGameStarted()
    {
        return PlayerPrefs.GetInt(GAME_STARTED_KEY, 0) == 1;
    }

    public static void MarkGameAsStarted()
    {
        PlayerPrefs.SetInt(GAME_STARTED_KEY, 1);
        PlayerPrefs.Save();
    }
    public static void ResetGameStartedFlag()
    {
        PlayerPrefs.DeleteKey(GAME_STARTED_KEY);
        PlayerPrefs.Save();
    }
    public static void SaveRecordDistance(int distance)
    {
        int savedDistance = PlayerPrefs.GetInt(RECORD_DISTANCE_KEY, 0);

        if (distance > savedDistance)
        {
            PlayerPrefs.SetInt(RECORD_DISTANCE_KEY, distance);
            PlayerPrefs.Save();
        }
    }
    public static int GetRecordDistance()
    {
        return PlayerPrefs.GetInt(RECORD_DISTANCE_KEY, 0);
    }
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public static bool CheckForKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    public static float GetFloatFromKey(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
}
