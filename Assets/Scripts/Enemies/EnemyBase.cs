using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHasHealth
{
    protected abstract string Name { get; set; }
    protected abstract int Damage { get; set; }
    protected abstract int StartHealth { get; }
    private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            if (value <= 0)
            {
                _health = 1;
            }
            else
            {
                _health = value;
            }
        }

    }
    protected virtual void Start()
    {
        Health = StartHealth;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            //Die
        }
    }
    public abstract void Attack();
}
