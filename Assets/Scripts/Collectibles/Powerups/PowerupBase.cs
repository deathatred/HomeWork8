using System.Collections;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    public abstract float Duration { get; set; }
    [SerializeField] protected PowerupsSO _powerupsSO;
    protected bool _isActive;

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
            StartCoroutine(DeactivateAfterTime(player));
        }
    }
    protected virtual IEnumerator DeactivateAfterTime(GameObject player)
    {
        yield return new WaitForSeconds(Duration);
        Deactivate(player);
        _isActive = false;
        Destroy(gameObject);
    }
    public PowerupsSO GetPowerupSO()
    {
        return _powerupsSO;
    }
}
