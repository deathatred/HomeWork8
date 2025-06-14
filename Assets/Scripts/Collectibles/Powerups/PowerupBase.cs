using System.Collections;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    public abstract float Duration { get; set; }
    [SerializeField] protected PowerupsSO _powerupsSO;
    protected bool _isActive;
    private Coroutine _lifetimeRoutine;
    public abstract void Activate(GameObject player);
    public abstract void Deactivate(GameObject player);
    public virtual void PickUp(GameObject player)
    {
        Activate(player);
        _isActive = true;
        if (Duration > 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _lifetimeRoutine = StartCoroutine(DeactivateAfterTime(player));
        }
    }
    public void RefreshDuration(GameObject player)
    {
        if (_lifetimeRoutine != null)
            StopCoroutine(_lifetimeRoutine);

        _lifetimeRoutine = StartCoroutine(DeactivateAfterTime(player));
    }
    protected virtual IEnumerator DeactivateAfterTime(GameObject player)
    {
        yield return new WaitForSeconds(Duration);
        Deactivate(player);
        _isActive = false;
        Destroy(gameObject);
        print("end");
    }
    public PowerupsSO GetPowerupSO()
    {
        return _powerupsSO;
    }
}
