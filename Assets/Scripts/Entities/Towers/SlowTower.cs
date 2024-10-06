using ProjectileComponents;

using UnityEngine;

public class SlowTower : Tower {

    [SerializeField] private float aoeDuration = 3;
    [SerializeField] private float slowDuration = 2;
    [SerializeField] private float speedModifier = 0.5f;
    [SerializeField] private float aoeRadius = 3;

    public override void Shoot(Collider2D[] enemies) {
        GameObject slow = Instantiate(projectilePrefab, enemies[0].transform.position, Quaternion.identity);
        slow.GetOrAddComponent<AutoDestroy>().Duration = aoeDuration;
        slow.GetOrAddComponent<EntityAoESlow>().Init(speedModifier, slowDuration, aoeRadius);
        attackTimer.Reset(attackTime);
        attackTimer.Start();
    }
}