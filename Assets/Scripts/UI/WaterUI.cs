using UnityEngine;
using UnityEngine.UI;


public class WaterUI : MonoBehaviour
{
    [SerializeField] private Image _waterMapImage;
    [SerializeField] private Image _playerIconImage;
    [SerializeField] private GameObject _waterGameObject;
    [SerializeField] private GameObject _player;

    private float startY;
    private float currentY;

    private float minY = -400f; // Minimal Y pos
    private float maxY = 400f;  // Maximum Y pos
    private float maxDistance = 500f; // Max distance between player and finish platform

    private void Start()
    {
        startY = _waterGameObject.transform.position.y;
    }
    private void Update()
    {
        ShowWaterLevel();
        HandlePlayerIcon();
    }
    private void ShowWaterLevel()
    {
        currentY = _waterGameObject.transform.position.y;
        float distanceTotal = maxDistance - startY;
        float distanceCurrent = currentY - startY;
        float progress = distanceCurrent / distanceTotal;
        _waterMapImage.fillAmount = Mathf.Clamp01(progress);

    }
    private void HandlePlayerIcon()
    {
        float distance = Vector3.Distance(_player.transform.position, new Vector3(0, 500, 0));

        float progress = 1f - Mathf.Clamp01(distance / maxDistance);
        float iconYPosition = Mathf.Lerp(minY, maxY, progress);

        Vector2 anchoredPos = _playerIconImage.rectTransform.anchoredPosition;
        _playerIconImage.rectTransform.anchoredPosition = new Vector2(anchoredPos.x, iconYPosition);
    }
}



