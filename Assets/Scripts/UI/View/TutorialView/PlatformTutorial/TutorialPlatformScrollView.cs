using UnityEngine;

public class PlatformTutorialScrollView : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private PlatformListSO _platformListSO;
    [SerializeField] private GameObject _singlePlatformTutorialPage;

    private void Awake()
    {
        Setup();
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
