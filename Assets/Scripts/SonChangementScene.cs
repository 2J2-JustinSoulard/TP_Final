using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChangementScene : MonoBehaviour
{
    public AudioClip Musique;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(Musique);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
