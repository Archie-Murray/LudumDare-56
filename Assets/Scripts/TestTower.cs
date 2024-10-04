using ProjectileComponents;

using UnityEngine;

public class TestTower : TowerBase {
    public override void Shoot(Vector3 enemy) {
        Debug.Log("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(enemy - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<ProjectileMover>().Speed = 5;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Enemy, damage);
    }
}