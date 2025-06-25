using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _musicSource;
    private void Awake()
    {
        _musicSource = GetComponent<AudioSource>();  
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float saved = PlayerPrefs.GetFloat("MusicVolume");
            print(saved + "SAVED");
            _musicSource.volume = saved;
        }
        else
        {
            float musicDefault = 0.5f;
            _musicSource.volume = musicDefault;
        }

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
            PlayerPrefs.SetFloat("MusicVolume", _musicSource.volume);
            PlayerPrefs.Save();
        }
    }
    private void VolumeDown()
    {
        if (_musicSource != null)
        {
            _musicSource.volume -= 0.1f;
            PlayerPrefs.SetFloat("MusicVolume", _musicSource.volume);
            PlayerPrefs.Save();
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
