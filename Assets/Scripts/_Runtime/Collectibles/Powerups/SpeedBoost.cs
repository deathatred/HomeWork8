using UnityEngine;
using UnityEngine.Rendering;

public class SpeedBoost : PowerupBase
{
    public float _duration = 3f;
    private float _speedBoostValue = 12f;
    private float _baseMoveSpeed;
    private bool _isSpeedStored = false;
    public override float Duration
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
            if (!_isSpeedStored)
            {
                _baseMoveSpeed = playerMovement.GetPlayerMoveSpeed();
                _isSpeedStored = true;
            }
            playerMovement.SetPlayerSpeed(_speedBoostValue);
       }
    }

    public override void Deactivate(GameObject player)
    {
        if (player.TryGetComponent<Player2DMovement>(out var playerMovement))
        {
            playerMovement.SetPlayerSpeed(7f);
            print("Deactivated");
        }

    }
}
