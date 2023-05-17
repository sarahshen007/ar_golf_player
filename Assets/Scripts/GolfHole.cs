using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    Swing golf_club;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "mp_ball") {
            transform.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.SetActive(false);
            golf_club.score.text = "final score:" + golf_club.score.text;
        }
    }
}
