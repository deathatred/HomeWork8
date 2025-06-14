using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerPowerupManager : MonoBehaviour
{
    private Dictionary<PowerupsSO, Coroutine> _activePowerups = new();
    private Dictionary<PowerupsSO, PowerupBase> _powerupInstances = new();

    public void ApplyPowerup(PowerupBase powerup)
    {
        PowerupsSO powerupSO = powerup.GetPowerupSO();
        if (_activePowerups.ContainsKey(powerupSO))
        {
            StopCoroutine(_activePowerups[powerupSO]);
            Destroy(_powerupInstances[powerupSO]);
        }
        powerup.Activate(gameObject);
        _powerupInstances[powerupSO] = powerup;
        _activePowerups[powerupSO] = StartCoroutine(HandleDuration(powerup));
    }
    private IEnumerator HandleDuration(PowerupBase powerup)
    {
        print("Im in corotine");
        float total = powerup.Duration;
        float timeLeft = total;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        powerup.Deactivate(gameObject);

        PowerupsSO powerupSO = powerup.GetPowerupSO();

        _activePowerups.Remove(powerupSO);
        _powerupInstances.Remove(powerupSO);
        Destroy(powerup.gameObject);
    }
}
