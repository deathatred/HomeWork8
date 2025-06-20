using UnityEngine;

public static class PlayerPrefsManager
{
    private const string GameStartedKey = "GameStarted";
    private const string RecordDistanceKey = "Distance";

    public static bool isGameStarted()
    {
        return PlayerPrefs.GetInt(GameStartedKey, 0) == 1;
    }

    public static void MarkGameAsStarted()
    {
        PlayerPrefs.SetInt(GameStartedKey, 1);
        PlayerPrefs.Save();
    }
    public static void ResetGameStartedFlag()
    {
        PlayerPrefs.DeleteKey(GameStartedKey);
        PlayerPrefs.Save();
    }
    public static void SaveRecordDistance(int distance)
    {
        int savedDistance = PlayerPrefs.GetInt(RecordDistanceKey, 0);

        if (distance > savedDistance)
        {
            PlayerPrefs.SetInt(RecordDistanceKey, distance);
            PlayerPrefs.Save();
        }
    }
    public static int GetRecordDistance()
    {
        return PlayerPrefs.GetInt(RecordDistanceKey, 0);
    }
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
