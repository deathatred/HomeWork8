using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTutorialSinglePage : MonoBehaviour
{
    [SerializeField] private PlatformListSO _platformList;
    [SerializeField] private TextMeshProUGUI _enemyTitle;
    [SerializeField] private Image _enemyImage;
    [SerializeField] private Image _enemyBackground;
    [SerializeField] private TextMeshProUGUI _enemySpawnText;
    [SerializeField] private TextMeshProUGUI _enemyAboutText;

    public void Setup(EnemySO enemy)
    {
        _enemyTitle.text = enemy.EnemyName;
        _enemyImage.sprite = enemy.EnemyImage;
        _enemyBackground.sprite = enemy.EnemyBackground;
        _enemySpawnText.text = $"Spawns on: {CheckWhatPlatformCanSpawn(enemy)}";
        _enemyAboutText.text = enemy.EnemyAbout;
    }
    private string CheckWhatPlatformCanSpawn(EnemySO enemy)
    {
        List<string> listOfPlatforms = new();

        foreach (PlatformSO platform in _platformList.PlatformList)
        {
            if (platform.PlatformPrefab.TryGetComponent<SpawnEnemyAction>(out var spawn))
            {
                if (spawn.GetEnemiesList().EnemiesList.Contains(enemy))
                {
                    listOfPlatforms.Add(platform.PlatformName);
                }
            }
        }
        return listOfPlatforms.Count > 0 ? string.Join(", ", listOfPlatforms) : "Nothing";
    }
}
