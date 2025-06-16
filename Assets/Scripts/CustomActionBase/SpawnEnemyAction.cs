using UnityEngine;

public class SpawnEnemyAction : ActionBase
{
    [SerializeField] private Transform _spawnEnemyLocation;
    [SerializeField] private PowerupsListSO _enemiesList;
    private GameObject _enemyGameObject;
    protected override void ExecuteInternal()
    {
        float chance = 0.02f;

        if (Random.value < chance)
        {
            int powerupsCount = _enemiesList.PowerupList.Count;
            //_powerupGameObject = Instantiate(_powerupList.PowerupList[Random.Range(0, powerupsCount)].PowerupPrefab,
            //_spawnPowerupLocation.position, Quaternion.identity);
        }
    }
}
