using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasLimites : MonoBehaviour
{
    public float limiteGauche1;
    public float limiteDroite1;
    public float limiteHaut1;
    public float limiteBas1;

    public float limiteGauche2;
    public float limiteDroite2;
    public float limiteHaut2;
    public float limiteBas2;


    public float limiteScene2;
    public GameObject Ninja;

    void Update()
    {
        Vector3 camPosition = transform.position;

        

        if (Ninja.transform.position.x <= 30)
        {
            // Vérifie si la caméra est à l'intérieur des premières limites horizontales
            if (camPosition.x < limiteGauche1)
            {
                camPosition.x = limiteGauche1;
            }
            else if (camPosition.x > limiteDroite1)
            {
                camPosition.x = limiteDroite1;
            }
            else if (camPosition.y >= limiteHaut1)
            {
                camPosition.y = limiteHaut1;
            }
            else if (camPosition.y <= limiteBas1)
            {
                camPosition.y = limiteBas1;
            }
        }

        // Vérifie si la caméra est à l'intérieur des deuxièmes limites horizontales

        if (Ninja.transform.position.x >= 30)
        { 
            //if (camPosition.x < limiteScene2) // Mets la caméra dans la scène 2
            //{
            //    camPosition.x = limiteScene2;
            //}
            if (camPosition.x < limiteGauche2)
            {
                camPosition.x = limiteGauche2;
            }
            else if (camPosition.x > limiteDroite2)
            {
                camPosition.x = limiteDroite2;
            }

            else if (camPosition.y >= limiteHaut2)
            {
                camPosition.y = limiteHaut2;
            }
            else if (camPosition.y <= limiteBas2)
            {
                camPosition.y = limiteBas2;
            }
        }

        transform.position = camPosition;
    }
}
