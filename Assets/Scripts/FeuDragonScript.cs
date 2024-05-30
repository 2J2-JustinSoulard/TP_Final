using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeuDragonScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D infoCollision)
    {

        GetComponent<Animator>().SetTrigger("AnimFeu");
        Destroy(gameObject, 2f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (infoCollision.gameObject.tag == "Decor")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
