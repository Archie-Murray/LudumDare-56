using UnityEngine;

using ProjectileComponents;
using Entity;

public class TestEnemy : Enemy {
    [SerializeField] int MoneyGained;
    public override void Shoot(Vector3 position) {
        Debug.Log("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(position - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<ProjectileMover>().Speed = projectileSpeed;
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Tower, damage);
    }
    private void Start()
    {
        base.Start();
        GetComponent<Health>().onDeath += () =>
        {
            Globals.instance.money += MoneyGained;
        };
    }
}