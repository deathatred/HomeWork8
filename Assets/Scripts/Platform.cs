using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour, IHasHealth
{
    [SerializeField]
    private int health;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public int Health
    {
        get => health;
        set
        {
            if (value < 0 || value > 100)
            {
                health = 100;
            }
            else {
                health = value;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            StartCoroutine(EnableFallingAfterDelay(0.3f));
        }
    }

    private IEnumerator EnableFallingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Collider2D>().enabled = false;
    }
}
