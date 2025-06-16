using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUpAction : ActionBase
{
    [SerializeField] private Transform _spawnPowerupLocation;
    [SerializeField] private PowerupsListSO _powerupList;
    private GameObject _powerupGameObject;

    protected override void ExecuteInternal()
    {
        float chance = 0.02f;

        if (Random.value < chance)
        {
            int powerupsCount = _powerupList.PowerupList.Count;
            _powerupGameObject = Instantiate(_powerupList.PowerupList[Random.Range(0, powerupsCount)].PowerupPrefab, _spawnPowerupLocation.position, Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        Destroy(_powerupGameObject);
    }
}
