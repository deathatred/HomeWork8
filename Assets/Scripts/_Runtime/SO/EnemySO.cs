using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemiesSO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public GameObject EnemyPrefab;
    [SerializeField] public Sprite EnemyImage;
    [SerializeField] public Sprite EnemyBackground;
    [SerializeField] public string EnemyName;
    [SerializeField] public string EnemyAbout;
}
