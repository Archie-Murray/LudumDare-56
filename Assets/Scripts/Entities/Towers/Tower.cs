using System.Collections.Generic;

using UnityEngine;

using Entity;
using Utilities;
using System.Linq;
using ProjectileComponents;
using System;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SFXEmitter))]
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
    private bool debug = true;

    public int Cost => cost;

    public abstract void Shoot(Collider2D[] enemies);

    private void Start() {
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        emitter = GetComponent<SFXEmitter>();
        animator = GetComponentInChildren<Animator>();
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
        Collider2D[] closest = Physics2D.OverlapCircleAll(transform.position, range, enemy);
        if (closest.Length == 0) {
            return new Collider2D[] {};
        } else {
            return closest;
        }
    }

    protected Collider2D GetClosest(Collider2D[] enemies) {
        Array.Sort(enemies, CompareDistance);
        return enemies[0];
    }

    private int CompareDistance(Collider2D enemy1, Collider2D enemy2) {
        float dist1 = Vector2.Distance(enemy1.transform.position, transform.position);
        float dist2 = Vector2.Distance(enemy2.transform.position, transform.position);
        if (dist1 > dist2) {
            return 1;
        } 
        if (dist1 < dist2) {
            return -1;
        }
        return 0;
    }
}