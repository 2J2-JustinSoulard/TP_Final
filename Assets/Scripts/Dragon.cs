using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public bool isMortTriggered = false;

    public AudioClip SonFeu;
    public GameObject Feu;
    public GameObject Texte;
    public GameObject Parchemin;

    public float Vie = 0;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("Consigne", 1f);

        if (!isMortTriggered)
        {
            InvokeRepeating("AttaqueDragon", 2f, 4f);
        }
    }
    private void Update()
    {
        if (isMortTriggered)
        {
            Parchemin.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D infoCollision)
    {
        if (!isMortTriggered)
        {
            if (infoCollision.gameObject.tag == "Ninja" || infoCollision.gameObject.tag == "couteau")
            {
                Vie++;
            }
            if (Vie == 20)
            {
                Invoke("Fin", 0f);
            }
        }
    }
    void Fin()
    {
        GetComponent<Animator>().SetTrigger("mort");
        Invoke("Mort", 1f);
        isMortTriggered = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    void Mort()
    {
        Destroy(gameObject);
    }

    void AttaqueDragon()
    {
        GetComponent<Animator>().SetTrigger("attaqueAnim");

        GetComponent<AudioSource>().PlayOneShot(SonFeu);

        GameObject FeuClone = Instantiate(Feu);
        FeuClone.SetActive(true);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            FeuClone.transform.position = transform.position + new Vector3(6f, 5);
            FeuClone.GetComponent<Rigidbody2D>().velocity = new Vector2(30, 4);
        }
        else if (GetComponent<SpriteRenderer>().flipX == true)
        {
            FeuClone.transform.position = transform.position + new Vector3(-6f, 5);
            FeuClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-30, 4);
            FeuClone.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Consigne()
    {
        Texte.SetActive(true);
        Destroy(Texte, 2f);
    }
}
