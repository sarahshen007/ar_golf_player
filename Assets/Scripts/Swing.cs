using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class Swing : MonoBehaviour
{
    public const float MAX_HEIGHT = 1;
    PlayerInput playerInput;
    public float swingc;

    [SerializeField]
    GameObject forwards;

    [SerializeField]
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        swingc = 0;
        playerInput = transform.gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = swingc.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mp_ball") {
            other.gameObject.GetComponent<Rigidbody>().AddForce(forwards.transform.forward * 60);

            swingc++;
        }
    }
}
