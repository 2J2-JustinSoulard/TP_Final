using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeanime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionExit2D(Collision2D InfosCollision)
    {
        if (InfosCollision.gameObject.tag == "plateformeanime")
        {
            gameObject.transform.parent = null;
        }
    }

    void OnCollisionEnter2D(Collision2D InfosCollision)
    { 

            // Est-ce qu'il touche a une plateforme animée. Si oui, on doit mettre Sonic enfant de cette
            // plateforme
            if (InfosCollision.gameObject.tag == "plateformeanime")
            {
                gameObject.transform.parent = InfosCollision.gameObject.transform;
            }

    }

}
