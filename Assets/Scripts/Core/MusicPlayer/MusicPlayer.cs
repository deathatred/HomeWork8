using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _musicSource;
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        _musicSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void VolumeUp()
    {
        if (_musicSource != null)
        {
            _musicSource.volume += 0.1f;
        }
    }
    private void VolumeDown()
    {
        if (_musicSource != null)
        {
            _musicSource.volume -= 0.1f;
        }
    }
    private void SubscribeToEvents()
    {
        GameEventBus.OnMusicPlusButtonClicked += GameEventBusOnMusicPlusButtonClicked;
        GameEventBus.OnMusicMinusButtonClicked += GameEventBusOnMusicMinusButtonClicked;
    }
    private void UnsubscribeFromEvents()
    {
        GameEventBus.OnMusicPlusButtonClicked -= GameEventBusOnMusicPlusButtonClicked;
        GameEventBus.OnMusicMinusButtonClicked -= GameEventBusOnMusicMinusButtonClicked;
    }
    private void GameEventBusOnMusicMinusButtonClicked()
    {
        VolumeDown();
    }
    private void GameEventBusOnMusicPlusButtonClicked()
    {
        VolumeUp();
    }
}
