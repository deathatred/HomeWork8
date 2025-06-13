using UnityEngine;
using UnityEngine.Rendering;

public class SpeedBoost : PowerupBase
{
    public float _duration = 10f;
    private float _speedBoostValue = 5f;
    private float _baseMoveSpeed;
    protected override float Duration
    {
        get => _duration;
        set
        {
            if (value > 0 && value < 100)
            {
                _duration = value;
            }
            else
            {
                _duration = 0;
            }
        }
    }

    public override void Activate(GameObject player)
    {     
       if (player.TryGetComponent<Player2DMovement>(out var playerMovement))
       {
            playerMovement.ModifyPlayerSpeed(_speedBoostValue);
       }
    }

    public override void Deactivate(GameObject player)
    {
        if (player.TryGetComponent<Player2DMovement>(out var playerMovement))
        {
            playerMovement.ModifyPlayerSpeed(-_speedBoostValue);
        }
    }
}
