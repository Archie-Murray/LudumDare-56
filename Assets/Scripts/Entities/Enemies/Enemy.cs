using Entity;

using UnityEngine;

using Utilities;
using System;
using System.Linq;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SFXEmitter))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {

    [SerializeField] protected Health health;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected CountDownTimer attackTimer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask tower;
    [SerializeField] protected LayerMask End;
    [SerializeField] protected float projectileSpeed = 3;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected bool canShoot = true;
    [SerializeField] protected Animator animator;
    [SerializeField] protected int moneyGained;
    [SerializeField] protected SFXEmitter emitter;

    protected readonly int deathID = Animator.StringToHash("Death");

    protected void Start() {
        emitter = GetComponent<SFXEmitter>();
        // tower = LayerMask.NameToLayer("Tower");
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        animator = GetComponent<Animator>();
        health.onDeath += () => {
            canShoot = false;
            emitter.Play(SoundEffectType.Death);
            animator.Play(deathID);
            Globals.instance.money += moneyGained;
            Instantiate(Assets.instance.enemyDeathParticles, transform.position, transform.rotation);
            Destroy(gameObject, emitter.Length(SoundEffectType.Death));
            enabled = false;
        };
        health.onDamage += (float _) => emitter.Play(SoundEffectType.Hit);
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Collider2D closest = Physics2D.OverlapCircleAll(transform.position, range, tower).FirstOrDefault();
            if (!closest) {
                return;
            }

            Shoot(closest);
            emitter.Play(SoundEffectType.Shoot);
            attackTimer.Reset(attackTime);
            attackTimer.Start();
        }
    }

    public abstract void Shoot(Collider2D position);

    private void OnTriggerEnter2D(Collider2D colide)
    {
        if(1 << colide.gameObject.layer == End.value) 
        {
            if(colide.TryGetComponent(out Health health))
                {
                health.Damage(damage);
                Destroy(gameObject);
            }

        }

    }
}