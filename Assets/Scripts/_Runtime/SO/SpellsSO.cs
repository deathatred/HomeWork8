using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Spell",menuName = "SpellsSO")]
public class SpellsSO : ScriptableObject
{
    [SerializeField] public InputActionReference spellAction;
    [SerializeField] public Sprite spellImage;
    [SerializeField] public GameObject spellPrefab;


}
