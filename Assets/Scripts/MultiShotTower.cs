using ProjectileComponents;

using UnityEngine;

public class MultiShotTower : TowerBase {

    [SerializeField] private int maxProjectiles = 3;

    protected override Collider2D[] GetTargets() {
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, range, enemy);
        if (inRange.Length == 0) {
            return new Collider2D[] {};
        }
        Collider2D[] targets = new Collider2D[Mathf.Min(maxProjectiles, inRange.Length)];
        for (int i = 0; i < targets.Length; i++) {
            targets[i] = inRange[i];
        }
        return targets;
    }

    public override void Shoot(Vector3 enemy) {
        Debug.Log("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(enemy - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<ProjectileMover>().Speed = 4;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Enemy, damage);
    }
}