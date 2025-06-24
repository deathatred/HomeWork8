using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;
    private float _moveUpDistance = 2f;
    private float _duration = 1f;

    public void Show(PowerupsSO pwrup)
    {
        _image.sprite = pwrup.PowerupSprite;
        transform.DOLocalMoveY(transform.localPosition.y + _moveUpDistance, _duration);
        _text.DOFade(0, _duration).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}
