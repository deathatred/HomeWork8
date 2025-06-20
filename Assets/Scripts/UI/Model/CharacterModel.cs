using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CharacterModel : MonoBehaviour
{
    public static int Distance;
    public static int PowerupsCollected;

    private void OnEnable()
    {
        Distance = 0;
        PowerupsCollected = 0;
    }
    private void OnDisable()
    {
        Distance = 0;
        PowerupsCollected = 0;
    }
}
