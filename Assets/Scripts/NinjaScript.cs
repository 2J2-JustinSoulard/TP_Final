using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaScript : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée
    public bool partieTerminee;
    public bool attaque;
    public bool DeplacementAttaque;
    public bool PeutTirer = true;

    public GameObject FinScene1;
    public GameObject DebutScene2;
    public GameObject Debutscene3;

    public GameObject ImgBulle1;
    public GameObject ImgBulle2;
    public GameObject Fleche;

    public GameObject consigne;
    public GameObject consigne2;
    public GameObject consigne3;
    public GameObject consigne4;
    public GameObject consigne5;

    private bool isAtStop = false; // Ajouter un indicateur pour vérifier l'événement de collision
    private bool uneFois = true;

    public int nbClefsRamassees;
    public GameObject cleUI1;
    public GameObject cleUI2;
    public GameObject cleUI3;

    public GameObject CouteauOriginale;

    //public AudioClip MusiqueIntro;
    public AudioClip Sabre;
    public AudioClip SonsCouteau;
    public AudioClip Mort;
    public AudioClip Cristaux;
    //public AudioClip PorteFermee;
    public AudioClip PorteOuverture;

    private void Start()
    {
        //GetComponent<AudioSource>().PlayOneShot(MusiqueIntro);
    }
    void Update()
    {
        if (!partieTerminee)
        {
            // déplacement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   //déplacement vers la droite
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
            }


            // sauter l'objet à l'aide la touche "w"
            //Physics2D.OverlapCircle(transform.position, 0.2f);

            if ((Input.GetKeyDown("w") && Physics2D.OverlapCircle(transform.position, 4f)) || (Input.GetKeyDown(KeyCode.UpArrow) && Physics2D.OverlapCircle(transform.position, 4f)))
            {
                vitesseY = vitesseSaut;
                GetComponent<Animator>().SetBool("saut", true);
                PeutTirer = false;
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
            }

            if (Input.GetKeyDown(KeyCode.Space) && !attaque)
            {
                attaque = true;
                Invoke("AnnulerAttaque", 0.5f);
                DeplacementAttaque = true;
                Invoke("AnnulerDeplacementAttaque", 0.4f);
                GetComponent<AudioSource>().PlayOneShot(Sabre);

                GetComponent<Animator>().SetTrigger("attaqueAnim");
                GetComponent<Animator>().SetBool("saut", false);
            }

            if (Input.GetKeyDown(KeyCode.Return) && PeutTirer)
            {
                GetComponent<Animator>().SetTrigger("lancer");
                GetComponent<AudioSource>().PlayOneShot(SonsCouteau);

                GameObject couteauClone = Instantiate(CouteauOriginale);
                couteauClone.SetActive(true);

                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    couteauClone.transform.position = transform.position + new Vector3(3f, 1);
                    couteauClone.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 0);
                }
                else if (GetComponent<SpriteRenderer>().flipX == true)
                {
                    couteauClone.transform.position = transform.position + new Vector3(-3f, 1);
                    couteauClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 0);
                    couteauClone.GetComponent<SpriteRenderer>().flipX = true;

                }

            }
            if (DeplacementAttaque && vitesseX <= vitesseXMax && vitesseX >= -vitesseXMax)
            {
                vitesseX *= 2;
            }

            //Applique les vitesses en X et Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);

            //**************************Gestion des animaitons de course et de repos********************************
            //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator

            if (vitesseX > 0.05f || vitesseX < -0.3f)
            {
                GetComponent<Animator>().SetBool("course", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("course", false);
            }
        }

        // Vérifiez l'entrée de la touche Espace dans Update
        if (isAtStop && Input.GetKeyDown(KeyCode.Space))
        {
            isAtStop = false;
            Invoke("ArretPersonnage", 0.5f);
            Invoke("ChangeTexte", 0.7f);
            Invoke("ChangeTexte2", 4f);
        }
    }

    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (Physics2D.OverlapCircle(transform.position, 4f))
        {
            GetComponent<Animator>().SetBool("saut", false);
            PeutTirer = true;
        }
        if (infoCollision.gameObject.tag == "crystaux")
        {
            nbClefsRamassees++;
            infoCollision.gameObject.SetActive(false);
            AfficheClesUi();

            GetComponent<AudioSource>().PlayOneShot(Cristaux);
        }

        if (infoCollision.gameObject.tag == "Ennemi1")
        {
            if (!attaque)
            {
                GetComponent<AudioSource>().PlayOneShot(Mort);

                partieTerminee = true;
                GetComponent<Animator>().SetTrigger("mort");
                Invoke("recommencer", 3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D infoCollision)
    {
        Debug.Log("Collision avec: " + infoCollision.gameObject.tag);
        if (infoCollision.gameObject.CompareTag("Sensai"))
        {
            partieTerminee = true;
            GetComponent<Animator>().SetBool("course", false);
            Invoke("ArretPersonnage", 3f);
            Invoke("ChangeImg", 1.5f);
            infoCollision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            //GetComponent<AudioSource>().PlayOneShot();
        }
        if (uneFois && infoCollision.gameObject.CompareTag("arret"))
        {
            consigne.SetActive(true);
            partieTerminee = true;
            GetComponent<Animator>().SetBool("course", false);
            isAtStop = true; // Définir l'indicateur sur true
            uneFois = false;
        }

        if (FinScene1 != null && DebutScene2 != null && infoCollision.gameObject.name == "FinScene1")
        {
            gameObject.transform.position = DebutScene2.transform.position;
        }
        if (infoCollision.gameObject.name == "TransitionScene")
        {
            GetComponent<Animator>().SetBool("course", false);
            partieTerminee = true;
            Invoke("ArretPersonnage", 1f);
            Invoke("changerScene2", 1f);
        }
        if (infoCollision.gameObject.tag == "porte")
        {
            if (nbClefsRamassees == 3)
            {
                infoCollision.gameObject.GetComponent<Animator>().enabled = true;
                partieTerminee = true;
                Invoke("changerScene", 3f);
                Invoke("ArretPersonnage", 3f);
                GetComponent<Animator>().SetBool("course", false);
                GetComponent<AudioSource>().PlayOneShot(PorteOuverture);
            }
        }
        if (infoCollision.gameObject.tag == "Vide")
        {
            GetComponent<AudioSource>().PlayOneShot(Mort);

            partieTerminee = true;
            GetComponent<Animator>().SetTrigger("mort");
            Invoke("recommencer", 2f);
        }
        if (infoCollision.gameObject.tag == "Victoire")
        {
            GetComponent<AudioSource>().PlayOneShot(Cristaux);
            Invoke("sceneVictoire", 1.5f);
            GetComponent<Animator>().SetBool("course", false);
            partieTerminee = true;
        }
        if (infoCollision.gameObject.tag == "dragon")
        {
            if (!attaque)
            {
                GetComponent<AudioSource>().PlayOneShot(Mort);
                partieTerminee = true;
                GetComponent<Animator>().SetBool("course", false);
                GetComponent<Animator>().SetTrigger("mort");
                Invoke("recommencer", 3f);
            }
        }
        if (infoCollision.gameObject.name == "ConsigneBox")
        {
            Destroy(infoCollision, 1f);
            Destroy(consigne5, 3f);
        }
    }


    void recommencer()
    {
        SceneManager.LoadScene(4);
    }
    void changerScene()
    {
        SceneManager.LoadScene(2);
    }
    void changerScene2()
    {
        SceneManager.LoadScene(3);
    }
    void AnnulerDeplacementAttaque()
    {
        DeplacementAttaque = false;
    }
    void AnnulerAttaque()
    {
        attaque = false;
    }
    void ArretPersonnage()
    {
        partieTerminee = false;

    }
    void ChangeImg()
    {
        Invoke("FlecheApparait", 1f);
        ImgBulle2.SetActive(true);
        ImgBulle1.SetActive(false);
    }
    void FlecheApparait()
    {
        Fleche.SetActive(true);
    }
    void ChangeTexte()
    {
        consigne.SetActive(false);
        consigne2.SetActive(true);

    }
    void ChangeTexte2()
    {
        consigne2.SetActive(false);
        consigne3.SetActive(true);
    }

    void AfficheClesUi()
    {
        if (nbClefsRamassees == 1)
        {
            cleUI1.gameObject.SetActive(true);
            cleUI2.gameObject.SetActive(false);
            cleUI3.gameObject.SetActive(false);
        }

        if (nbClefsRamassees == 2)
        {
            cleUI1.gameObject.SetActive(true);
            cleUI2.gameObject.SetActive(true);
            cleUI3.gameObject.SetActive(false);
        }

        if (nbClefsRamassees == 3)
        {
            cleUI1.gameObject.SetActive(true);
            cleUI2.gameObject.SetActive(true);
            cleUI3.gameObject.SetActive(true);

            consigne3.SetActive(false);
            consigne4.SetActive(true);
        }
    }

    void sceneVictoire()
    {
        SceneManager.LoadScene(5);
    }

}