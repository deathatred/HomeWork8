using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTutorialSinglePage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _platformTitle;
    [SerializeField] private Image _platformImage;
    [SerializeField] private Image _platformBackground;
    [SerializeField] private TextMeshProUGUI _platformHealth;
    [SerializeField] private TextMeshProUGUI _platformSpawnText;
    [SerializeField] private TextMeshProUGUI _platformAboutText;

    public void Setup(PlatformSO platformSO)
    {
        if (!platformSO.PlatformPrefab.TryGetComponent<Platform>(out var platform))
        {
            Debug.LogError($"Platform component not found on prefab: {platformSO.PlatformPrefab.name}");
            return;
        }

        _platformTitle.text = platformSO.PlatformName;
        _platformImage.sprite = platformSO.PlatformImage;
        _platformBackground.sprite = platformSO.PlatformBackground;
        _platformHealth.text = $"Health: {platform.Health}";
        _platformSpawnText.text = $"Spawns: {GetPlatformSpawnInfo(platform)}";
        _platformAboutText.text = platformSO.PlatformAbout;
    }
    private string GetPlatformSpawnInfo(Platform platform)
    {
        List<string> spawnables = new();

        if (platform.TryGetComponent<SpawnEnemyAction>(out _))
            spawnables.Add("enemies");

        if (platform.TryGetComponent<SpawnPowerUpAction>(out _))
            spawnables.Add("powerups");

        return spawnables.Count > 0 ? string.Join(", ", spawnables) : "nothing";
    }
}
