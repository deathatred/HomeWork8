using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAction : ActionBase
{
    [SerializeField] private Transform _spawnEnemyLocation;
    [SerializeField] private List<GameObject> _enemiesList;
    private GameObject _enemyGameObject;
    protected override void ExecuteInternal()
    {
        float chance = 0.1f;

        if (Random.value < chance)
        {
            int powerupsCount = _enemiesList.Count;
            _enemyGameObject = Instantiate(_enemiesList[Random.Range(0, powerupsCount)],
            _spawnEnemyLocation.position, Quaternion.identity);
            Platform platform = transform.GetComponent<Platform>();
            platform.SetHasSpawnedObject();
        }
    }
}
