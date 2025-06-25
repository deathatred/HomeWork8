using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Platform", menuName = "PlatfromsSO")]
public class PlatformSO : ScriptableObject
{
    [SerializeField] public GameObject PlatformPrefab;
    [SerializeField] public Sprite PlatformImage;
    [SerializeField] public string PlatformName;
    [SerializeField] public string PlatformAbout;
}


