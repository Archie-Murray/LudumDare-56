using System.Collections;
using System.Collections.Generic;

using Entity;

using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] public Transform[] Locations;
    int counter = 0;
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 360;
    private bool canMove = true;

    void Start() {
        GetComponent<Health>().onDeath += () => canMove = false;
    }
    // Update is called once per frame
    void FixedUpdate() {
        if (Vector2.Distance((Vector2)transform.position, Locations[counter].position) < 0.1) {
            if (counter == Locations.Length - 1) {
                canMove = false;
            } else {
                counter = Mathf.Min(Locations.Length - 1, counter + 1);
            }
        } else if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, Locations[counter].position, speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, Locations[counter].position - transform.position), Vector3.forward),
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}