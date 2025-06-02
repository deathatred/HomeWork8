using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour, IHasHealth
{
    [SerializeField]
    private int _health;
    protected Rigidbody2D _rb;
    protected bool _isAlive = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public int Health
    {
        get => _health;
        set
        {
            if (value < 0 || value > 100)
            {
                _health = 100;
            }
            else {
                _health = value;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            _isAlive = false;
            StartCoroutine(EnableFallingAfterDelay(0.3f));
        }
    }

    private IEnumerator EnableFallingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        //GetComponent<Collider2D>().enabled = false;
    }
}
