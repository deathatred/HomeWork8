using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlatformSpell : BaseSpell
{
    [SerializeField] private ScriptableObject _spellSO;
    [SerializeField] private GameObject _platformPrefab;
    private float _SkillCooldown = 12f;
    public override float SkillCooldown { get => _SkillCooldown; 
        set 
        {
            if (value < 0 || value > 100)
            {
                Debug.LogError("Cooldown cannot be lesser than zero or bigger than 100, change the value, it is defaulted to 0 now.");
                _SkillCooldown = 0;
            }
            else
            {
                _SkillCooldown = value;
            }
        } 
    }
    protected override ScriptableObject SpellSO
    {
        get => _spellSO;
        set
        {
            if (value is ScriptableObject)
            {
                _spellSO = value;
            }
            else
            {
                Debug.LogError("Value must be Scriptable object");
            }
        }
    }

    public override void Cast(Transform playerTransform)
    {
        float platformYOffset = 4.5f;
        Vector3 platfromLocation = new Vector3(playerTransform.position.x, 
            playerTransform.position.y - platformYOffset,
            playerTransform.position.z);
        Instantiate(_platformPrefab, platfromLocation, Quaternion.identity);
    }
}
