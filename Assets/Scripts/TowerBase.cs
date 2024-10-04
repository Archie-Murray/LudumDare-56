using System.Collections.Generic;

using UnityEngine;

using Entity;
using Utilities;
using System.Linq;

public abstract class TowerBase : MonoBehaviour {
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected Health health;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected CountDownTimer attackTimer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask enemy;
    [SerializeField] protected bool canShoot = true;

    public abstract void Shoot(Vector3 position);

    private void Start() {
        attackTimer = new CountDownTimer(0f);
        attackTimer.Start();
        health.onDeath += () => canShoot = false;
    }

    private void FixedUpdate() {
        attackTimer.Update(Time.fixedDeltaTime);
        if (attackTimer.IsFinished && canShoot) {
            Debug.Log("Checking for close objects");
            Collider2D closest = Physics2D.OverlapCircleAll(transform.position, range, enemy).FirstOrDefault();
            if (!closest) {
                Debug.Log("No objects found");
                return;
            }

            Shoot(closest.transform.position);
            attackTimer.Reset(attackTime);
            attackTimer.Start();
        }
    }
    
}