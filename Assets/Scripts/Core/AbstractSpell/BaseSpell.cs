using UnityEngine;
using UnityEngine.InputSystem;
public abstract class BaseSpell : MonoBehaviour
{
    public abstract float SkillCooldown { get; set; }
    protected abstract ScriptableObject SpellSO { get; set; }

    public abstract void Cast(Transform playerTransform);
}
