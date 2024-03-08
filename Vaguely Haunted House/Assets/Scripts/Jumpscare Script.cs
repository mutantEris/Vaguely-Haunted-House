using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareScript : MonoBehaviour
{
    [SerializeField] public AudioSource Scream;
    [SerializeField] public GameObject BOO;

    // Start is called before the first frame update
    void Start()
    {
        //Scream = new AudioSource();
        //Scream = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        Scream.Play();
        BOO.SetActive(true);
        }
    }
}
