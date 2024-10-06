using System.Collections.Generic;

using UnityEngine;

using Entity;
using Utilities;
using System.Linq;

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

    public int Cost => cost;

    public abstract void Shoot(Vector3 position);

    private void Start() {
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        health.onDeath += () => canShoot = false;
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Collider2D[] targets = GetTargets();
            foreach (Collider2D target in GetTargets()) {
                Shoot(target.transform.position);
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
            return null;
        } else {
            return new Collider2D[] { closest };
        }
    }
}