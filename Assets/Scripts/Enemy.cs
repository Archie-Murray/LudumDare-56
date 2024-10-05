using Entity;

using UnityEngine;

using Utilities;
using System;
using System.Linq;

public abstract class Enemy : MonoBehaviour {

    [SerializeField] protected Health health;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected CountDownTimer attackTimer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask tower;
    [SerializeField] protected float projectileSpeed = 3;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected bool canShoot = true;
    [SerializeField] protected Animator animator;
    [SerializeField] protected int moneyGained;

    protected readonly int deathID = Animator.StringToHash("Death");

    protected void Start() {
        // tower = LayerMask.NameToLayer("Tower");
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        animator = GetComponent<Animator>();
        health.onDeath += () => {
            canShoot = false;
            animator.Play(deathID);
            Globals.instance.money += moneyGained;
            Instantiate(Assets.instance.enemyDeathParticles, transform.position, transform.rotation);
        };
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Collider2D closest = Physics2D.OverlapCircleAll(transform.position, range, tower).FirstOrDefault();
            if (!closest) {
                return;
            }

            Shoot(closest.transform.position);
            attackTimer.Reset(attackTime);
            attackTimer.Start();
        }
    }

    public abstract void Shoot(Vector3 position);
}