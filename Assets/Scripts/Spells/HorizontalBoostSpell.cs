using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalBoostSpell : BaseSpell
{
    [SerializeField] private ScriptableObject _spellSO;
    [SerializeField] private float _SkillCooldown = 2f;
    public override float SkillCooldown
    {
        get => _SkillCooldown;
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
        PlayerAnimation playerAnim = playerTransform.GetComponent<PlayerAnimation>();
        Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 0f;
        playerAnim.SetIsSpinning(true);
    }
    public void BoostUpwards()
    {
        //also need to propel a bit up if idle(!) and freeze while spinning
        Transform playerTransform = GameObject.FindWithTag("Player").transform;
        Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();
        PlayerEffects playerEffects = playerTransform.GetComponent<PlayerEffects>();
        playerEffects.EnableTrail();
        PlayerAnimation playerAnim = playerTransform.GetComponent<PlayerAnimation>();
        playerAnim.SetIsSpinning(false);
        playerRb.gravityScale = 5f;
        float jumpForce = 60f;
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
    }
    private IEnumerator StartSpinningAndBoostingUp(Transform playerTransform)
    {
        PlayerAnimation playerAnim = playerTransform.GetComponent<PlayerAnimation>();
        playerAnim.SetIsSpinning(true);
        yield return new WaitForSeconds(2f);
        playerAnim.SetIsSpinning(false);
        Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();
        float jumpForce = 50f;
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
    }
}
