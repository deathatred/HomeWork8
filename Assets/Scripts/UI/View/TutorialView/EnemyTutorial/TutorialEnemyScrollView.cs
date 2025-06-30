using UnityEngine;
using UnityEngine.UI;

public class TutorialEnemyScrollView : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private EnemiesListSO _enemiesListSO;
    [SerializeField] private GameObject _singleEnemyTutorialPage;
    [SerializeField] private Button _backButton;
    private void Awake()
    {
        Setup();
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _backButton.onClick.AddListener(Hide);
    }
    private void UnsubscribeFromEvents()
    {
        _backButton.onClick.RemoveListener(Hide);
    }


    private void Setup()
    {
        foreach (var enemy in _enemiesListSO.EnemiesList)
        {
            GameObject pageGameObject = Instantiate(_singleEnemyTutorialPage, _content.transform);
            EnemyTutorialSinglePage page = pageGameObject.GetComponent<EnemyTutorialSinglePage>();
            page.Setup(enemy);
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
