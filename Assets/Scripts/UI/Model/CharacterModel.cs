using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CharacterModel : MonoBehaviour
{
    public static int Distance;
    public static int PowerupsCollected;

    public static void ResetStats()
    {
        Distance = 0;
        PowerupsCollected = 0;
    }
}
