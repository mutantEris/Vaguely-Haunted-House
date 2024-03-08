using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject PlayerObj;
    private  MP3Player mp3_player_script;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mp3_player_script = PlayerObj.GetComponent<MP3Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mp3_player_script.mp3_player_state == 1){
        Vector3 away_pos = transform.position + (transform.position - PlayerObj.transform.position);
        agent.destination = away_pos;
        }
        else{
        agent.destination = PlayerObj.transform.position;
        }
    }
}
