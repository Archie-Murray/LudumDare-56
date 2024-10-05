using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

using Utilities;

public class Nanny : MonoBehaviour {
    [SerializeField] GameObject[] EnemyTypes;
    [SerializeField] int[] Enemies;
    [SerializeField] Transform[] points;
    [SerializeField] float spawnDelay;
    public bool waveComplete = false;
    [SerializeField] Nanny waitSpawner;

    // Update is called once per frame
    void Update() {
        if (!waveComplete) {
            if (waitSpawner && !waitSpawner.waveComplete) {
                return;
            }
            StartCoroutine(SpawnWave());
        }
    }

    void Spawn(int EnemiesID) {
        GameObject enemyObj = Instantiate(EnemyTypes[EnemiesID], points[0].position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (points[1].position - points[0].position).normalized), Vector3.forward));
        enemyObj.GetComponent<Movement>().Locations = points;
    }
    IEnumerator SpawnWave() {
        for (int i = 0; i < Enemies.Length; i++) {
            Spawn(i);
            yield return Yielders.WaitForSeconds(spawnDelay);
        }
        waveComplete = true;
    }
}