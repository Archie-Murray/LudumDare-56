using System.Collections.Generic;

using UnityEngine;

using Entity;
using Utilities;
using System.Linq;
using ProjectileComponents;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SFXEmitter))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Tower : MonoBehaviour {
    [SerializeField] protected int cost;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected Health health;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected CountDownTimer attackTimer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask enemy;
    [SerializeField] protected bool canShoot = true;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SFXEmitter emitter;
    public int Cost => cost;

    public abstract void Shoot(Collider2D[] enemies);

    private void Start() {
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        emitter = GetComponent<SFXEmitter>();
        animator = GetComponent<Animator>();
        health.onDeath += () => {
            canShoot = false;
            GridManager.instance.RemoveTower(this);
            Instantiate(Assets.instance.towerDeathParticles, transform.position, transform.rotation);
            Destroy(gameObject, emitter.Length(SoundEffectType.Death));
            enabled = false;
        };
        health.onDamage += (_) => {
            emitter.Play(SoundEffectType.Hit);
            Instantiate(Assets.instance.towerHitParticles, transform.position, Quaternion.identity)
                .GetOrAddComponent<AutoDestroy>().Duration = 1f;
        };
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Collider2D[] targets = GetTargets();
            if (targets.Length > 0) {
                Shoot(targets);
            }
        }
    }
    
    protected virtual Collider2D[] GetTargets() {
        Collider2D closest = Physics2D.OverlapCircleAll(transform.position, range, enemy).FirstOrDefault();
        if (!closest) {
            return new Collider2D[] {};
        } else {
            return new Collider2D[] { closest };
        }
    }
}