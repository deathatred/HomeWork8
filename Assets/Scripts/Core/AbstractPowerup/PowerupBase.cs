using System.Collections;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    public abstract float Duration { get; set; }
    [SerializeField] protected PowerupsSO _powerupsSO;
    protected bool _isActive;
    private Coroutine _lifetimeRoutine;
    private float _timeRemaining;
    public abstract void Activate(GameObject player);
    public abstract void Deactivate(GameObject player);

    public virtual void PickUp(GameObject player)
    {
        transform.SetParent(null);
        Activate(player);
        _isActive = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        _timeRemaining = Duration;

        if (_lifetimeRoutine != null) StopCoroutine(_lifetimeRoutine);

        _lifetimeRoutine = StartCoroutine(DeactivateTimer(player));
    }
    public virtual void RefreshDuration(GameObject player)
    {
        _timeRemaining = Duration;
    }

    private IEnumerator DeactivateTimer(GameObject player)
    {
        while (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            yield return null;
        }

        Deactivate(player);
        _isActive = false;
        _lifetimeRoutine = null;
        Destroy(gameObject);

    }

    public PowerupsSO GetPowerupSO()
    {
        return _powerupsSO;
    }
}

