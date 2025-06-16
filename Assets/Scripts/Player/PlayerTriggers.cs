using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    public static PlayerTriggers Instance { get; private set; }
    public event Action<PowerupsSO> OnPowerupPicked;
    public event EventHandler OnPlayerHit;
    public event Action<PowerupsSO> OnPowerupRefreshed;
    private readonly Dictionary<PowerupsSO, PowerupBase> _activePowerups = new();


    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PowerupBase>(out var powerup))
        {
            var powerupSO = powerup.GetPowerupSO();
            if (powerup != null)
            {
                if (!_activePowerups.ContainsKey(powerupSO))
                {
                    _activePowerups.Add(powerupSO, powerup);
                    powerup.PickUp(gameObject);
                    OnPowerupPicked?.Invoke(powerup.GetPowerupSO());
                }
                else
                {
                    if (_activePowerups[powerupSO] == null)
                    {
                        print("was null");
                        _activePowerups.Remove(powerupSO);
                        _activePowerups.Add(powerupSO, powerup);
                        powerup.PickUp(gameObject);
                        OnPowerupPicked?.Invoke(powerup.GetPowerupSO());
                    }
                    else
                    {
                        _activePowerups[powerupSO].RefreshDuration(gameObject);
                        Destroy(collision.gameObject);
                        OnPowerupPicked?.Invoke(powerup.GetPowerupSO());
                    }
                }
            }
        }
        if (collision.TryGetComponent<ProjectileBase>(out var projectile))
        {
            Destroy(collision.gameObject);
            OnPlayerHit?.Invoke(this,EventArgs.Empty);
        }
    }
}