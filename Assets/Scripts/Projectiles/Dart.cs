using UnityEngine;

public class Dart : ProjectileBase
{
    [SerializeField] private float _speed = 15f;
    private Vector3 _direction;

    private int _damage = 1;
    protected override int Damage { get => _damage;
        set
        {
            if (value < 0)
            {
                _damage = 0;
            } 
            else
            {
                _damage = value;
            }
        }
    }


    public override void Setup(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _direction = direction;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

}
