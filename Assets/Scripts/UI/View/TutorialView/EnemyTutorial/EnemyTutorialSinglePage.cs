using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTutorialSinglePage : MonoBehaviour
{
    [SerializeField] private PlatformListSO _platformList;
    [SerializeField] private TextMeshProUGUI _enemyTitle;
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TextMeshProUGUI _enemySpawnText;
    [SerializeField] private TextMeshProUGUI _enemyAboutText;

    public void Setup(EnemySO enemy)
    {
        _enemyTitle.text = enemy.EnemyName;
        _enemyImage.sprite = enemy.EnemyImage;
        _enemySpawnText.text = $"Spawns on: {CheckWhatPlatformCanSpawn(enemy)}";
        _enemyAboutText.text = enemy.EnemyAbout;
    }
    private string CheckWhatPlatformCanSpawn(EnemySO enemy)
    {
        string result = string.Empty;
        foreach (PlatformSO platform in _platformList.PlatformList) 
        {
            print(platform.PlatformName);
            if (platform.PlatformPrefab.TryGetComponent<SpawnEnemyAction>(out var spawn))
            {
                if (spawn.GetEnemiesList().EnemiesList.Contains(enemy))
                {
                    result += $" {platform.PlatformName}" ;
                }
            }
        }
        return result;


    }
}
