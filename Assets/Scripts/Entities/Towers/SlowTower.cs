using ProjectileComponents;

using UnityEngine;

public class SlowTower : Tower {

    [SerializeField] private float aoeDuration = 3;
    [SerializeField] private float slowDuration = 2;
    [SerializeField] private float speedModifier = 0.5f;
    [SerializeField] private float aoeRadius = 3;

    private static readonly int attackID = Animator.StringToHash("Slow shoot");

    public override void Shoot(Collider2D[] enemies) {
        animator.Play(attackID);
        GameObject slow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        slow.GetOrAddComponent<AutoDestroy>().Duration = aoeDuration;
        slow.GetOrAddComponent<SlowTowerProjectile>().Init(3, speedModifier, slowDuration, aoeDuration, aoeRadius, enemies[0].transform.position, enemy);
        attackTimer.Reset(attackTime);
        attackTimer.Start();
    }
}