using UnityEngine;
using UnityEngine.UI;

public class TutorialPlatformScrollView : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private PlatformListSO _platformListSO;
    [SerializeField] private GameObject _singlePlatformTutorialPage;
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
        foreach (var platform in _platformListSO.PlatformList)
        {
            GameObject pageGameObject = Instantiate(_singlePlatformTutorialPage, _content.transform);
            PlatformTutorialSinglePage page = pageGameObject.GetComponent<PlatformTutorialSinglePage>();
            page.Setup(platform);
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
