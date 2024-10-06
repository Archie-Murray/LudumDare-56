using System.Collections.Generic;

using UnityEngine;

using Entity;
using Utilities;
using System.Linq;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SFXEmitter))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class TowerBase : MonoBehaviour {
    [SerializeField] protected int cost;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected Health health;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected CountDownTimer attackTimer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask enemy;
    [SerializeField] protected bool canShoot = true;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SFXEmitter emitter;
    public int Cost => cost;

    public abstract void Shoot(Vector3 position);

    private void Start() {
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        health.onDeath += () => {
            canShoot = false;
            GridManager.instance.RemoveTower(this);
            Destroy(gameObject, emitter.Length(SoundEffectType.Death));
            enabled = false;
        };
        health.onDamage += (_) => emitter.Play(SoundEffectType.Hit);
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Collider2D[] targets = GetTargets();
            foreach (Collider2D target in targets) {
                transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (Vector2)(target.transform.position - transform.position).normalized), Vector3.forward);
                Shoot(target.transform.position);
                emitter.Play(SoundEffectType.Shoot);
            }
            if (targets.Length > 0) {
                attackTimer.Reset(attackTime);
                attackTimer.Start();
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