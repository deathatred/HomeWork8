using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public static int Distance;
    public static int PowerupsCollected;

    private void OnEnable()
    {
        PowerupsCollected = 0;
    }
    private void OnDisable()
    {
        PowerupsCollected = 0;
    }
}
