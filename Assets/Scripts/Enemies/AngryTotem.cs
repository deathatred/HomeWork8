using UnityEngine;

public class AngryTotem : EnemyBase
{
    [SerializeField] private GameObject _projectilePrefab;
    private string _name = "AngryTotem";
    private int _damage = 5;
    private int _startHealth = 1;
    private GameObject _target;
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
        Attack();
    }
    public override void Attack()
    {
        GameObject projectileBase = Instantiate(_projectilePrefab,transform.position,Quaternion.identity);
        ProjectileBase projectile = projectileBase.GetComponent<ProjectileBase>();
        projectile.Setup(_target.transform.position);
    }
}
