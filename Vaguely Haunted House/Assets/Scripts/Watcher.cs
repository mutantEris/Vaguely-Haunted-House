using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    public Transform Player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     

     void Update()
     {
          if(Player != null)
          {
               //transform.LookAt(Player);
               Vector3 targetPostition = new Vector3( Player.position.x, this.transform.position.y, Player.position.z ) ;
               this.transform.LookAt( targetPostition ) ;
          }
     }
}
