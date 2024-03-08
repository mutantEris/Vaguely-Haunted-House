using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public bool isFlickering = false;
    public float Delay;
    public GameObject Light1;
    public GameObject Light2;


    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }    
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        Light1.SetActive(false);
        Light2.SetActive(false);
        Delay = Random.Range(0.01f, 1f);
        yield return new WaitForSeconds(Delay);
        Light1.SetActive(true);
        Light2.SetActive(true);
        Delay = Random.Range(0.01f, 1f);
        yield return new WaitForSeconds(Delay);
        isFlickering = false;
    }
}
