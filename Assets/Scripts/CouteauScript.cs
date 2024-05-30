using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouteauScript : MonoBehaviour
{

    //public AudioClip SonsExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        Destroy(gameObject, 1f);
        //GetComponent<Animator>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        GetComponent<PolygonCollider2D>().enabled = false;

        if (infoCollision.gameObject.tag == "Ennemi1")
        {
            //GetComponent<AudioSource>().PlayOneShot(SonsExplosion);
            Destroy(infoCollision.gameObject, 1.5f);

        }

    }
}

