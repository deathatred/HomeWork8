using System;
using UnityEngine;

public class PlayerPowerupsPickup : MonoBehaviour
{
    public static PlayerPowerupsPickup Instance { get; private set; }
    public event Action<PowerupsSO> OnPowerupPicked;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PowerupBase>(out var powerup))
        {
            if (powerup != null)
            {
                powerup.PickUp(gameObject);
                OnPowerupPicked?.Invoke(powerup.GetPowerupSO());
            }
        }
    }
}
