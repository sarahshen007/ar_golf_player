using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotFallThroughFloor : MonoBehaviour
{
    static float MAX_DIST = 50;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > MAX_DIST) {
            transform.position = new Vector3(MAX_DIST,transform.position.y,transform.position.z);
        }

        if (transform.position.x < -MAX_DIST) {
            transform.position = new Vector3(-MAX_DIST,transform.position.y,transform.position.z);
        }

        if (transform.position.z > MAX_DIST) {
            transform.position = new Vector3(transform.position.x,transform.position.y,MAX_DIST);
        }

        if (transform.position.z < -MAX_DIST) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -MAX_DIST);
        }
    }
}
