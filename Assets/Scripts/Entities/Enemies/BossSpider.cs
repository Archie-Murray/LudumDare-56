using System.Collections;
using System.Collections.Generic;

using Entity;

using Spider;

using UnityEngine;

using Utilities;

public class BossSpider : MonoBehaviour {
    [Header("Gameplay")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float eggDropCooldown = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackTime = 1.5f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private CountDownTimer attackTimer = new CountDownTimer(0f);
    [SerializeField] private CountDownTimer eggTimer = new CountDownTimer(0f);
    [SerializeField] private bool dead = false;
    [SerializeField] private LayerMask hit;
    [SerializeField] private int moneyGained = 100;

    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;
    [SerializeField] private SFXEmitter emitter;

    readonly private int deathID = Animator.StringToHash("Death");

    // Start is called before the first frame update
    private void Start() {
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
        emitter = GetComponent<SFXEmitter>();
        hit = 1 << LayerMask.NameToLayer("Tower") | 1 << LayerMask.NameToLayer("End");
        attackTimer.Start();
        eggTimer.Start();

        health.onDamage += (_) => emitter.Play(SoundEffectType.Hit);
        health.onDeath += () => {
            dead = true;
            emitter.Play(SoundEffectType.Death);
            animator.Play(deathID);
            Globals.instance.money += moneyGained;
            Instantiate(Assets.instance.enemyDeathParticles, transform.position, transform.rotation);
            Destroy(gameObject, emitter.Length(SoundEffectType.Death));
            enabled = false;
        };
    }

    private void FixedUpdate() {
        if (dead) {
            return;
        }
        eggTimer.Update(Time.fixedDeltaTime);
        attackTimer.Update(Time.fixedDeltaTime);
        if (eggTimer.IsFinished) {
            GameObject egg = Instantiate(eggPrefab, transform.position, transform.rotation);
            egg.GetComponent<Egg>().Init(movement.Locations, movement.counter);
            eggTimer.Reset(eggDropCooldown);
            eggTimer.Start();
        }
        if (attackTimer.IsFinished) {
            movement.AllowMovement();
            foreach (Collider2D entity in Physics2D.OverlapCircleAll(transform.position, attackRange, hit)) {
                movement.StopMovement();
                if (!entity.TryGetComponent(out Health health)) { return; }
                health.Damage(damage);
                attackTimer.Reset(attackTime);
                attackTimer.Start();
            }
        }
    }
}