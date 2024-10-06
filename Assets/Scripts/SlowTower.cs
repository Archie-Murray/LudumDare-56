using ProjectileComponents;

using UnityEngine;

public class SlowTower : TowerBase {

    [SerializeField] private float aoeDuration = 3;
    [SerializeField] private float slowDuration = 2;
    [SerializeField] private float speedModifier = 0.5f;
    [SerializeField] private float aoeRadius = 3;

    public override void Shoot(Vector3 position) {
        GameObject slow = Instantiate(projectilePrefab, position, Quaternion.identity);
        slow.GetOrAddComponent<AutoDestroy>().Duration = aoeDuration;
        slow.GetOrAddComponent<EntityAoESlow>().Init(speedModifier, slowDuration, aoeRadius);
    }
}