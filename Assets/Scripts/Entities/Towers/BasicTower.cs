using ProjectileComponents;

using UnityEngine;

public class BasicTower : Tower {
    [SerializeField] string AnimName = "Hand Throw";

    public override void Shoot(Collider2D[] enemies) {

        Debug.Log("Shoot");
        animator.Play(AnimName);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(enemies[0].transform.position - transform.position).normalized), Vector3.forward));
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<ProjectileMover>().Speed = 5;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Enemy, damage);
        attackTimer.Reset(attackTime);
        attackTimer.Start();
        emitter.Play(SoundEffectType.Shoot);
    }
}