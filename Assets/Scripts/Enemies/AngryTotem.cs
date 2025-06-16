using UnityEngine;

public class AngryTotem : EnemyBase
{
    [SerializeField] private GameObject _projectilePrefab;
    private string _name = "AngryTotem";
    private int _damage = 5;
    private int _startHealth = 1;
    private GameObject _target;
    private float _attackTimer;
    private float _attackTimerMax = 1.5f;
    protected override string Name { get => _name; set => _name = value.ToString(); }
    protected override int StartHealth { get => _startHealth; }
    protected override int Damage
    {
        get => _damage;
        set
        {
            if (value < 0) value = 0;
            else _damage = value;
        }
    }
    protected override void Start()
    {
        base.Start();
        _target = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (_attackTimer <= 0)
        {
            Attack();
            _attackTimer = _attackTimerMax;
        }
        _attackTimer -= Time.deltaTime;

    }
    public override void Attack()
    {
        GameObject projectileBase = Instantiate(_projectilePrefab,transform.position,Quaternion.identity);
        ProjectileBase projectile = projectileBase.GetComponent<ProjectileBase>();
        projectile.Setup(_target.transform.position);
    }
}
