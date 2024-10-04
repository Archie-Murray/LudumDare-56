using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Nanny : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyTypes;
    [SerializeField] int[] Enemies;
    int EnemyID;
    [SerializeField] float spawnDelay;
    public bool waveComplete = false;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            spawn(i);
            waiter();
            if(i == Enemies.Length)
            {
                waveComplete = true;
            }

        }
        
        
    }
    void spawn(int EnemiesID)
    {
        EnemyTypes[EnemyID].transform.parent = transform;
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(spawnDelay);
    }
}
