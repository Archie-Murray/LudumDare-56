using UnityEngine;

using ProjectileComponents;
using Entity;

public class BasicEnemy : Enemy {
    public override void Shoot(Collider2D tower) {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(tower.transform.position - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<ProjectileMover>().Speed = projectileSpeed;
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Tower, damage);
    }
}