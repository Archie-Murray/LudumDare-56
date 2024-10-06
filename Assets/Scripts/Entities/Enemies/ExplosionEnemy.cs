using ProjectileComponents;

using UnityEngine;

public class ExplosionEnemy : Enemy {

    [SerializeField] private float explosionRange = 4;
    [SerializeField] private float shotHeight = 4;

    public override void Shoot(Collider2D tower) {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(tower.transform.position - transform.position).normalized), Vector3.forward));
        // TODO: Making arc projectile mover
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<ArcProjectileController>().Init(projectileSpeed, damage, explosionRange, shotHeight, tower.transform, base.tower);
    }
}