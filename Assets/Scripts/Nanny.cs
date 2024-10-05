using System;
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
    public Transform[] Locations => points;
    public Action OnWaveComplete;


    void Spawn(int EnemiesID) {
        GameObject enemyObj = Instantiate(EnemyTypes[EnemiesID], points[0].position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, (points[1].position - points[0].position).normalized), Vector3.forward));
        enemyObj.GetComponent<Movement>().Locations = points;
    }
    IEnumerator SpawnWave() {
        for (int i = 0; i < wave.Length; i++) {
            Spawn(wave[i]);
            yield return Yielders.WaitForSeconds(spawnDelay);
        }
        OnWaveComplete?.Invoke();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
}