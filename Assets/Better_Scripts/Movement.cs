using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2[] Locations;
    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance ((Vector2)transform.position, Locations[counter]) < 0.1)
        {
         transform.  position = Vector2.Lerp(transform.position, Locations[counter], Time.deltaTime);
        }
        else if (counter == Locations.Length)
        {
            Destroy(gameObject);
        }
        else { counter++; }
    }
}
