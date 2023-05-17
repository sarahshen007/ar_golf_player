using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerControllerScript : MonoBehaviour
{

    // necessary prefabs
    [SerializeField]
    public GameObject paper_prefab;
    [SerializeField]
    public GameObject crate_prefab;
    [SerializeField]
    public GameObject bush_prefab;
    [SerializeField]
    public GameObject slabs_prefab;
    [SerializeField]
    public GameObject rock_prefab;
    [SerializeField]
    public GameObject hole_prefab;
    [SerializeField]
    public GameObject ball_prefab;
    [SerializeField]
    public GameObject spawn_prefab;

    // right hand controller objects
    [SerializeField]
    public GameObject golf_club; // golf club
    [SerializeField]
    public GameObject placed_paper; // paper placer

    // left hand controller text instructions
    [SerializeField]
    public TextMeshProUGUI left_text;

    // positional data asset
    [SerializeField]
    public TextAsset info;

    // player input
    PlayerInput playerInput;

    // game reference paper
    GameObject paper;

    // spawn point
    GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        golf_club.SetActive(false);
        placed_paper.SetActive(true);

        playerInput.SwitchCurrentActionMap("SetMode");
        spawnpoint = Instantiate(spawn_prefab, Vector3.zero, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Confirm"].triggered) {

            Vector3 paper_pos = new Vector3(placed_paper.transform.position.x, 0, placed_paper.transform.position.z);
            paper = Instantiate(paper_prefab, Vector3.zero, Quaternion.identity);
            paper.transform.position = paper_pos;
            float yRotation = placed_paper.transform.eulerAngles.y;
            paper.transform.eulerAngles = new Vector3( paper.transform.eulerAngles.x, yRotation, paper.transform.eulerAngles.z);

            spawnpoint.transform.position = paper.transform.position;
            spawnpoint.transform.rotation = paper.transform.rotation;

            playerInput.SwitchCurrentActionMap("PlayMode");
            placed_paper.SetActive(false);
            golf_club.SetActive(true);
            left_text.text = "";
            
            string[] data_lines = info.text.Split('\n'); 
            List<string[]> data_list = new List<string[]>();
            
            for (int i = 0; i < data_lines.Length; i++ ) {
                string[] line = data_lines[i].Split(",");

                Debug.Log("added data line:");
                Debug.Log(String.Join(" ", line));

                data_list.Add(line);
            }

            for (int i = 0; i < data_list.Count; i++) {

                if (data_list[i].Length == 5) {
                    string tag = data_list[i][0];
                    float y_rot = float.Parse(data_list[i][1]) + paper.transform.eulerAngles.y;
                    float x = float.Parse(data_list[i][2]) + paper.transform.position.x;
                    float y = float.Parse(data_list[i][3]) + paper.transform.position.y;
                    float z = float.Parse(data_list[i][4]) + paper.transform.position.z;
                    
                    Vector3 newobj_pos = new Vector3(x,y,z);
                    Vector3 newobj_rot = new Vector3(0,y_rot,0);

                    GameObject prefab = crate_prefab;

                    if (tag == "mp_bush") {
                        prefab = bush_prefab;
                    } else if (tag == "mp_slabs") {
                        prefab = slabs_prefab;
                    } else if (tag == "mp_rock") {
                        prefab = rock_prefab;
                    } else if (tag == "mp_hole") {
                        prefab = hole_prefab;
                    } else if (tag == "mp_ball") {
                        prefab = ball_prefab;
                    }

                    GameObject new_obj = Instantiate(prefab, spawnpoint.transform);
                    new_obj.transform.eulerAngles = newobj_rot;
                    new_obj.transform.position = newobj_pos;
                }
            }
            
        }
    }
}
