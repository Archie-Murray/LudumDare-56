using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] public Transform[] Locations;
    int counter = 0;
    [SerializeField] float speed = 5;
    // Update is called once per frame
    void Update() {
        if (Vector2.Distance((Vector2)transform.position, Locations[counter].position) < 0.1) {
            counter = Mathf.Min(Locations.Length - 1, counter + 1);
        } else if (counter == Locations.Length) {
            Destroy(gameObject);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, Locations[counter].position, Time.deltaTime * speed);
        }
    }
}