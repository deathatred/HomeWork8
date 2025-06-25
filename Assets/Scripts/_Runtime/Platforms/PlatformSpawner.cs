using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Global Settings")]
    [Space]
    [SerializeField] private Transform _target;
    [Space]
    [Header("SpawnSettings")]
    [Space]
    [SerializeField] private List<Platform> _platformPrefabVariants;
    [SerializeField] private GameObject _finishPlatform;
    [SerializeField] private Vector2Int _platformsSpawnedPerStepCount;
    [SerializeField] private int _stepsCountToSpawn;
    [SerializeField] private float _stepsCountToDelete;
    [SerializeField] private float _stepHeight;
    [SerializeField] private Vector2 _bounds;

    private float maxY = 495f;

    private Queue<Platform[]> _spawnedPlatforms;

    private float _lastPlatformsSpawnedOnPlayerPosition;
    private float _lastPlatformsDeletedOnPlayerPosition;

    private void Awake()
    {
         _spawnedPlatforms = new Queue<Platform[]>();

        _lastPlatformsDeletedOnPlayerPosition = _lastPlatformsSpawnedOnPlayerPosition = _target.position.y;

        for (int i = 0; i < _stepsCountToSpawn; i++)
        {
            SpawnPlatform(i + 1);
        }
        Vector3 finishLocation = new Vector3(0, 500, 0);
        Instantiate(_finishPlatform, finishLocation, Quaternion.identity);
    }
    private void Update()
    {
        HandlePlatformSpawning();
    }
    private void HandlePlatformSpawning()
    {
        if (_target.position.y - _lastPlatformsSpawnedOnPlayerPosition > _stepHeight)
        {
            SpawnPlatform(_stepsCountToSpawn);
            _lastPlatformsSpawnedOnPlayerPosition += _stepHeight;
        }
        if (_target.position.y - _lastPlatformsDeletedOnPlayerPosition > _stepHeight * _stepsCountToDelete)
        {
            if (_spawnedPlatforms.Count > 0)
            {
                var platformGroupToDelete = _spawnedPlatforms.Dequeue();

                for (int i = 0; i < platformGroupToDelete.Length; i++)
                {
                    if (platformGroupToDelete[i] && platformGroupToDelete[i].gameObject)
                    {
                        Destroy(platformGroupToDelete[i].gameObject);
                    }
                }

                _lastPlatformsDeletedOnPlayerPosition += _stepHeight;
            }
        }

    }
    private void SpawnPlatform(int stepsCount)
    {
        var platformPositionY = _target.position.y + stepsCount * _stepHeight;

        var platformsToSpawnCount = Random.Range(_platformsSpawnedPerStepCount.x, _platformsSpawnedPerStepCount.y + 1);
        var platformGroup = new Platform[platformsToSpawnCount];

        List<float> usedXPositions = new List<float>();
        float minDistanceBetweenPlatforms = 5f;
        for (int i = 0;i <platformsToSpawnCount;i++)
        {
            float platformPositionX;
            int maxTries = 15;
            int tries = 0;

            do
            {
                platformPositionX = Random.Range(_bounds.x, _bounds.y);
                tries++;
            }
            while (tries < maxTries && usedXPositions.Exists(x => Mathf.Abs(x - platformPositionX) < minDistanceBetweenPlatforms));

            usedXPositions.Add(platformPositionX);

            float randomYOffset = Random.Range(-1f, 1f);
            var platformPosition = new Vector3(platformPositionX, platformPositionY+ randomYOffset, transform.position.z);
            if (platformPosition.y < maxY)
            {
                var randomPlatform = _platformPrefabVariants[Random.Range(0, _platformPrefabVariants.Count)];

                var spawnedPlatform = Instantiate(randomPlatform, platformPosition, Quaternion.identity, this.transform);

                platformGroup[i] = spawnedPlatform;
            }
            else { return; }
        }

        _spawnedPlatforms.Enqueue(platformGroup);
    }
}
