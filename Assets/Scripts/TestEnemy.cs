using UnityEngine;

using ProjectileComponents;

public class TestEnemy : Enemy {
    public override void Shoot(Vector3 position) {
        Debug.Log("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(position - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<ProjectileMover>().Speed = projectileSpeed;
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Tower, damage);
    }
}