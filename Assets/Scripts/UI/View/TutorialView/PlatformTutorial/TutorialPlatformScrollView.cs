using UnityEngine;
using UnityEngine.UI;

public class PlatformTutorialScrollView : MonoBehaviour
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
        _backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
    private void UnsubscribeFromEvents()
    {
        _backButton.onClick.RemoveAllListeners();
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
}
