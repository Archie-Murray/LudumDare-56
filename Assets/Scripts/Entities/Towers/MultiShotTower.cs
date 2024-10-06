using System;
using System.Collections;

using ProjectileComponents;

using UnityEngine;

using Utilities;

public class MultiShotTower : Tower {

    [SerializeField] private int maxProjectiles = 3;
    [SerializeField] private float shotDelay = 0.5f;
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float heightModifier = 0.25f;
    static readonly int attackID = Animator.StringToHash("Foot");
    const float ANIM_TIME = 0.5f;

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



    public override void Shoot(Collider2D[] enemy) {
        StartCoroutine(ShootTargets(enemy));
    }

    private IEnumerator ShootTargets(Collider2D[] enemies) {
        canShoot = false;
        animator.speed = ANIM_TIME / shotDelay;
        foreach (Collider2D enemy in enemies) {
            if (!enemy)
            {
                continue;
            }
            emitter.Play(SoundEffectType.Shoot);
            animator.Play(attackID);
            Quaternion rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(enemy.transform.position - transform.position).normalized), Vector3.forward);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);
            projectile.GetOrAddComponent<AutoDestroy>().Duration = 4;
            projectile.GetOrAddComponent<ArcProjectileController>().Init(projectileSpeed, damage, range, range * heightModifier, enemy.transform, this.enemy);
            yield return Yielders.WaitForSeconds(shotDelay);
        }
        attackTimer.Reset(attackTime);
        attackTimer.Start();
        canShoot = true;
    }
}