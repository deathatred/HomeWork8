using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTutorialSinglePage : MonoBehaviour
{
    //Enemy SO

    [SerializeField] private TextMeshProUGUI _enemyTitle;
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TextMeshProUGUI _enemySpawnText;
    [SerializeField] private TextMeshProUGUI _enemyAboutText;

    public void Setup(/*Enemy so*/)
    {

        _enemyTitle.text = null;
        _enemyImage.sprite = null;
        _enemySpawnText.text = null;
        _enemyAboutText.text = null;
    }
    private string CheckWhatPlatformCanSpawn(/*EnemySo*/)
    {
       return string.Empty;
    }
}
