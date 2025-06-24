using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private GameObject _floatingtextObject;
    public static PlayerTriggers Instance { get; private set; }
    public event EventHandler OnPlayerHit;
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
                    GameEventBus.PowerupPickedUp(powerupSO);
                }
                else
                {
                    if (_activePowerups[powerupSO] == null)
                    {
                        print("was null");
                        _activePowerups.Remove(powerupSO);
                        _activePowerups.Add(powerupSO, powerup);
                        powerup.PickUp(gameObject);
                        GameEventBus.PowerupPickedUp(powerupSO);
                    }
                    else
                    {
                        _activePowerups[powerupSO].RefreshDuration(gameObject);
                        Destroy(collision.gameObject);
                        GameEventBus.PowerupPickedUp(powerupSO);
                    }
                }
            }
            GameObject floatingText = Instantiate(_floatingtextObject, transform.position + Vector3.up,
                Quaternion.identity);
            floatingText.GetComponent<FloatingText>().Show(powerupSO);
        }
        if (collision.TryGetComponent<ProjectileBase>(out var projectile))
        {
            Destroy(collision.gameObject);
            OnPlayerHit?.Invoke(this,EventArgs.Empty);
        }
    }
}