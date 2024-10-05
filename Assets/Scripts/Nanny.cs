using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

using Utilities;

public class Nanny : MonoBehaviour {
    [SerializeField] GameObject[] EnemyTypes;
    [SerializeField] int[] wave;
    [SerializeField] Transform[] points;
    [SerializeField] float spawnDelay;
    public bool waveComplete = false;
    [SerializeField] Nanny waitSpawner;
    public Transform[] Locations => points;

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
        waveComplete = true;
        for (int i = 0; i < wave.Length; i++) {
            Spawn(wave[i]);
            yield return  Yielders.WaitForSeconds(spawnDelay);
        }
        
    }
}