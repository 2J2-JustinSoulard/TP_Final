
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiNinja : MonoBehaviour
{
    public bool isMortTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isMortTriggered)
        {
            InvokeRepeating("AttaqueEnnemi", 0f, 5f);
        }
    }


    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (!isMortTriggered)
        {
            if (infoCollision.gameObject.tag == "Ninja" || infoCollision.gameObject.tag == "couteau")
            {
                GetComponent<Animator>().SetTrigger("mort");
                Invoke("Mort", 1f);
                isMortTriggered = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }

    void Mort()
    {
        Destroy(gameObject);
    }
    void AttaqueEnnemi()
    {
        GetComponent<Animator>().SetTrigger("attaqueAnim");
    }
}