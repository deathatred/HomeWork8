using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAction : ActionBase
{
    [SerializeField] private Transform _spawnEnemyLocation;
    [SerializeField] private EnemiesListSO _enemiesList;
    private GameObject _enemyGameObject;
    protected override void ExecuteInternal()
    {
        Platform platform = transform.GetComponent<Platform>();
        float chance = 0.1f;

        if (Random.value < chance && !platform.GetHasSpawnedObject())
        {
            int powerupsCount = _enemiesList.EnemiesList.Count;
            _enemyGameObject = Instantiate(_enemiesList.EnemiesList[Random.Range(0, powerupsCount)].EnemyPrefab,
            _spawnEnemyLocation.position, Quaternion.identity, platform.transform);
            platform.SetHasSpawnedObject();
        }
    }
    private void OnDestroy()
    {
        Destroy( _enemyGameObject );
    }
    public EnemiesListSO GetEnemiesList()
    {
        return _enemiesList;
    }
}
