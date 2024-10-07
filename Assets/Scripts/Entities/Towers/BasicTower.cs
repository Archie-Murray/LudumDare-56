using ProjectileComponents;

using UnityEngine;

public class BasicTower : Tower {
    [SerializeField] string AnimName = "Hand Throw";
    [SerializeField] private float projectileSpeed;

    public override void Shoot(Collider2D[] enemies) {

        Debug.Log("Shoot");
        animator.Play(AnimName);
        Quaternion rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(GetClosest(enemies).transform.position - transform.position).normalized), Vector3.forward);
        animator.transform.rotation = rotation;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);
        projectile.GetOrAddComponent<AutoDestroy>().Duration = 2;
        projectile.GetOrAddComponent<ProjectileMover>().Speed = projectileSpeed;
        projectile.GetOrAddComponent<EntityDamager>().Init(DamageFilter.Enemy, damage);
        attackTimer.Reset(attackTime);
        attackTimer.Start();
        emitter.Play(SoundEffectType.Shoot);
    }
}