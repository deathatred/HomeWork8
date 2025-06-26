using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTutorialSinglePage : MonoBehaviour
{
    private PlatformSO _platformSO;

    [SerializeField] private TextMeshProUGUI _platformTitle;
    [SerializeField] private Image _platformImage;
    [SerializeField] private TextMeshProUGUI _platformHealth;
    [SerializeField] private TextMeshProUGUI _platformSpawnText;
    [SerializeField] private TextMeshProUGUI _platformAboutText;

    public void Setup(PlatformSO _platformSO)
    {
        _platformSO.PlatformPrefab.TryGetComponent<Platform>(out var platform);

        _platformTitle.text = _platformSO.PlatformName;
        _platformImage.sprite = _platformSO.PlatformImage;
        _platformHealth.text = $"Health: {platform.Health}";
        _platformSpawnText.text = $"Spawns: {CheckWhatPlatformCanSpawn(platform)}";
        _platformAboutText.text = _platformSO.PlatformAbout;
    }
    private string CheckWhatPlatformCanSpawn(Platform platform)
    {
        string result = string.Empty;
        if (platform.TryGetComponent<SpawnEnemyAction>(out _))
        {
            result += "enemies";
        }
        if (platform.TryGetComponent<SpawnPowerUpAction>(out _))
        {
            result += ", powerups";
        }

        if (string.IsNullOrEmpty(result))
        {
            return "nothing";
        }
        else 
        return result;
    }
}
