using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Powerup", menuName = "PowerupsSO")]
public class PowerupsSO : ScriptableObject
{
    [SerializeField] public GameObject PowerupPrefab;
    [SerializeField] public Image PowerupIcon;
    [SerializeField] public Sprite PowerupSprite;
}