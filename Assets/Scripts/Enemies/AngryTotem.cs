using UnityEngine;

public class AngryTotem : EnemyBase
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public static int AttackHash = Animator.StringToHash("Attack");
    public static int BlinkHash = Animator.StringToHash("Blink");

    private string _name = "AngryTotem";
    private int _damage = 5;
    private int _startHealth = 1;
    private GameObject _target;
    private float _attackTimer;
    private float _attackTimerMax = 3f;

    private bool _hasBlinked;

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
        _attackTimer = _attackTimerMax;
    }
    private void Update()
    {
        if (_attackTimer <= 0 && _spriteRenderer.isVisible)
        {
            _animator.SetTrigger(AttackHash);
            Attack();
            _attackTimer = _attackTimerMax;
            _hasBlinked = false;
        }
        if (!_hasBlinked && _attackTimer <= _attackTimerMax/2)
        {
            _animator.SetTrigger(BlinkHash);
            _hasBlinked = true;

        }
        if (_spriteRenderer.isVisible)
        _attackTimer -= Time.deltaTime;

    }
    public override void Attack()
    {
        GameObject projectileBase = Instantiate(_projectilePrefab,transform.position,Quaternion.identity);
        ProjectileBase projectile = projectileBase.GetComponent<ProjectileBase>();
        projectile.Setup(_target.transform.position);
    }
}
